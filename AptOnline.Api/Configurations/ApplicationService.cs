using AptOnline.Application;
using Nuna.Lib.AutoNumberHelper;
using Nuna.Lib.CleanArchHelper;
using Nuna.Lib.ValidationHelper;
using Scrutor;
using MassTransit;
using MediatR;

namespace AptOnline.Api.Configurations;

public static class ApplicationService
{
    private const string APPLICATION_ASSEMBLY = "Aptol.Application";

    public static IServiceCollection AddApplication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddMediatR(typeof(ApplicationAssemblyAnchor))
            .AddScoped<INunaCounterBL, NunaCounterBL>()
            .AddScoped<DateTimeProvider, DateTimeProvider>();

        services
            .Scan(selector => selector
                .FromAssemblyOf<ApplicationAssemblyAnchor>()
                    .AddClasses(c => c.AssignableTo(typeof(INunaWriter<>)))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()
                .FromAssemblyOf<ApplicationAssemblyAnchor>()
                    .AddClasses(c => c.AssignableTo(typeof(INunaWriterWithReturn<>)))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()
                .FromAssemblyOf<ApplicationAssemblyAnchor>()
                    .AddClasses(c => c.AssignableTo(typeof(INunaBuilder<>)))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()
            );
        
        services
            .AddMassTransit(x =>
            {
                x.AddConsumers(typeof(ApplicationAssemblyAnchor).Assembly);
                x.SetKebabCaseEndpointNameFormatter();

                var rabbitMqOption = configuration.GetSection("RabbitMqOption");
                var server = rabbitMqOption["Server"];
                var userName = rabbitMqOption["UserName"];
                var password = rabbitMqOption["Password"];

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(server, "/", h =>
                    {
                        h.Username(userName ?? string.Empty);
                        h.Password(password ?? string.Empty);
                    }); ;
                    cfg.ConfigureEndpoints(context);

                });
            });

        return services;
    }
}