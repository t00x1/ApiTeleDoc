using System;
using System.Linq;
using System.Reflection;
using Domain.Interfaces.Common;

namespace Domain.Common
{
    public class AutoMapper : IAutoMapper // я понимаю что на практике лучше использовать Mapster
    {
 
        public void CopyPropertiesTo<TSource, TDest>(TSource source, TDest dest)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (dest == null) throw new ArgumentNullException(nameof(dest));

            var sourceProperties = typeof(TSource).GetProperties()
                .Where(prop => prop.CanRead);

            var destProperties = typeof(TDest).GetProperties()
                .Where(prop => prop.CanWrite);

            foreach (var sourceProp in sourceProperties)
            {
                var destProp = destProperties.FirstOrDefault(p => p.Name == sourceProp.Name && p.PropertyType == sourceProp.PropertyType);
                if (destProp != null)
                {
                    destProp.SetValue(dest, sourceProp.GetValue(source));
                }
            }
        }
    }
}
