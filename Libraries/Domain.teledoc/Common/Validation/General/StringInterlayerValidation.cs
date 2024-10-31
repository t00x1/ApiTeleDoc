using System;
using System.Linq;
using Domain.Validation; 
using Domain.Interfaces.Validation;

namespace Domain.Common
{
    public class StringInterlayerValidation : IStringInterlayerValidation 
    {
        private readonly IProcessingString _processingString;

        public StringInterlayerValidation(IProcessingString processingString)
        {
            _processingString = processingString ?? throw new ArgumentNullException(nameof(processingString));
        }

        public void Validate<T>(T entity) where T : class
        {
            if(entity == null) throw new ArgumentNullException(nameof(entity));

            var properties = typeof(T).GetProperties(); 
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string) && property.CanWrite )
                {
                    string value = property.GetValue(entity) as string ?? "";
                    _processingString.ClearingString(ref value );
                    property.SetValue(entity, value); 
                }
            }
        }
    }
}
