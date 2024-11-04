using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Library.Utils;
using Library.Requsets;
using Global;
using Domain.ModelsDTO;
public class TestFoundersrCreate
{
    private readonly HttpClient _client;

    public TestFoundersrCreate()
    {
        _client = new HttpClient { BaseAddress = GlobalVariables.URL };
    }

    [Fact]
    public async Task CreateFounder()
    {
        string _createdClientINN = await CreateClientRequest.CreateClientAndGetINN();

        FounderDto founderJsonData = new FounderDto()
        {
            INN = Functions.GenerateRandomNumber(10),
            Phone = Functions.GenerateRandomNumber(10),
            LastName = "Alexandr",
            FirstName = "Spektor",
            Patronymic = "",
            Email = $"founder_{Guid.NewGuid()}@example.com",
            ClientINN = _createdClientINN
        };

        

        var response = await _client.PostAsync("/Founders/Create", JsonProcessing.ToStringJsonForBody<FounderDto>(founderJsonData));
        var content = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode, content);
    }

    [Fact]
    public async Task CreateWithSameINN()
    {
        string INNOfClient =  await CreateClientRequest.CreateClientAndGetINN();
         FounderDto firstFounderJsonData = new FounderDto()
        {
            INN = "1234567890",
            Phone = Functions.GenerateRandomNumber(10),
            LastName = "Alexandr",
            FirstName = "Spektor",
            Patronymic = "",
            Email = $"founder_{Guid.NewGuid()}@example.com",
            ClientINN = INNOfClient
        };

       

        var initialResponse = await _client.PostAsync("/Founders/Create", JsonProcessing.ToStringJsonForBody<FounderDto>(firstFounderJsonData));
        var content = await initialResponse.Content.ReadAsStringAsync();


     

    
        FounderDto secondFounderJsonData = new FounderDto()

        {
            INN = "1234567890",
            Phone = Functions.GenerateRandomNumber(10),
            LastName = "Alexandr",
            FirstName = "Spektor",
            Patronymic = "",
            Email = $"founder_{Guid.NewGuid()}@example.com",
            ClientINN = INNOfClient
        };

        
    
        var response = await _client.PostAsync("/Founders/Create", JsonProcessing.ToStringJsonForBody<FounderDto>(secondFounderJsonData));
        content = await response.Content.ReadAsStringAsync();


        Assert.False(response.IsSuccessStatusCode, content);

    }


    [Fact]
    public async Task CreateFounderButOneToManyClientsWhenType1()
    {
        string _createdClientINN = await CreateClientRequest.CreateClientAndGetINN("1");

        FounderDto firstFounderJsonData = new FounderDto()
        {
            INN = Functions.GenerateRandomNumber(10),
            Phone = Functions.GenerateRandomNumber(10),
            LastName = "Alexandr",
            FirstName = "Spektor",
            Patronymic = "",
            Email = $"founder_{Guid.NewGuid()}@example.com",
            ClientINN = _createdClientINN
        };

        

        var firstResponse = await _client.PostAsync("/Founders/Create", JsonProcessing.ToStringJsonForBody<FounderDto>(firstFounderJsonData));
        var firstContent = await firstResponse.Content.ReadAsStringAsync();

        Assert.True(firstResponse.IsSuccessStatusCode, firstContent);

        FounderDto secondFounderJsonData = new FounderDto()
        {
            INN = Functions.GenerateRandomNumber(10),
            Phone = Functions.GenerateRandomNumber(10),
            LastName = "Ivanov",
            FirstName = "Ivan",
            Patronymic = "Petrovich",
            Email = $"founder_{Guid.NewGuid()}@example.com",
            ClientINN = _createdClientINN
        };

       

        var secondResponse = await _client.PostAsync("/Founders/Create", JsonProcessing.ToStringJsonForBody<FounderDto>(secondFounderJsonData));
        var secondContent = await secondResponse.Content.ReadAsStringAsync();

        Assert.False(secondResponse.IsSuccessStatusCode, secondContent);
    }
}
