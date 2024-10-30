using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces.Validation;
using Domain.Interfaces.Services;
using DataAccess.Wrapper;
using Domain.Interfaces.Wrapper;
using Business.Services;
using Domain.Common;
using Domain.Validation;
using Domain.Interfaces.Common;
using Business.Interfaces.Services;

namespace APIdep
{
    public static class ServiceExtensions
    {
        public static void AddSingletonDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IStringInterlayerValidation, StringInterlayerValidation>();
            services.AddSingleton<IProcessingString, ProcessingString>();
            services.AddSingleton<IClientValidation, ClientValidation>();
            services.AddSingleton<IFounderValidation, FounderValidation>();
            services.AddSingleton<IAutoMapper, AutoMapper>();
            services.AddSingleton<ISearchNotNullProperty, SearchNotNullProperty>();
            services.AddSingleton<ISearchPropertyByName, SearchPropertyByName>();
        }

        public static void AddScopedDependencies(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IClientServiceContainer, ClientServiceContainer>();
            services.AddScoped<IFounderServiceContainer, FounderServiceContainer>();
        }

        public static void AddTransientDependencies(this IServiceCollection services)
        {
            services.AddTransient<IClientServiceCreate, ClientServiceCreateDefaultRelisation>();
            services.AddTransient<IClientServiceUpdate, ClientServiceUpdateDefaultRelisation>();
            services.AddTransient<ISearchingClient, SearchingClient>();
            services.AddTransient<IFounderServiceCreate, FounderServiceCreateDefaultRelisation>();
            services.AddTransient<IClientServiceDelete, ClientServiceDeleteDefaultRelisation>();
            services.AddTransient<IClientServiceRead, ClientServiceReadDefaultRelisation>();
            services.AddTransient<IFounderServiceDelete, FounderServiceDeleteDefaultRelisation>();
            services.AddTransient<IFounderServiceRead, FounderServiceReadDefaultRelisation>();
            services.AddTransient<IFounderServiceUpdate, FounderServiceUpdateDefaultRelisation>();
        }
    }
}
