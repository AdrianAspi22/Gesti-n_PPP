using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Application.Interfaces.Serialization.Serializers;
using GestionAsesoria.Operator.Application.Interfaces.Services.Storage;
using GestionAsesoria.Operator.Application.Interfaces.Services.Storage.Provider;
using GestionAsesoria.Operator.Application.Serialization.JsonConverters;
using GestionAsesoria.Operator.Application.Serialization.Options;
using GestionAsesoria.Operator.Application.Serialization.Serializers;
using GestionAsesoria.Operator.Infrastructure.Persistence.Repository;
using GestionAsesoria.Operator.Infrastructure.Repositories;
using GestionAsesoria.Operator.Infrastructure.Services.Storage;
using GestionAsesoria.Operator.Infrastructure.Services.Storage.Provider;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace GestionAsesoria.Operator.Infrastructure.Persistence.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services

                .AddTransient(typeof(IGenericRepositoryAsync<,>), typeof(GenericRepositoryAsync<,>))
                .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }


        public static IServiceCollection AddServerStorage(this IServiceCollection services)
          => AddServerStorage(services, null!);

        public static IServiceCollection AddServerStorage(this IServiceCollection services, Action<SystemTextJsonOptions> configure)
        {
            return services
                .AddScoped<IJsonSerializer, SystemTextJsonSerializer>()
                .AddScoped<IStorageProvider, ServerStorageProvider>()
                .AddScoped<IServerStorageService, ServerStorageService>()
                .AddScoped<ISyncServerStorageService, ServerStorageService>()

                .Configure<SystemTextJsonOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    if (!configureOptions.JsonSerializerOptions.Converters.Any(c => c.GetType() == typeof(TimespanJsonConverter)))
                        configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                });
        }
    }
}