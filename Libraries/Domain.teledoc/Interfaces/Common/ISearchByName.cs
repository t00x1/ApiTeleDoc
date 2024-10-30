using System.Reflection;
namespace Domain.Interfaces.Common
{
    public interface ISearchPropertyByName
    {
        PropertyInfo? Search<T>(T entity, string name) where T : class;
    }
}