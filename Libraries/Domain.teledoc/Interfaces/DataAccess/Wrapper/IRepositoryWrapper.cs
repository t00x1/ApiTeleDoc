using Domain.Interfaces.Repository;
using Domain.Models;
namespace Domain.Interfaces.Wrapper
{
    public interface IRepositoryWrapper
    {
        IRepositoryFounder Founder {get;}
        IRepositoryClient Client{get;}
        Task SaveChangesAsync();
    }
}