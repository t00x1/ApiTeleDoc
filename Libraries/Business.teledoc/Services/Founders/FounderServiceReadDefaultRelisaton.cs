using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Domain.Response;
using Domain.Models;
using Domain.Interfaces;
using Domain.Common;
using Domain.ModelsDTO;
using Microsoft.Extensions.Logging;
using Domain.Interfaces.Services;
using Domain.Interfaces.Wrapper;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Validation;
using Microsoft.EntityFrameworkCore;
using Domain.Validation;
using Domain.Interfaces.Common;
using Domain.Enums;
using Domain.Interfaces.Response;

namespace Business.Services
{
    public class FounderServiceReadDefaultRelisation : IFounderServiceRead
    {
        private readonly IRepositoryWrapper _wrapper; 
        private readonly ILogger<FounderServiceReadDefaultRelisation> _logger; 
        private readonly IStringInterlayerValidation _stringInterlayerValidation;
        private readonly IFounderValidation _founderValidation;
        private readonly ISearchNotNullProperty _notNullProperty;
        private readonly ISearchPropertyByName _searchPropertyByName;

        public FounderServiceReadDefaultRelisation(
            IRepositoryWrapper wrapper,
            ILogger<FounderServiceReadDefaultRelisation> logger,
            IStringInterlayerValidation stringInterlayerValidation,
            IFounderValidation founderValidation,
            ISearchNotNullProperty notNullProperty,
            ISearchPropertyByName searchPropertyByName)
        {
            _searchPropertyByName = searchPropertyByName;
            _notNullProperty = notNullProperty;
            _founderValidation = founderValidation;
            _wrapper = wrapper;
            _logger = logger; 
            _stringInterlayerValidation = stringInterlayerValidation;
        }

        private string GetErrorMessage(string message)
        {
            _logger.LogWarning("Read failed: {Message}", message);
            return message;
        }

        private void LogError(Exception ex, string message)
        {
            _logger.LogError(ex, "Error while processing client. Exception: {ExceptionMessage}", ex.Message);
        }

        public async Task<IResponse<Founder>> Read(FounderDto entity)
        {
            try
            {
                entity.DateAdded = null;
                entity.DateUpdated = null;
                entity.ClientINN = null;
                PropertyInfo? prop = _notNullProperty.Search(entity);
                if (prop == null)
                {
                    return new Response<Founder>().InvalidInput(GetErrorMessage("Invalid inputs"));
                }
                string name = prop.Name;
                var value = prop.GetValue(entity)?.ToString();

                var founders = await _wrapper.Founder.FindByCondition(x => EF.Property<string>(x, name) == value);
                var founder = founders.FirstOrDefault();
                if (founder == null)
                {
                    return new Response<Founder>().NotFound(GetErrorMessage("Founders not found"));
                }

                return new Response<Founder>().Success(founder);
            }
            catch (Exception ex)
            {
                LogError(ex, "Error while reading founders.");
                return new Response<Founder>().Error("Error while reading founders.");
            }
        }

        public async Task<IResponse<List<Founder>>> ReadAll()
        {
            try
            {
                var founders = await _wrapper.Founder.FindAll();
                if (!founders.Any())
                {
                    return new Response<List<Founder>>().NotFound(GetErrorMessage("founders not found"));
                }

                return new Response<List<Founder>>().Success(founders ?? new List<Founder>());
            }
            catch (Exception ex)
            {
                LogError(ex, "Error while reading all founders.");
                return new Response<List<Founder>>().Error("Error while reading all founders.");
            }
        }
    }
}