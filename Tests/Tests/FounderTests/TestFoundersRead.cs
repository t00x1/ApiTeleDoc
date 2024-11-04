using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Library.Utils;
using Newtonsoft.Json;
using Library.Requsets;
using Global;

public class TestFoundersRead
{
    private readonly HttpClient _client;

    public TestFoundersRead()
    {
        _client = new HttpClient { BaseAddress = GlobalVariables.URL };
    }

    [Fact]
    public async Task ReadFounder_ShouldReturnOk()
    {   
      
        string createdClientINN = await CreateClientRequest.CreateClientAndGetINN();
        string inn = Functions.GenerateRandomNumber(10);
        var founderJsonData = new
        {
            INN = inn,
            Phone = Functions.GenerateRandomNumber(10),
            LastName = "Alexandr",
            FirstName = "Spektor",
            Patronymic = "",
            Email = $"founder_{Guid.NewGuid()}@example.com",
            ClientINN = createdClientINN
        };
        var jsonContent = new StringContent(
            JsonConvert.SerializeObject(founderJsonData),
            Encoding.UTF8,
            "application/json"
        );

        var createResponse = await _client.PostAsync("/Founders/Create", jsonContent);
        var createContent = await createResponse.Content.ReadAsStringAsync();
        Assert.True(createResponse.IsSuccessStatusCode, createContent);
        var readResponse = await _client.GetAsync($"/Founders/ReadByCondition?inn={inn}");
        var readContent = await readResponse.Content.ReadAsStringAsync();
        Assert.True(readResponse.IsSuccessStatusCode, readContent);
    }

    [Fact]
    public async Task ReadFounderAll_ShouldReturnOk()
    {
        string createdClientINN = await CreateClientRequest.CreateClientAndGetINN();
        string inn = Functions.GenerateRandomNumber(10);
        var founderJsonData = new
        {
            INN = inn,
            Phone = Functions.GenerateRandomNumber(10),
            LastName = "Alexandr",
            FirstName = "Spektor",
            Patronymic = "",
            Email = $"founder_{Guid.NewGuid()}@example.com",
            ClientINN = createdClientINN
        };
        var jsonContent = new StringContent(
            JsonConvert.SerializeObject(founderJsonData),
            Encoding.UTF8,
            "application/json"
        );

        var createResponse = await _client.PostAsync("/Founders/Create", jsonContent);
        var response = await _client.GetAsync("/Founders/ReadAll");
        var content = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode, content);
    }
}
