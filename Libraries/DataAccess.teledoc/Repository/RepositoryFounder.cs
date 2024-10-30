using Domain.Interfaces.Repository;
using Domain.Models;
using DataAccess.Context;
namespace DataAccess.Repository
{
    public class FounderRepository : RepositoryBase<Founder>, IRepositoryFounder
    {
         public FounderRepository(TSteledocDbContext context) : base(context){
            
        }
    }
}