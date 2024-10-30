using System.Linq;
using System.Reflection;
using Domain.Interfaces.Common;

namespace Domain.Interfaces.Common
{
    public class SearchNotNullProperty : ISearchNotNullProperty
    {
        public SearchNotNullProperty() 
        {
        }

        public PropertyInfo? Search<T>(T entity) where T : class
        {
            return typeof(T)
                .GetProperties()
                .Where(prop => prop.CanRead)
                .FirstOrDefault(el => el.GetValue(entity) != null);
        }
    }
}
