using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Library.Requsets;
using Library.Utils;

public class TestUserCreate
{
    private readonly HttpClient _client;

    public TestUserCreate()
    {
        _client = new HttpClient { BaseAddress = new Uri("http://localhost:5110") };
    }
    [Fact]
    public async Task CreateClient()
    {
        var jsonData = new
        {
            INN = Functions.GenerateRandomNumber(10),
            Type = "1",
            Phone = Functions.GenerateRandomNumber(10),
            Status = "1",
            Email = $"test_{Guid.NewGuid()}@example.com"
        };

        var jsonContent = new StringContent(
            JsonConvert.SerializeObject(jsonData),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PostAsync("/Clients/Create", jsonContent);
        var content = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode, content);
    }
    [Fact]
    public async Task CreateClientWithSameINN()
    {
        string _createdClientINN = await CreateClientRequest.CreateClientAndGetINN();
        var jsonData = new
        {
            INN = _createdClientINN,
            Type = "1",
            Phone = Functions.GenerateRandomNumber(10),
            Status = "1",
            Email = $"test_{Guid.NewGuid()}@example.com"
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
    
    
    
}