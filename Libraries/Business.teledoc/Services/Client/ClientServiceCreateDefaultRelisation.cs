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
using Domain.Enums;
using Domain.Interfaces.Response;

namespace Business.Services
{
    public class ClientServiceCreateDefaultRelisation : IClientServiceCreate
    {
        private readonly IRepositoryWrapper _wrapper; 
        private readonly ILogger<ClientServiceCreateDefaultRelisation> _logger; 
        private readonly IStringInterlayerValidation _stringInterlayerValidation;
        private readonly IAutoMapper _autoMapper;
        private readonly IClientValidation _clientValidation;
        private readonly ISearchingClient _seachingClient;

        public ClientServiceCreateDefaultRelisation(
            IRepositoryWrapper wrapper,
            ILogger<ClientServiceCreateDefaultRelisation> logger,
            IStringInterlayerValidation stringInterlayerValidation,
            IAutoMapper autoMapper,
            IClientValidation clientValidation,
            ISearchingClient seachingClient            
            )
        {
            _seachingClient = seachingClient;
            _autoMapper = autoMapper;
            _clientValidation = clientValidation;
            _wrapper = wrapper;
            _logger = logger; 
            _stringInterlayerValidation = stringInterlayerValidation;

        }


        public async Task<IResponse<ClientDto>> Create(ClientDto entity)
        {
           

        
            _stringInterlayerValidation.Validate<ClientDto>(entity);
            var errors = new List<string>();
            if (!_clientValidation.Validation(entity, errors))
            {
                return InvalidInputWithLog(errors);
            }

      
            Client client = new Client();
            _autoMapper.CopyPropertiesTo<ClientDto, Client>(entity, client);
            client.Type = (Domain.Enums.ClientType)(entity?.Type ?? -1);
            client.Status = (Domain.Enums.ClientStatus)(entity?.Status ?? -1);
            client.DateAdded = DateTime.UtcNow;
            client.DateUpdated = DateTime.UtcNow;

            try
            {
                var existingClients = await _seachingClient.GetExistingClients(client);
                if (existingClients.Any())
                {
                    return InvalidInputWithLog("Client with the same INN, Email, or Phone already exists");
                }


                await _wrapper.Client.Create(client);
                await _wrapper.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating client.");
                return new Response<ClientDto>().Error("Error while creating client.");
            }

            return new Response<ClientDto>().Success(entity ?? new ClientDto());
        }

        
        private IResponse<ClientDto> InvalidInputWithLog(object errorDetails)
        {
            var errorMessage = errorDetails is string ? (string)errorDetails : string.Join("\t", (List<string>)errorDetails);
            _logger.LogWarning("Validation failed: {Errors}", errorMessage);
            return new Response<ClientDto>().InvalidInput(errorMessage);
        }

    }
}