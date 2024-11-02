using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Library.Utils;

public class TestUserInvalidData
{
    private readonly HttpClient _client;

    public TestUserInvalidData()
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
            Email = $"test_{Guid.NewGuid()}example.com"
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
            Phone = Functions.GenerateRandomNumber(1),
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
    [Fact]
    public async Task CreateWithInvalidINN()
    {
        var jsonData = new
        {
            INN = Functions.GenerateRandomNumber(0),
            Type = "1",
            Phone = Functions.GenerateRandomNumber(1),
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