using System.Threading.Tasks;
using Domain.ModelsDTO;
using Domain.Interfaces.Response;
using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IFounderServiceDelete
    {
        Task<IResponse<FounderDto>> Delete(FounderDto founderDto);
        Task<IResponse<List<Founder>>> DeleteAll();
    }
}
