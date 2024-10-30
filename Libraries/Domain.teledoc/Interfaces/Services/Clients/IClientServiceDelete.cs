using System.Threading.Tasks;
using Domain.ModelsDTO;
using Domain.Interfaces.Response;
using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IClientServiceDelete
    {
        Task<IResponse<ClientDto>> Delete(ClientDto clientDto);
        Task<IResponse<List<Client>>>DeleteAll();
    }
}
