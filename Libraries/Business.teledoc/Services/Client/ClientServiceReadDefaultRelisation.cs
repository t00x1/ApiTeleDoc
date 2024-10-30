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
    public class ClientServiceReadDefaultRelisation : IClientServiceRead
    {
        private readonly IRepositoryWrapper _wrapper; 
        private readonly ILogger<ClientServiceReadDefaultRelisation> _logger; 
        private readonly IStringInterlayerValidation _stringInterlayerValidation;
        private readonly IClientValidation _clientValidation;
        private readonly ISearchNotNullProperty _notNullProperty;
        private readonly ISearchPropertyByName _searchPropertyByName;

        public ClientServiceReadDefaultRelisation(
            IRepositoryWrapper wrapper,
            ILogger<ClientServiceReadDefaultRelisation> logger,
            IStringInterlayerValidation stringInterlayerValidation,
            IClientValidation clientValidation,
            ISearchNotNullProperty notNullProperty,
            ISearchPropertyByName searchPropertyByName)
        {
            _searchPropertyByName = searchPropertyByName;
            _notNullProperty = notNullProperty;
            _clientValidation = clientValidation;
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

        public async Task<IResponse<Client>> Read(ClientDto entity)
        {
            try
            {
                entity.DateAdded = null;
                entity.DateUpdated = null;
                entity.Status = null;
                entity.Type = null;

                PropertyInfo? prop = _notNullProperty.Search(entity);
                if (prop == null)
                {
                    return new Response<Client>().InvalidInput(GetErrorMessage("Invalid inputs"));
                }

                string name = prop.Name;
                var value = prop.GetValue(entity)?.ToString();

                var clients = await _wrapper.Client.FindByCondition(x => EF.Property<string>(x, name) == value);
                var client = clients.FirstOrDefault();
                if (client == null)
                {
                    return new Response<Client>().NotFound(GetErrorMessage("Client not found"));
                }

                return new Response<Client>().Success(client);
            }
            catch (Exception ex)
            {
                LogError(ex, "Error while reading client.");
                return new Response<Client>().Error("Error while reading client.");
            }
        }

        public async Task<IResponse<List<Client>>> ReadAll()
        {
            try
            {
                var clients = await _wrapper.Client.FindAll();
                if (!clients.Any())
                {
                    return new Response<List<Client>>().NotFound(GetErrorMessage("Clients not found"));
                }

                return new Response<List<Client>>().Success(clients ?? new List<Client>());
            }
            catch (Exception ex)
            {
                LogError(ex, "Error while reading all clients.");
                return new Response<List<Client>>().Error("Error while reading all clients.");
            }
        }
    }
}