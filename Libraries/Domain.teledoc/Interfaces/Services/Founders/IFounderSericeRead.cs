using System.Collections.Generic;
using System.Threading.Tasks;

using Domain.Interfaces.Response;
using Domain.Models;
using Domain.ModelsDTO;

namespace Domain.Interfaces.Services
{
    public interface IFounderServiceRead
    {
        Task<IResponse<Founder>> Read(FounderDto entity);
        Task<IResponse<List<Founder>>> ReadAll();
    }
}
