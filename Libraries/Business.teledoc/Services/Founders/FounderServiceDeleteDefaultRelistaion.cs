using Domain.Response;
using Domain.ModelsDTO;
using Microsoft.Extensions.Logging;
using Domain.Interfaces.Wrapper;
using Domain.Interfaces.Common;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Response;
using Domain.Interfaces.Services;
using Domain.Models;

namespace Business.Services
{
    public class FounderServiceDeleteDefaultRelisation : IFounderServiceDelete
    {
        private readonly IRepositoryWrapper _wrapper;
        private readonly ILogger<FounderServiceDeleteDefaultRelisation> _logger;
        private readonly ISearchNotNullProperty _notNullProperty;

        public FounderServiceDeleteDefaultRelisation(
            IRepositoryWrapper wrapper,
            ILogger<FounderServiceDeleteDefaultRelisation> logger,
            ISearchNotNullProperty notNullProperty)
        {
            _wrapper = wrapper;
            _logger = logger;
            _notNullProperty = notNullProperty;
        }

        private string LogWarning(string message)
        {
            _logger.LogWarning("Delete failed: {Message}", message);
            return message;
        }

        private string LogError(Exception ex, string message)
        {
            _logger.LogError(ex, "Error occurred while deleting founder. Exception: {ExceptionMessage}", ex.Message);
            return message;
        }

        public async Task<IResponse<FounderDto>> Delete(FounderDto entity)
        {
            entity.DateAdded = null;
            entity.DateUpdated = null;
            entity.ClientINN = null;
            var prop = _notNullProperty.Search(entity);
            if (prop == null)
            {
                return new Response<FounderDto>().InvalidInput(LogWarning("Invalid inputs"));
            }

            var value = prop.GetValue(entity)?.ToString();
            if (string.IsNullOrWhiteSpace(value))
            {
                return new Response<FounderDto>().InvalidInput(LogWarning($"Value for property {prop.Name} is null or empty"));
            }

            var founders = await _wrapper.Founder.FindByCondition(x => EF.Property<string>(x, prop.Name) == value);
            var founder = founders.FirstOrDefault();
            if (founder == null)
            {
                return new Response<FounderDto>().NotFound(LogWarning("Founder not found"));
            }

            try
            {
                await _wrapper.Founder.Delete(founder);
                await _wrapper.SaveChangesAsync();
                return new Response<FounderDto>().Success(entity);
            }
            catch (Exception ex)
            {
                return new Response<FounderDto>().Error(LogError(ex, "Error occurred while deleting founder"));
            }
        }
        public async Task<IResponse<List<Founder>>> DeleteAll()
        {
            try
            {
                var founders = await _wrapper.Founder.FindAll();
                Console.WriteLine(founders.Count());
                foreach (var el in founders)
                {
                    await _wrapper.Founder.Delete(el);
                }
                 await _wrapper.SaveChangesAsync();

                return new Response<List<Founder>>().Success(new List<Founder>());
            }
            catch (Exception ex)
            {
                LogError(ex, "Error while deleting all founders.");
                
                return new Response<List<Founder>>().Error("An error occurred while deleting founders.");
            }
        }



    }
}
