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
    public class FounderServiceCreateDefaultRelisation : IFounderServiceCreate
    {
        private readonly IFounderValidation _founderValidation;
        private readonly IRepositoryWrapper _wrapper;
        private readonly ILogger<ClientServiceCreateDefaultRelisation> _logger;
        private readonly IStringInterlayerValidation _stringInterlayerValidation;
        private readonly IAutoMapper _autoMapper;

        public FounderServiceCreateDefaultRelisation(
            IFounderValidation founderValidation,
            IRepositoryWrapper wrapper,
            ILogger<ClientServiceCreateDefaultRelisation> logger,
            IStringInterlayerValidation stringInterlayerValidation,
            IAutoMapper autoMapper)
        {
            _founderValidation = founderValidation;
            _autoMapper = autoMapper;
            _logger = logger;
            _wrapper = wrapper;
            _stringInterlayerValidation = stringInterlayerValidation;
        }

        private string GetErrorMessage(string message)
        {
            _logger.LogWarning("Creation failed: {Message}", message);
            return message;
        }

        public async Task<IResponse<FounderDto>> Create(FounderDto entity)
        {
            var errors = new List<string>();
            _logger.LogInformation("Starting validation for FounderDto: {@FounderDto}", entity);
            _stringInterlayerValidation.Validate<FounderDto>(entity);
            bool isValid = _founderValidation.Validation(entity, errors);
            if (errors.Any() && !isValid)
            {
                string errorsString = string.Join("\t", errors);
                _logger.LogWarning("Validation failed for FounderDto: {Errors}", errorsString);
                return new Response<FounderDto>().InvalidInput(errorsString);
            }

            Founder founder = new Founder();
            _autoMapper.CopyPropertiesTo<FounderDto, Founder>(entity, founder);
            founder.DateAdded = DateTime.UtcNow;
            founder.DateUpdated = DateTime.UtcNow;

            var clients = await _wrapper.Client.FindByCondition(X => X.INN == entity.ClientINN);
            if (!clients.Any())
            {
                return new Response<FounderDto>().AlreadyExists(GetErrorMessage("Client with the same INN doesn't exist"));
            }

            var client = clients.FirstOrDefault();
            clients = null;

            var founders = await _wrapper.Founder.FindByCondition(X =>
                X.ClientINN == founder.ClientINN ||
                X.INN == founder.INN ||
                X.Email == founder.Email ||
                X.Phone == founder.Phone
            );

            if (client?.Type != ClientType.LegalEntity && founders.Any(X => X.ClientINN == entity.ClientINN))
            {
                return new Response<FounderDto>().InvalidInput(GetErrorMessage("Only LegalEntity can have few founders"));
            }

            if (founders.Any(X => X.INN == founder.INN || X.Email == founder.Email || X.Phone == founder.Phone))
            {
                return new Response<FounderDto>().AlreadyExists(GetErrorMessage("Founder with the same INN, Email, or Phone already exists"));
            }

            await _wrapper.Founder.Create(founder);
            await _wrapper.SaveChangesAsync();
            _logger.LogInformation("Founder created successfully: {@Client}", client);

            return new Response<FounderDto>().Success(entity);
        }
    }
}