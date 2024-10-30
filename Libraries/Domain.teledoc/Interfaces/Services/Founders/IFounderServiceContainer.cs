

namespace Domain.Interfaces.Services
{
    public interface IFounderServiceContainer
    {
        IFounderServiceCreate FounderServiceCreate { get; }
        IFounderServiceUpdate FounderServiceUpdate { get; }
        IFounderServiceDelete FounderServiceDelete { get; }
        IFounderServiceRead FounderServiceRead { get; }
    }
}