using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Library.Utils;

public class ClientsControllerTests
{
    private readonly HttpClient _client;

    public ClientsControllerTests()
    {
        _client = new HttpClient { BaseAddress = new Uri("http://localhost:5110") };
    }

    [Fact]
    public async Task DeleteAllClients()
    {
        var response = await _client.DeleteAsync("/Clients/DeleteAll");
        Assert.True(response.IsSuccessStatusCode);

        var result = await _client.DeleteAsync("/Clients/DeleteAll");
        Assert.True(result.IsSuccessStatusCode);
    }


    [Fact]
    public async Task ReadClient()
    {
        var jsonData = new
        {
            INN = "1234567892",
            Type = "1",
            Phone = "1234567892",
            Status = "1",
            Email = "test2@gmagil.com"
        };

        var jsonContent = new StringContent(
            JsonConvert.SerializeObject(jsonData),
            Encoding.UTF8,
            "application/json"
        );

        await _client.PostAsync("/Clients/Create", jsonContent);

        var response = await _client.GetAsync($"/Clients/ReadByCondition?INN={jsonData.INN}");
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task ReadAllClients()
    {
        var jsonData = new
        {
            INN = "1234567892",
            Type = "1",
            Phone = "1234567892",
            Status = "1",
            Email = "test2@gmagil.com"
        };

        var jsonContent = new StringContent(
            JsonConvert.SerializeObject(jsonData),
            Encoding.UTF8,
            "application/json"
        );

        await _client.PostAsync("/Clients/Create", jsonContent);

        var response = await _client.GetAsync("/Clients/ReadAll");
        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task UpdateClient()
    {
        var jsonData = new
        {
            INN = "1234567892",
            Type = "1",
            Phone = "1234567892",
            Status = "1",
            Email = "test2@gmagil.com"
        };

        var jsonContent = new StringContent(
            JsonConvert.SerializeObject(jsonData),
            Encoding.UTF8,
            "application/json"
        );

        await _client.PostAsync("/Clients/Create", jsonContent);

        var updatedJsonData = new
        {
            INN = "1234567892",
            Type = "1",
            Phone = "9234567890",
            Status = "1",
            Email = "UpdatedEmail@gmail.com"
        };

        var updatedJsonContent = new StringContent(
            JsonConvert.SerializeObject(updatedJsonData),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PutAsync("/Clients/Update", updatedJsonContent);
        Assert.True(response.IsSuccessStatusCode);
    }
}