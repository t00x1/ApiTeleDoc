using Domain.Interfaces.Services;

namespace Business.Interfaces.Services
{
    public interface IClientServiceContainer
    {
        IClientServiceCreate ClientServiceCreate { get; }
        IClientServiceUpdate ClientServiceUpdate { get; }
        IClientServiceDelete ClientServiceDelete { get; }
        IClientServiceRead ClientServiceRead{ get; }
    }
}
