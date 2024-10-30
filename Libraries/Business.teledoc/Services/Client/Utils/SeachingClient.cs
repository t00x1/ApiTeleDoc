using Domain.Interfaces.Common;
using Domain.Interfaces.Wrapper;
using Domain.Models; 
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces.Services;

namespace Business.Services
{
    public class SearchingClient : ISearchingClient
    {   
        private readonly IRepositoryWrapper _wrapper;
        
        public SearchingClient(IRepositoryWrapper wrapper)
        {
            _wrapper = wrapper;
        }
        
        public async Task<List<Client>> GetExistingClients(Client client)
        {
            var existingClients = await _wrapper.Client.FindByCondition(X => X.INN == client.INN || X.Email == client.Email || X.Phone == client.Phone);
            return existingClients;
        }
    }
}
