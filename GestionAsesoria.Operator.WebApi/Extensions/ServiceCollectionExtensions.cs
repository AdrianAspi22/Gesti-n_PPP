using FluentValidation;
using GestionAsesoria.Operator.Application.Configurations;
using GestionAsesoria.Operator.Application.Interfaces.Serialization.Options;
using GestionAsesoria.Operator.Application.Interfaces.Serialization.Serializers;
using GestionAsesoria.Operator.Application.Interfaces.Serialization.Settings;
using GestionAsesoria.Operator.Application.Interfaces.Services;
using GestionAsesoria.Operator.Application.Interfaces.Services.Account;
using GestionAsesoria.Operator.Application.Interfaces.Services.ExternalRequest;
using GestionAsesoria.Operator.Application.Interfaces.Services.Identity;
using GestionAsesoria.Operator.Application.Serialization.JsonConverters;
using GestionAsesoria.Operator.Application.Serialization.Options;
using GestionAsesoria.Operator.Application.Serialization.Serializers;
using GestionAsesoria.Operator.Application.Serialization.Settings;
using GestionAsesoria.Operator.Domain.ConfigParameters.Container;
using GestionAsesoria.Operator.Domain.Entities.Identity;
using GestionAsesoria.Operator.Infrastructure.Persistence.Contexts;
using GestionAsesoria.Operator.Infrastructure.Persistence.Seed;
using GestionAsesoria.Operator.Infrastructure.Persistence.Services.ExternalRequest;
using GestionAsesoria.Operator.Infrastructure.Services;
using GestionAsesoria.Operator.Infrastructure.Services.Identity;
using GestionAsesoria.Operator.Infrastructure.Shared.Services;
using GestionAsesoria.Operator.Shared.Constants.Application;
using GestionAsesoria.Operator.Shared.Constants.Localization;
using GestionAsesoria.Operator.Shared.Constants.Permission;
using GestionAsesoria.Operator.Shared.Wrapper;
using GestionAsesoria.Operator.WebApi.Managers.Preferences;
using GestionAsesoria.Operator.WebApi.Services;
using GestionAsesoria.Operator.WebApi.Settings;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace GestionAsesoria.Operator.WebApi.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        //internal static async Task<IStringLocalizer> GetRegisteredServerLocalizerAsync<T>(this IServiceCollection services) where T : class
        //{
        //    var serviceProvider = services.BuildServiceProvider();
        //    await SetCultureFromServerPreferenceAsync(serviceProvider);
        //    var localizer = serviceProvider.GetService<IStringLocalizer<T>>();
        //    await serviceProvider.DisposeAsync();
        //    return localizer!;
        //}


        public static void ConfigureHangfireServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();

            SettingsContainer globalMessageContainer = new SettingsContainer(configuration.GetConnectionString("DefaultConnection"));

            // Agrega la configuración al contenedor global
            LocalSettingContainer.UpdateOrAddSettingContainer(globalMessageContainer);
        }

        internal static IServiceCollection AddForwarding(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationSettingsConfiguration = configuration.GetSection(nameof(AppConfiguration));
            var config = applicationSettingsConfiguration.Get<AppConfiguration>();
            if (config!.BehindSSLProxy)
            {
                services.Configure<ForwardedHeadersOptions>(options =>
                {
                    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                    if (!string.IsNullOrWhiteSpace(config.ProxyIP))
                    {
                        var ipCheck = config.ProxyIP;
                        if (IPAddress.TryParse(ipCheck, out var proxyIP))
                            options.KnownProxies.Add(proxyIP);
                        else
                            Log.Logger.Warning("Invalid Proxy IP of {IpCheck}, Not Loaded", ipCheck);
                    }
                });

                //services.AddCors(options =>
                //{
                //    options.AddDefaultPolicy(
                //        builder =>
                //        {
                //            builder
                //                .AllowCredentials()
                //                .AllowAnyHeader()
                //                .AllowAnyMethod()
                //                .WithOrigins(config.ApplicationUrl.TrimEnd('/'));


                //        });
                //});
            }
            return services;
        }


        private static async Task SetCultureFromServerPreferenceAsync(IServiceProvider serviceProvider)
        {
            var storageService = serviceProvider.GetService<ServerPreferenceManager>();
            if (storageService != null)
            {
                CultureInfo culture;
                if (await storageService.GetPreference() is ServerPreference preference)
                    culture = new(preference.LanguageCode);
                else
                    culture = new(LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "es-ES");
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }
        }
        //internal static IServiceCollection AddServerLocalization(this IServiceCollection services)
        //{
        //    services.TryAddTransient(typeof(IStringLocalizer<>), typeof(ServerLocalizer<>));
        //    return services;
        //}

        internal static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<AppConfiguration>();
            return services;
        }

        internal static AppConfiguration GetApplicationSettings(
           this IServiceCollection services,
           IConfiguration configuration)
        {
            var applicationSettingsConfiguration = configuration.GetSection(nameof(AppConfiguration));
            services.Configure<AppConfiguration>(applicationSettingsConfiguration);
            return applicationSettingsConfiguration.Get<AppConfiguration>()!;
        }

        internal static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(async c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Management Consulting",
                    Description = "ManagementConsulting-DEV Service API 2024",
                    TermsOfService = new Uri("https://opensource.org/licenses/NIT"),
                    Contact = new OpenApiContact
                    {
                        Name = "Management Consulting",
                        Email = "devperu@gmail.com",
                        Url = new Uri("https://devperu.com.pe")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://opensource.org/licenses/")
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            });
        }

        internal static IServiceCollection AddSerialization(this IServiceCollection services)
        {
            services
                .AddScoped<IJsonSerializerOptions, SystemTextJsonOptions>()
                .Configure<SystemTextJsonOptions>(configureOptions =>
                {
                    if (!configureOptions.JsonSerializerOptions.Converters.Any(c => c.GetType() == typeof(TimespanJsonConverter)))
                        configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                });
            services.AddScoped<IJsonSerializerSettings, NewtonsoftJsonSettings>();

            services.AddScoped<IJsonSerializer, SystemTextJsonSerializer>(); // you can change it
            return services;
        }

        internal static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            }

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
            });

            services.AddTransient<IDatabaseSeeder, DatabaseSeeder>();
            return services;
        }

        internal static IServiceCollection AddCurrentUserService(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            return services;
        }

        internal static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<AcademicUser, AcademicRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }

        internal static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.Configure<MailConfiguration>(configuration.GetSection("MailSettings"));
            services.AddTransient<IMailService, SMTPMailService>();
            return services;
        }

        internal static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IRoleClaimService, RoleClaimService>();
            services.AddTransient<ITokenService, IdentityService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFileStorageLocal, FileStorageLocal>();
            services.AddTransient<IUploadService, UploadService>();
            services.AddTransient<IAuditService, AuditService>();
            services.AddTransient<IGenerateExcel, GenerateExcel>();
            services.AddTransient<ISunatReniecService, SunatReniecService>();
            return services;
        }

        internal static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services, AppConfiguration config)
        {
            var key = Encoding.UTF8.GetBytes(config.Secret!);
            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(async bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RoleClaimType = ClaimTypes.Role,
                        ClockSkew = TimeSpan.Zero
                    };

                    bearer.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments(ApplicationConstants.SignalR.HubUrl)))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = c =>
                        {
                            if (c.Exception is SecurityTokenExpiredException)
                            {
                                c.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                c.Response.ContentType = "application/json";
                                var result = JsonConvert.SerializeObject(Result.Fail("The Token is expired."));
                                return c.Response.WriteAsync(result);
                            }
                            else
                            {
#if DEBUG
                                c.NoResult();
                                c.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                c.Response.ContentType = "text/plain";
                                return c.Response.WriteAsync(c.Exception.ToString());
#else
                                c.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                c.Response.ContentType = "application/json";
                                var result = JsonConvert.SerializeObject(Result.Fail("An unhandled error has occurred."));
                                return c.Response.WriteAsync(result);
#endif
                            }
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            if (!context.Response.HasStarted)
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                context.Response.ContentType = "application/json";
                                var result = JsonConvert.SerializeObject(Result.Fail("You are not Authorized."));
                                return context.Response.WriteAsync(result);
                            }

                            return Task.CompletedTask;
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(Result.Fail("You are not authorized to access this resource."));
                            return context.Response.WriteAsync(result);
                        },
                    };
                });
            services.AddAuthorization(options =>
            {
                // Here I stored necessary permissions/roles in a constant
                foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
                {
                    var propertyValue = prop.GetValue(null);
                    if (propertyValue is not null)
                    {
                        options.AddPolicy(propertyValue.ToString()!, policy => policy.RequireClaim(ApplicationClaimTypes.Permission, propertyValue.ToString()!));
                    }
                }
            });
            return services;
        }
    }
}