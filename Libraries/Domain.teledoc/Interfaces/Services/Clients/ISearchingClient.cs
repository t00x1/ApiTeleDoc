using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ISearchingClient
    {
        Task<List<Client>> GetExistingClients(Client client);
    }
}
