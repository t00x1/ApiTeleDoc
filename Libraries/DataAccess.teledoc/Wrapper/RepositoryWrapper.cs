using DataAccess.Context;
using DataAccess.Repository;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Wrapper;


namespace DataAccess.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly TSteledocDbContext _context;
        private IRepositoryFounder? _founderRepository;
  
        private IRepositoryClient? _client;

        public RepositoryWrapper(TSteledocDbContext context)
        {
            _context = context;
        }

        public IRepositoryFounder Founder
        {
            get
            {
                if (_founderRepository == null)
                {
                    _founderRepository = new FounderRepository(_context);
                }
                return _founderRepository;
            }
        }

        

        public IRepositoryClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new RepositoryClient(_context);
                }
                return _client;
            }
        }

       
        public async  Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
