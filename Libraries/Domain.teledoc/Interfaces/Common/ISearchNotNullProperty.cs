
using System.Reflection;
namespace Domain.Interfaces.Common

{
    public interface ISearchNotNullProperty
    {
        PropertyInfo? Search<T>(T entity) where T : class;
    }
}