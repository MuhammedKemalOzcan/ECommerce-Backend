// Application/ServiceRegistration.cs veya Program.cs
using ECommerceAPI.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // MediatR ekle
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(assembly);

            // Validation behavior'ı pipeline'a ekle
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        // Tüm validator'ları otomatik kaydet
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
