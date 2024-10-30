using Domain.ModelsDTO;
using Domain.Response;
using  Domain.Interfaces.Response;
namespace Domain.Interfaces.Services
{
    public interface IClientServiceUpdate{

        Task<IResponse<ClientDto>> Update(ClientDto entity);
    }
}