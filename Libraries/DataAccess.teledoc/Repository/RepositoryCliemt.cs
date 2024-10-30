using Domain.Interfaces.Repository;
using Domain.Models;
using DataAccess.Context;
namespace DataAccess.Repository
{
    public class RepositoryClient : RepositoryBase<Client>, IRepositoryClient
    {
        public RepositoryClient(TSteledocDbContext context) : base(context){
            
        }
    }
}