using Domain.Interfaces.Repository;
using DataAccess.Context;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected TSteledocDbContext _context { get; set; }

        public RepositoryBase(TSteledocDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> FindAll() => await _context.Set<T>().AsNoTracking().ToListAsync();

        public async Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression) =>
            await _context.Set<T>().Where(expression).AsNoTracking().ToListAsync();

          public async Task<T?> FindByConditionFirst(Expression<Func<T, bool>> expression) =>
            await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);
        public async Task Create(T entity) => await _context.Set<T>().AddAsync(entity);

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await Task.CompletedTask;
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }
    }
}