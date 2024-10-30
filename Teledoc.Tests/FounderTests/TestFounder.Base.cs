using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;

public class FoundersControllerTests
{
    private readonly HttpClient _client;
    private string _createdFounderINN;
    private string _createdClientINN;

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

    public FoundersControllerTests()
    {
        _client = new HttpClient { BaseAddress = new Uri("http://localhost:5110") };
    }

    private async Task<string> CreateClientAndGetINN()
    {
        var jsonData = new
        {
            INN = GenerateRandomNumber(10),
            Type = "1",
            Phone = GenerateRandomNumber(10),
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
        
        Assert.True(response.IsSuccessStatusCode, content);
        
        
        return jsonData.INN;
    }

    [Fact]
    public async Task CreateFounder_ShouldReturnOk()
    {
       
        _createdClientINN = await CreateClientAndGetINN();

        var founderJsonData = new
        {
            INN = GenerateRandomNumber(10),
            Phone = GenerateRandomNumber(10),
            LastName = "Alexandr",
            FirstName = "Spektor",
            Patronymic = "",
            Email = $"founder_{Guid.NewGuid()}@example.com",
            ClientINN = _createdClientINN
        };

        var jsonContent = new StringContent(
            JsonConvert.SerializeObject(founderJsonData),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PostAsync("/Founders/Create", jsonContent);
        var content = await response.Content.ReadAsStringAsync();
        
        Assert.True(response.IsSuccessStatusCode, content);
        

        _createdFounderINN = founderJsonData.INN;
    }

    [Fact]
    public async Task ReadFounder_ShouldReturnOk()
    {
        await CreateFounder_ShouldReturnOk();

        var response = await _client.GetAsync($"/Founders/ReadByCondition?inn={_createdFounderINN}");
        var content = await response.Content.ReadAsStringAsync();
        
        Assert.True(response.IsSuccessStatusCode, content);
    }

    [Fact]
    public async Task UpdateFounder_ShouldReturnOK()
    {

        await CreateFounder_ShouldReturnOk();

        var updatedJsonData = new
        {
            INN = _createdFounderINN,
            Phone = "9234567890",
            LastName = "UpdatedLastName",
            FirstName = "UpdatedFirstName",
            Patronymic = "",
            Email = "UpdatedEmail@gmail.com",
            ClientINN = _createdClientINN 
        };

        var updatedJsonContent = new StringContent(
            JsonConvert.SerializeObject(updatedJsonData),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PutAsync("/Founders/Update", updatedJsonContent);
        var content = await response.Content.ReadAsStringAsync();
        
        Assert.False(response.IsSuccessStatusCode, content);
    }

    [Fact]
    public async Task DeleteAllFounders_ShouldReturnOk()
    {
        var response = await _client.DeleteAsync("/Founders/DeleteAll");
        var content = await response.Content.ReadAsStringAsync();
        
        Assert.True(response.IsSuccessStatusCode, content);
    }
}
