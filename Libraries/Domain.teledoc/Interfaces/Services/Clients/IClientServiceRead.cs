using System.Threading.Tasks;
using Domain.ModelsDTO;
using Domain.Interfaces.Response;
using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IClientServiceRead
    {
        Task<IResponse<Client>> Read(ClientDto clientDto);
        Task<IResponse<List<Client>>> ReadAll();
    }

}