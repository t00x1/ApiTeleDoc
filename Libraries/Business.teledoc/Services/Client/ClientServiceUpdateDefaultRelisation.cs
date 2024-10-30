using System;
using System.Collections.Generic;
using System.Linq;
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
using Domain.Validation;
using Domain.Interfaces.Common;
using Domain.Interfaces.Response;

namespace Business.Services
{
    public class ClientServiceUpdateDefaultRelisation : IClientServiceUpdate
    {
        private readonly IRepositoryWrapper _wrapper; 
        private readonly ILogger<ClientServiceUpdateDefaultRelisation> _logger; 
        private readonly IStringInterlayerValidation _stringInterlayerValidation;
        private readonly IAutoMapper _autoMapper;
        private readonly IClientValidation _clientValidation;
        private readonly ISearchingClient _searchingClient;

        public ClientServiceUpdateDefaultRelisation(
            IRepositoryWrapper wrapper,
            ILogger<ClientServiceUpdateDefaultRelisation> logger,
            IStringInterlayerValidation stringInterlayerValidation,
            IAutoMapper autoMapper,
            IClientValidation clientValidation,
            ISearchingClient searchingClient
            )
        {
            _searchingClient = searchingClient;
            _autoMapper = autoMapper;
            _clientValidation = clientValidation;
            _wrapper = wrapper;
            _logger = logger; 
            _stringInterlayerValidation = stringInterlayerValidation;
        }

    
        private void LogError(Exception ex, string message)
        {
            _logger.LogError(ex, "Error while updating client. Exception: {ExceptionMessage}", ex.Message);
        }

        private IResponse<ClientDto> CreateInvalidResponse(string errorMessage)
        {
            _logger.LogWarning("Update failed: {Message}", errorMessage);
            return new Response<ClientDto>().InvalidInput(errorMessage);
        }

         

        public async Task<IResponse<ClientDto>> Update(ClientDto entity)
        {
            if (entity == null)
            {
                return CreateInvalidResponse("Provided ClientDto entity is null.");
            }

            var errors = new List<string>();
            _logger.LogInformation("Validation started for ClientDto: {@ClientDto}", entity);

            _stringInterlayerValidation.Validate<ClientDto>(entity);
            if (!_clientValidation.Validation(entity, errors))
            {
                string errorsString = string.Join("\t", errors);
                return CreateInvalidResponse(errorsString);
            }

            Client client = new Client();
            _autoMapper.CopyPropertiesTo<ClientDto, Client>(entity, client);
            client.DateUpdated = DateTime.UtcNow;
            client.Type = (Domain.Enums.ClientType)(entity?.Type ?? -1);
            client.Status = (Domain.Enums.ClientStatus)(entity?.Status ?? -1);

            try
            {
                var existingClients = await _searchingClient.GetExistingClients(client);

                if (!existingClients.Any())
                {
                    return CreateInvalidResponse("Update failed: No existing clients found.");
                }
                client.DateAdded = existingClients.FirstOrDefault()?.DateAdded ?? DateTime.MinValue;
    
                if (existingClients.Any(existingClient => 
                    (existingClient.Email == client.Email || existingClient.Phone == client.Phone) && 
                    existingClient.INN != client.INN))
                {
                    return CreateInvalidResponse("Update failed: Client with the same Email or Phone already exists.");
                }

                await _wrapper.Client.Update(client);
                await _wrapper.SaveChangesAsync();
              
            }
            catch (Exception ex)
            {
                LogError(ex, "Error while updating client.");
                return new Response<ClientDto>().Error("Error while updating client.");
            }

            return new Response<ClientDto>().Success(entity);
        }
    }
}
