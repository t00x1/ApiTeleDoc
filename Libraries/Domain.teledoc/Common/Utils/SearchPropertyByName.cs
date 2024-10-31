using System.Linq;
using System.Reflection;
using Domain.Interfaces.Common;

namespace Domain.General.Common.Utils
{
    public class SearchPropertyByName : ISearchPropertyByName
    {
        public PropertyInfo? Search<T>(T entity, string name) where T : class 
        {
            return typeof(T)
                .GetProperties()
                .Where(prop => prop.CanRead)
                .FirstOrDefault(el => el.Name == name);
        }
    }
}
