using Library.Utils;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Library.Requsets
{
    public static class CreateClientRequest
    {
        static HttpClient _client = new HttpClient { BaseAddress = new Uri("http://localhost:5110") };
        static public async Task<string> CreateClientAndGetINN(string type = "0")
        {
            var jsonData = new
            {
                INN = Functions.GenerateRandomNumber(10),
                Type = type,
                Phone = Functions.GenerateRandomNumber(10),
                Status = "1",
                Email = $"client_{Guid.NewGuid()}@example.com"
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(jsonData),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync("/Clients/Create", jsonContent);
            var content = await response.Content.ReadAsStringAsync();
            
          
            
            
            return jsonData.INN;
        }
    }
}