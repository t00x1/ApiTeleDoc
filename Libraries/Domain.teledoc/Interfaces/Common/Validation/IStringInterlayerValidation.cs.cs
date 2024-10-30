using System.Collections.Generic;
using System.Reflection;

namespace Domain.Common
{
    public interface IStringInterlayerValidation
    {
        public void Validate<T>(T entity) where T : class;
    }
}
