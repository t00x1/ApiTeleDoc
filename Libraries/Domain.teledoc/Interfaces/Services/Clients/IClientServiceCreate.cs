using Domain.ModelsDTO;
using Domain.Response;
using  Domain.Interfaces.Response;
namespace Domain.Interfaces.Services
{
    public interface IClientServiceCreate
    {
         Task<IResponse<ClientDto>> Create(ClientDto entity);
    }
}