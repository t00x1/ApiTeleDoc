using System.Threading.Tasks;
using Domain.Interfaces.Response;
using Domain.ModelsDTO;

namespace Domain.Interfaces.Services
{
    public interface IFounderServiceUpdate
    {
        Task<IResponse<FounderDto>> Update(FounderDto entity);
    }
}