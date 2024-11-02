using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Library.Utils;
using Library.Requsets;
public class TestFoundersInvalidData
{
    private readonly HttpClient _client;


    

    public TestFoundersInvalidData()
    {
        _client = new HttpClient { BaseAddress = new Uri("http://localhost:5110") };
    }

    
        [Fact]
        public async Task CreateWithInvalidEmail()
        {
           
            var jsonData = new
            {
                INN = Functions.GenerateRandomNumber(10),
                Type = "1",
                Phone = Functions.GenerateRandomNumber(10),
                Status = "1",
                Email = $"client_{Guid.NewGuid()}example.com",
                ClientINN = await CreateClientRequest.CreateClientAndGetINN()
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(jsonData),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync("/Clients/Create", jsonContent);
            var content = await response.Content.ReadAsStringAsync();
            
            Assert.False(response.IsSuccessStatusCode, content);
            
            
        
        }
        [Fact]
        public async Task CreateWithInvalidPhone()
        {
           
            var jsonData = new
            {
                INN = Functions.GenerateRandomNumber(10),
                Type = "1",
                Phone = Functions.GenerateRandomNumber(9),
                Status = "1",
                Email = $"client_{Guid.NewGuid()}@example.com",
                ClientINN = await CreateClientRequest.CreateClientAndGetINN()
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(jsonData),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync("/Clients/Create", jsonContent);
            var content = await response.Content.ReadAsStringAsync();
            
            Assert.False(response.IsSuccessStatusCode, content);
        
        }
        [Fact]
        public async Task CreateWithInvalidINN()
        {
            
            var jsonData = new
            {
                INN = "1",
                Type = "1",
                Phone = Functions.GenerateRandomNumber(10),
                Status = "1",
                Email = $"client_{Guid.NewGuid()}@example.com",
                ClientINN = await CreateClientRequest.CreateClientAndGetINN()
            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(jsonData),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PostAsync("/Clients/Create", jsonContent);
            var content = await response.Content.ReadAsStringAsync();
            
            Assert.False(response.IsSuccessStatusCode, content);
            
            
        
        }
        [Fact]
        public async Task UpdateWithInvalidEmail()
        {
             
           
            var jsonData = new
            {
                INN = Functions.GenerateRandomNumber(10),
                Type = "1",
                Phone = Functions.GenerateRandomNumber(10),
                Status = "1",
                Email = $"client_{Guid.NewGuid()}example.com",
                ClientINN = await CreateClientRequest.CreateClientAndGetINN()

            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(jsonData),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PutAsync("/Clients/Update", jsonContent);
            var content = await response.Content.ReadAsStringAsync();
            
            Assert.False(response.IsSuccessStatusCode, content);
            
            
        
        }
        [Fact]
        public async Task UpdateWithInvalidPhone()
        {
             
           
            var jsonData = new
            {
                INN = Functions.GenerateRandomNumber(10),
                Type = "1",
                Phone = Functions.GenerateRandomNumber(1),
                Status = "1",
                Email = $"client_{Guid.NewGuid()}@example.com",
                ClientINN = await CreateClientRequest.CreateClientAndGetINN()

            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(jsonData),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PutAsync("/Clients/Update", jsonContent);
            var content = await response.Content.ReadAsStringAsync();
            
            Assert.False(response.IsSuccessStatusCode, content);
            
            
        
        }
        [Fact]
        public async Task UpdateWithInvalidINN()
        {
             
           
            var jsonData = new
            {
                INN = Functions.GenerateRandomNumber(1),
                Type = "1",
                Phone = Functions.GenerateRandomNumber(10),
                Status = "1",
                Email = $"client_{Guid.NewGuid()}@example.com",
                ClientINN = await CreateClientRequest.CreateClientAndGetINN()

            };

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(jsonData),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _client.PutAsync("/Clients/Update", jsonContent);
            var content = await response.Content.ReadAsStringAsync();
            
            Assert.False(response.IsSuccessStatusCode, content);
            
            
        
        }
    
    
}