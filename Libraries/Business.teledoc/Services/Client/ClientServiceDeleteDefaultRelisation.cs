using System;
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
using Domain.Interfaces.Response;
using Domain.Interfaces.Common;

namespace Business.Services
{
    public class ClientServiceDeleteDefaultRelisation : IClientServiceDelete
    {
        private readonly IRepositoryWrapper _wrapper; 
        private readonly ILogger<ClientServiceDeleteDefaultRelisation> _logger; 
        private readonly IStringInterlayerValidation _stringInterlayerValidation;
        private readonly IClientValidation _clientValidation;
        private readonly ISearchNotNullProperty _notNullProperty;

        public ClientServiceDeleteDefaultRelisation(
            IRepositoryWrapper wrapper,
            ILogger<ClientServiceDeleteDefaultRelisation> logger,
            IStringInterlayerValidation stringInterlayerValidation,
            IClientValidation clientValidation,
            ISearchNotNullProperty notNullProperty)
        {
            _wrapper = wrapper;
            _logger = logger; 
            _stringInterlayerValidation = stringInterlayerValidation;
            _clientValidation = clientValidation;
            _notNullProperty = notNullProperty;
        }

        private string LogWarning(string message)
        {
            _logger.LogWarning("Delete failed: {Message}", message);
            return message;
        }

        private string LogError(Exception ex, string message)
        {
            _logger.LogError(ex, "Error occurred while deleting client. Exception: {ExceptionMessage}", ex.Message);
            return message;
        }

        public async Task<IResponse<ClientDto>> Delete(ClientDto entity)
        {
                entity.DateAdded = null;
                entity.DateUpdated = null;
                entity.Status = null;
                entity.Type = null;
            var prop = _notNullProperty.Search(entity);
            if (prop == null)
            {
                return new Response<ClientDto>().InvalidInput(LogWarning("Invalid inputs"));
            }

            var value = prop.GetValue(entity)?.ToString();
            if (string.IsNullOrWhiteSpace(value))
            {
                return new Response<ClientDto>().InvalidInput(LogWarning($"Value for property {prop.Name} is null or empty"));
            }

            var clients = await _wrapper.Client.FindByCondition(x => EF.Property<string>(x, prop.Name) == value);
            var client = clients.FirstOrDefault();
            if (client == null)
            {
                return new Response<ClientDto>().NotFound(LogWarning("Client not found"));
            }

            try
            {
                await _wrapper.Client.Delete(client);
                await _wrapper.SaveChangesAsync();
                return new Response<ClientDto>().Success(entity);
            }
            catch (Exception ex)
            {
                return new Response<ClientDto>().Error(LogError(ex, "Error occurred while deleting client"));
            }
        }
         public async Task<IResponse<List<Client>>> DeleteAll()
        {
            try{
                var clients = await _wrapper.Client.FindAll();
                
                foreach (var el in clients)
                {
                    await _wrapper.Client.Delete(el);
                }
                await _wrapper.SaveChangesAsync();
                return new Response<List<Client>>().Success(new List<Client>());  
            }
            catch (Exception ex)
            {
                LogError(ex, "Error while deleting all clients.");
                
                return new Response<List<Client>>().Error("An error occurred while deleting clients.");
            }
        }

    }
}
