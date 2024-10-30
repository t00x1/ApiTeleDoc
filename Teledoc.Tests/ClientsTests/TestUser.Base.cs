using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

public class ClientsControllerTests
{
    private readonly HttpClient _client;
    
    static string GenerateRandomNumber(int length)
    {
        Random random = new Random();
        char[] chars = new char[length];

        for (int i = 0; i < length; i++)
        {
            chars[i] = (char)('0' + random.Next(10));
        }

        return new string(chars);
    }

    public ClientsControllerTests()
    {
        _client = new HttpClient { BaseAddress = new Uri("http://localhost:5110") };
    }

    [Fact]
    public async Task DeleteAllClients_ShouldReturnOk()
    {
        var response = await _client.DeleteAsync("/Clients/DeleteAll");
        Assert.True(response.IsSuccessStatusCode);

        var result = await _client.DeleteAsync("/Clients/DeleteAll");
        Assert.True(result.IsSuccessStatusCode);
    }

    [Fact]
    public async Task CreateClient_ShouldReturnOk()
    {
        var jsonData = new
        {
            INN = GenerateRandomNumber(10),
            Type = "1",
            Phone = GenerateRandomNumber(10),
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
    public async Task ReadClient_ShouldReturnOk()
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
    public async Task ReadAllClients_ShouldReturnOk()
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
    public async Task UpdateClient_ShouldReturnOk()
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
