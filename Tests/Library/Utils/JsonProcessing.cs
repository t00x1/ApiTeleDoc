using System.Text;
using Newtonsoft.Json;
namespace Library.Utils
{
    public static class JsonProcessing
    {
     
        public static StringContent ToStringJsonForBody<T>(T entity) where T : class
        {
            return new StringContent
            (
                JsonConvert.SerializeObject(entity),
                Encoding.UTF8,
                "application/json"
            );
        } 
    }   

}