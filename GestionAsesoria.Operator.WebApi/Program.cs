using Asp.Versioning;
using GestionAsesoria.Operator.Application.Extensions;
using GestionAsesoria.Operator.Application.Interfaces.Services;
using GestionAsesoria.Operator.Application.Interfaces.Services.ExternalRequest;
using GestionAsesoria.Operator.Infrastructure.Persistence.Configurations;
using GestionAsesoria.Operator.Infrastructure.Persistence.Extensions;
using GestionAsesoria.Operator.Infrastructure.Persistence.Services.ExternalRequest;
using GestionAsesoria.Operator.Infrastructure.Shared.Services;
using GestionAsesoria.Operator.WebApi.Extensions;
using GestionAsesoria.Operator.WebApi.Filters;
using GestionAsesoria.Operator.WebApi.Managers.Preferences;
using GestionAsesoria.Operator.WebApi.Middlewares;
using Google.Apis.Auth.AspNetCore3;
using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var _configuration = builder.Configuration;

    var Cors = "Cors";


    builder.Services.AddHttpClient();

    // Registrar EmailSettings desde appsettings.json
    builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

    builder.Services.AddTransient<IEmailService, EmailService>();

    // Ruta al archivo de configuración JSON
    string configFilePath = Path.Combine(Directory.GetCurrentDirectory(), "config.json");

    // Servicio MessageService con archivo JSON
    builder.Services.AddSingleton<IMessageService>(provider =>
    {
        var env = provider.GetRequiredService<IHostEnvironment>();
        var jsonFilePath = Path.Combine(env.ContentRootPath, "messages.json");

        return new MessageService(
            env,
            provider.GetRequiredService<ILogger<MessageService>>(),
            jsonFilePath
        );
    });

    // Configuración de Google Authentication
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
        options.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddGoogleOpenIdConnect(options =>
    {
        options.ClientId = _configuration["GoogleAuth:ClientId"];
        options.ClientSecret = _configuration["GoogleAuth:ClientSecret"];
        options.CallbackPath = "/signin-google";
    });

    //Configuración de Google Calendar
    builder.Services.Configure<GoogleCalendarOptions>
    (builder.Configuration.GetSection("GoogleCalendarOptions"));
    builder.Services.AddScoped<IGoogleCalendarService, GoogleCalendarService>();

    // Cargar variables de entorno
    builder.Configuration.AddEnvironmentVariables();


    // builder.Services.AddAutoMapper(typeof(ResearchGroupMappingProfile));

    builder.Services.AddForwarding(_configuration);
    builder.Services.AddCurrentUserService();
    builder.Services.AddSerialization();
    builder.Services.AddDatabase(_configuration);
    builder.Services.AddServerStorage();
    builder.Services.AddScoped<ServerPreferenceManager>();
    //builder.Services.AddServerLocalization();
    builder.Services.AddIdentity();
    builder.Services.AddJwtAuthentication(builder.Services.GetApplicationSettings(_configuration));
    builder.Services.AddSignalR();
    builder.Services.AddApplicationLayer();

    builder.Services.AddApplicationServices();
    builder.Services.AddRepositories();
    builder.Services.AddSharedInfrastructure(_configuration);
    builder.Services.AddMemoryCache();

    builder.Services.AddInfrastructureMappings();
    builder.Services.ConfigureHangfireServices(_configuration);
    builder.Services.AddLogging();
    builder.Services.AddControllers().AddValidators();
    builder.Services.AddRazorPages();
    builder.Services.AddApiVersioning(config =>
    {
        config.DefaultApiVersion = new ApiVersion(1, 0);
        config.AssumeDefaultVersionWhenUnspecified = true;
        config.ReportApiVersions = true;
    });
    builder.Services.AddLazyCache();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.RegisterSwagger();
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: Cors,
            builder =>
            {
                builder.WithOrigins("*");
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
                //builder.AllowCredentials(); // Solo si es necesario
            });
    });

    var app = builder.Build();
    app.UseCors(Cors);

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseForwarding(_configuration);
    app.UseHttpsRedirection();
    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.UseStaticFiles();
    //  app.UseRequestLocalizationByCulture();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.UseHangfireDashboard("/jobs", new DashboardOptions
    {
        DashboardTitle = "Academic Consulting",
        Authorization = new[] { new HangfireAuthorizationFilter() }
    });

    app.UseEndpoints();

    app.ConfigureSwagger();
    app.Initialize(_configuration);
    app.Run();
}
catch (Exception ex)
{
    Log.Warning(ex, "An error occurred starting the application");
}
finally
{
    Log.CloseAndFlush();
}
