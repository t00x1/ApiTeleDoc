using Domain.Response; 
using Domain.Models;
using Domain.Interfaces;
using Domain.Common;
using System;
using System.Collections.Generic; 
using Domain.ModelsDTO;
using Microsoft.Extensions.Logging;
using Domain.Interfaces.Services;
using Domain.Interfaces.Wrapper;
using Domain.Interfaces.Repository;
using Domain.Enums;
using Domain.Interfaces.Validation;
using Domain.Validation;
using Domain.Interfaces.Common;
using Domain.Interfaces.Response;

namespace Business.Services
{
    public class FounderServiceUpdateDefaultRelisation : IFounderServiceUpdate
    {
        private readonly IRepositoryWrapper _wrapper; 
        private readonly ILogger<FounderServiceUpdateDefaultRelisation> _logger; 
        private readonly IStringInterlayerValidation _stringInterlayerValidation;
        private readonly IAutoMapper _autoMapper;
        private readonly IFounderValidation _founderValidation;

        public FounderServiceUpdateDefaultRelisation
        (
            IRepositoryWrapper wrapper,
            ILogger<FounderServiceUpdateDefaultRelisation> logger,
            IStringInterlayerValidation stringInterlayerValidation,
            IAutoMapper autoMapper,
            IFounderValidation founderValidation
        )
        {
            _autoMapper = autoMapper;
            _founderValidation = founderValidation;
            _wrapper = wrapper;
            _logger = logger; 
            _stringInterlayerValidation = stringInterlayerValidation;
        }

        public async Task<IResponse<FounderDto>> Update(FounderDto entity)
        {
            entity.DateAdded = null;
            var errors = new List<string>();


            _stringInterlayerValidation.Validate<FounderDto>(entity);
            bool isValid = _founderValidation.Validation(entity, errors);

            if (!isValid || errors.Any())
            {
                string errorsString = string.Join("\t", errors);
                _logger.LogWarning("Validation failed for ClientDto: {Errors}", errorsString);
                return new Response<FounderDto>().InvalidInput(errorsString);
            }

            Founder founder = new Founder();
            _autoMapper.CopyPropertiesTo<FounderDto, Founder>(entity, founder);
            founder.DateUpdated = DateTime.UtcNow;

            var clients = await _wrapper.Client.FindByCondition(X => X.INN == founder.ClientINN);
            if (clients.Count != 1)
            {
                return new Response<FounderDto>().InvalidInput(GetErrorMessage("Client with the same INN doesn't exist or data is not unique"));
            }

            Client client = clients.FirstOrDefault() ?? new Client();
            clients = null;

  
            var founders = await _wrapper.Founder.FindByCondition(X =>
                X.INN == founder.INN ||
                 X.Email == founder.Email || X.Phone == founder.Phone ||
                X.ClientINN == founder.ClientINN
            );
            client.DateAdded = founders.FirstOrDefault()?.DateAdded ?? DateTime.MinValue;
    
            if (founders.Any(X => ( X.Email == founder.Email || X.Phone == founder.Phone) && X.INN != founder.INN))
            {
                return new Response<FounderDto>().AlreadyExists(GetErrorMessage("Founders already exist"));
            }

            if (founders.Count(X => X.INN == founder.INN) != 1)
            {
                return new Response<FounderDto>().NotFound(GetErrorMessage("Founders not found or not unique"));
            }

            if (client.Type != ClientType.LegalEntity && founders.Any(X => (X.ClientINN == founder.ClientINN) && X.INN != founder.INN))
            {
                return new Response<FounderDto>().AlreadyExists(GetErrorMessage("Only LegalEntity can have few founders"));
            }

            await _wrapper.Founder.Update(founder);
            await _wrapper.SaveChangesAsync();
           

            return new Response<FounderDto>().Success(entity);
        }

       

        private string GetErrorMessage(string message)
        {
            _logger.LogWarning("Updating failed: {Message}", message);
            return message;
        }
    }
}