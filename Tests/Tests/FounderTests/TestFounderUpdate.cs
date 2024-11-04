using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Library.Utils;
using Library.Requsets;
using Global;
using Domain.ModelsDTO;

public class TestFounderUpdate
{
    private readonly HttpClient _client;

    public TestFounderUpdate()
    {
         _client = new HttpClient { BaseAddress = GlobalVariables.URL };
    }

    [Fact]
    public async Task UpdateFounder_ShouldReturnOK()
    {
        string createdClientINN = await CreateClientRequest.CreateClientAndGetINN("1");
        FounderDto firstFounderJsonData = new FounderDto()
        {
            INN = Functions.GenerateRandomNumber(10),
            Phone = Functions.GenerateRandomNumber(10),
            LastName = "Alexandr",
            FirstName = "Spektor",
            Patronymic = "",
            Email = $"founder_{Guid.NewGuid()}@example.com",
            ClientINN = createdClientINN
        };

       
        var firstResponse = await _client.PostAsync("/Founders/Create", JsonProcessing.ToStringJsonForBody<FounderDto>(firstFounderJsonData));
        var firstContent = await firstResponse.Content.ReadAsStringAsync();

        Assert.True(firstResponse.IsSuccessStatusCode, firstContent);

        FounderDto updatedJsonData = new FounderDto()
        {
            INN = firstFounderJsonData.INN,
            Phone = "9234567890",
            LastName = "UpdatedLastName",
            FirstName = "UpdatedFirstName",
            Patronymic = "",
            Email = "UpdatedEmail@gmail.com",
            ClientINN = createdClientINN
        };

        
        var response = await _client.PutAsync("/Founders/Update", JsonProcessing.ToStringJsonForBody<FounderDto>(updatedJsonData));
        var content = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode, content);
    }

    [Fact]
    public async Task UpdateFounder_ButNotExistClient()
    {
        FounderDto updatedJsonData = new FounderDto()
        {
            INN = "1111111111",
            Phone = "9234567890",
            LastName = "UpdatedLastName",
            FirstName = "UpdatedFirstName",
            Patronymic = "",
            Email = "UpdatedEmail@gmail.com",
            ClientINN = "21312312313123131312312"
        };

    

        var response = await _client.PutAsync("/Founders/Update", JsonProcessing.ToStringJsonForBody<FounderDto>(updatedJsonData));
        var content = await response.Content.ReadAsStringAsync();

        Assert.False(response.IsSuccessStatusCode, content);
    }
    [Fact]
    public async Task UpdateFounderButOneToManyClientsWhenType1()
    {

        string createdClientINN = await CreateClientRequest.CreateClientAndGetINN("1");
        FounderDto firstFounderJsonData = new FounderDto()
        {
            INN = Functions.GenerateRandomNumber(10),
            Phone = Functions.GenerateRandomNumber(10),
            LastName = "Alexandr",
            FirstName = "Spektor",
            Patronymic = "",
            Email = $"founder_{Guid.NewGuid()}@example.com",
            ClientINN = createdClientINN
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
            ClientINN = await CreateClientRequest.CreateClientAndGetINN("0")
        };

        

 
        var secondResponse = await _client.PostAsync("/Founders/Create", JsonProcessing.ToStringJsonForBody<FounderDto>(secondFounderJsonData));
        var secondContent = await secondResponse.Content.ReadAsStringAsync();

        Assert.True(secondResponse.IsSuccessStatusCode, secondContent);

        FounderDto updatedSecondFounderJsonData = new FounderDto()
        {
            INN = secondFounderJsonData.INN,
            Phone = secondFounderJsonData.Phone,
            LastName =  secondFounderJsonData.LastName,
            FirstName =  secondFounderJsonData.FirstName,
            Patronymic = "",
            Email =  secondFounderJsonData.Email,
            ClientINN = createdClientINN
        };

       
        var updateFirstResponse = await _client.PutAsync("/Founders/Update", JsonProcessing.ToStringJsonForBody<FounderDto>(updatedSecondFounderJsonData));
        var updateFirstContent = await updateFirstResponse.Content.ReadAsStringAsync();

        Assert.False(updateFirstResponse.IsSuccessStatusCode, updateFirstContent);
    }
    
}
