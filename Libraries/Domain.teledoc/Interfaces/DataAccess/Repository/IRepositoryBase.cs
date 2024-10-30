using System.Linq.Expressions;

namespace Domain.Interfaces.Repository
{
    public interface IRepositoryBase<T> where T : class 
    {
    
        Task<List<T>> FindAll();
        Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}