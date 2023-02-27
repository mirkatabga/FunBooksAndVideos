using System.Reflection;
using FluentValidation;
using FunBooksAndVideos.Application.Behaviours;
using FunBooksAndVideos.Application.Features.Commands.Orders;
using FunBooksAndVideos.Application.Features.Commands.Orders.CheckoutOrder.CheckoutOrderProcessor;
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
            services.AddScoped<IOrderProcessor, OrderProcessor>();
            services.AddImplementedInterfaces(typeof(IOrderItemsProcessor), ServiceLifetime.Scoped);

            return services;
        }

        public static void AddImplementedInterfaces(this IServiceCollection services, Type interfaceType, ServiceLifetime serviceLifetime)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var implementationTypes = assembly
                .GetTypes()
                .Where(x => !x.IsAbstract && x.IsClass && x.GetInterface(interfaceType.Name) == interfaceType);

            foreach (var implementationType in implementationTypes)
            {
                services.Add(new ServiceDescriptor(interfaceType, implementationType, serviceLifetime));
            }
        }
    }
}