using System.Reflection;
using FluentValidation;
using FunBooksAndVideos.Application.Behaviours;
using FunBooksAndVideos.Application.Features.Commands.Orders;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FunBooksAndVideos.Application
{
    public static class DependencyInjection
    {
         public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatR(assembly);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddScoped<ICheckoutOrderCommandConsistencyValidator, CheckoutOrderCommandConsistencyValidator>();

            return services;
        }
    }
}