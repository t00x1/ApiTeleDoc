using Domain.Interfaces.Response;
using Domain.ModelsDTO;
using Domain.Response;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IFounderServiceCreate
    {
        Task<IResponse<FounderDto>> Create(FounderDto entity);
    }
}