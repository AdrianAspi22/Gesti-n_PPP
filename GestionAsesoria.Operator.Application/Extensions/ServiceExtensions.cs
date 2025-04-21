using FluentValidation;
using GestionAsesoria.Operator.Application.Helpers;
using GestionAsesoria.Operator.Application.Interfaces;
using GestionAsesoria.Operator.Application.Validators.Features.Files;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GestionAsesoria.Operator.Application.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped<IModelHelper, ModelHelper>();
            services.AddScoped<IFileUploadValid, FileUploadValid>();
        }
    }
}