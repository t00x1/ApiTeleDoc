using Microsoft.Extensions.DependencyInjection;
using Domain.Interfaces.Validation;
using Domain.Interfaces.Services;
using DataAccess.Wrapper;
using Domain.Interfaces.Wrapper;
using Business.Services;
using Domain.Common;
using Domain.General.Validation;
using Domain.Interfaces.Common;
using Business.Interfaces.Services;
using Domain.General.Common.Utils;
using Domain.Validation.CertainValidation;

namespace APIdep
{
    public static class ServiceExtensions
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            AddValidationDependencies(services);
            AddUtilityDependencies(services);
            AddBusinessServiceDependencies(services);
            AddRepositoryDependencies(services);
        }

        private static void AddValidationDependencies(IServiceCollection services)
        {
            services.AddSingleton<IStringInterlayerValidation, StringInterlayerValidation>();
            services.AddSingleton<IClientValidation, ClientValidation>();
            services.AddSingleton<IFounderValidation, FounderValidation>();
        }

        private static void AddUtilityDependencies(IServiceCollection services)
        {
            services.AddSingleton<IProcessingString, ProcessingString>();
            services.AddSingleton<IAutoMapper, AutoMapper>();
            services.AddSingleton<ISearchNotNullProperty, SearchNotNullProperty>();
            services.AddSingleton<ISearchPropertyByName, SearchPropertyByName>();
        }

        private static void AddBusinessServiceDependencies(IServiceCollection services)
        {

            services.AddTransient<IClientServiceCreate, ClientServiceCreateDefaultRelisation>();
            services.AddTransient<IClientServiceUpdate, ClientServiceUpdateDefaultRelisation>();
            services.AddTransient<IClientServiceDelete, ClientServiceDeleteDefaultRelisation>();
            services.AddTransient<IClientServiceRead, ClientServiceReadDefaultRelisation>();
            
            services.AddTransient<IFounderServiceCreate, FounderServiceCreateDefaultRelisation>();
            services.AddTransient<IFounderServiceUpdate, FounderServiceUpdateDefaultRelisation>();
            services.AddTransient<IFounderServiceDelete, FounderServiceDeleteDefaultRelisation>();
            services.AddTransient<IFounderServiceRead, FounderServiceReadDefaultRelisation>();


            services.AddTransient<ISearchingClient, SearchingClient>();
            services.AddScoped<IClientServiceContainer, ClientServiceContainer>();
            services.AddScoped<IFounderServiceContainer, FounderServiceContainer>();
        }

        private static void AddRepositoryDependencies(IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
