// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Reflection;

// namespace Domain.Common
// {
//     public class ProcessingStringFields : IProcessingStringFields
//     {
//         public List<PropertyInfo> GetStrings<T>(T entity) where T : class
//         {
//             List<PropertyInfo> list = new List<PropertyInfo>();
//             var typedEntity = typeof(T).GetProperties().Where(p => p.CanRead);
//             foreach (var item in typedEntity)
//             {
//                 if (item.PropertyType == typeof(string))
//                 {
//                     list.Add(item);
//                 }
//             }
//             return list;
//         }
//     }
// }
