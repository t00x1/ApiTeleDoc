using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Library.Utils;
using Library.Requsets;

public class TestFoundersrCreate
{
    private readonly HttpClient _client;

    public TestFoundersrCreate()
    {
        _client = new HttpClient { BaseAddress = new Uri("http://localhost:5110") };
    }

    [Fact]
    public async Task CreateFounder()
    {
        string _createdClientINN = await CreateClientRequest.CreateClientAndGetINN();

        var founderJsonData = new
        {
            INN = Functions.GenerateRandomNumber(10),
            Phone = Functions.GenerateRandomNumber(10),
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
    }

    [Fact]
    public async Task CreateWithSameINN()
    {
        string INNOfUser = Functions.GenerateRandomNumber(10);
        var initialClientJson = new
        {
            INN = INNOfUser,
            Type = "1",
            Phone = Functions.GenerateRandomNumber(10),
            Status = "1",
            Email = $"client_{Guid.NewGuid()}@example.com"
        };

        var initialClientContent = new StringContent(
            JsonConvert.SerializeObject(initialClientJson),
            Encoding.UTF8,
            "application/json"
        );


        var initialResponse = await _client.PostAsync("/Clients/Create", initialClientContent);
        initialResponse.EnsureSuccessStatusCode(); 


        var clientINN = await CreateClientRequest.CreateClientAndGetINN();

    
        var jsonData = new
        {
            INN = clientINN,
            Type = "1",
            Phone = Functions.GenerateRandomNumber(10),
            Status = "1",
            Email = $"client_{Guid.NewGuid()}@example.com",
            ClientINN = clientINN
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
    public async Task CreateFounderButOneToManyClientsWhenType1()
    {
        string _createdClientINN = await CreateClientRequest.CreateClientAndGetINN("1");

        var firstFounderJsonData = new
        {
            INN = Functions.GenerateRandomNumber(10),
            Phone = Functions.GenerateRandomNumber(10),
            LastName = "Alexandr",
            FirstName = "Spektor",
            Patronymic = "",
            Email = $"founder_{Guid.NewGuid()}@example.com",
            ClientINN = _createdClientINN
        };

        var firstJsonContent = new StringContent(
            JsonConvert.SerializeObject(firstFounderJsonData),
            Encoding.UTF8,
            "application/json"
        );

        var firstResponse = await _client.PostAsync("/Founders/Create", firstJsonContent);
        var firstContent = await firstResponse.Content.ReadAsStringAsync();

        Assert.True(firstResponse.IsSuccessStatusCode, firstContent);

        var secondFounderJsonData = new
        {
            INN = Functions.GenerateRandomNumber(10),
            Phone = Functions.GenerateRandomNumber(10),
            LastName = "Ivanov",
            FirstName = "Ivan",
            Patronymic = "Petrovich",
            Email = $"founder_{Guid.NewGuid()}@example.com",
            ClientINN = _createdClientINN
        };

        var secondJsonContent = new StringContent(
            JsonConvert.SerializeObject(secondFounderJsonData),
            Encoding.UTF8,
            "application/json"
        );

        var secondResponse = await _client.PostAsync("/Founders/Create", secondJsonContent);
        var secondContent = await secondResponse.Content.ReadAsStringAsync();

        Assert.False(secondResponse.IsSuccessStatusCode, secondContent);
    }
}
