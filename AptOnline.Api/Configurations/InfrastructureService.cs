using AptOnline.Infrastructure;
using AptOnline.Infrastructure.BillingContext;
using AptOnline.Infrastructure.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Nuna.Lib.AutoNumberHelper;
using Nuna.Lib.CleanArchHelper;
using Nuna.Lib.DataAccessHelper;
using Nuna.Lib.ValidationHelper;
using Scrutor;

namespace AptOnline.Api.Configurations;

public static class InfrastructureService
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services
            .AddScoped<INunaCounterDal, ParamNoDal>()
            .AddScoped<ITglJamProvider, TglJamDal>()
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IMemoryCache, MemoryCache>()
            .AddScoped<INunaCounterDal, ParamNoDal>()
            .AddScoped<INunaCounterDecDal, ParamNoDal>()
            .AddScoped<IRestClientFactory,  RestClientFactory>()
            .AddMemoryCache();

        services
            .Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.SECTION_NAME))
            .Configure<BillingOptions>(configuration.GetSection(BillingOptions.SECTION_NAME))
            .Configure<FarmasiOptions>(configuration.GetSection(FarmasiOptions.SECTION_NAME))
            .Configure<BpjsOptions>(configuration.GetSection(BpjsOptions.SECTION_NAME))
            .Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.SECTION_NAME));
            
        services
            .Scan(selector => selector
                .FromAssemblyOf<InfrastructureAssemblyAnchor>()
                    .AddClasses(c => c.AssignableTo(typeof(IInsert<>)))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()
                .FromAssemblyOf<InfrastructureAssemblyAnchor>()
                    .AddClasses(c => c.AssignableTo(typeof(IUpdate<>)))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()
                .FromAssemblyOf<InfrastructureAssemblyAnchor>()
                    .AddClasses(c => c.AssignableTo(typeof(IDelete<>)))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()
                .FromAssemblyOf<InfrastructureAssemblyAnchor>()
                    .AddClasses(c => c.AssignableTo(typeof(IGetData<,>)))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()
                .FromAssemblyOf<InfrastructureAssemblyAnchor>()
                    .AddClasses(c => c.AssignableTo(typeof(IListData<>)))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()
                .FromAssemblyOf<InfrastructureAssemblyAnchor>()
                    .AddClasses(c => c.AssignableTo(typeof(IListData<,>)))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()
                .FromAssemblyOf<InfrastructureAssemblyAnchor>()
                    .AddClasses(c => c.AssignableTo(typeof(IListData<,,>)))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()
                .FromAssemblyOf<InfrastructureAssemblyAnchor>()
                    .AddClasses(c => c.AssignableTo(typeof(INunaService<,>)))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()
                .FromAssemblyOf<InfrastructureAssemblyAnchor>()
                    .AddClasses(c => c.AssignableTo(typeof(IRequestResponseService<,>)))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()
                .FromAssemblyOf<InfrastructureAssemblyAnchor>()
                    .AddClasses(c => c.AssignableTo(typeof(INunaService<,>)))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()
                .FromAssemblyOf<InfrastructureAssemblyAnchor>()
                    .AddClasses(c => c.AssignableTo(typeof(INunaService<>)))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()
                .FromAssemblyOf<InfrastructureAssemblyAnchor>()
                    .AddClasses(c => c.AssignableTo(typeof(INunaServiceVoid<>)))
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsSelfWithInterfaces()
                    .WithScopedLifetime()
            
            );
        return services;
    }

}