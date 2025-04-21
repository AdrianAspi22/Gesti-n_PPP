using FluentValidation.AspNetCore;
using GestionAsesoria.Operator.Application.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace GestionAsesoria.Operator.WebApi.Extensions
{
    internal static class MvcBuilderExtensions
    {
        internal static IMvcBuilder AddValidators(this IMvcBuilder builder)
        {
            builder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AppConfiguration>());
            return builder;
        }


    }
}