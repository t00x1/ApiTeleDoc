using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Library.Utils;
using Library.Requsets;

public class TestFounderUpdate
{
    private readonly HttpClient _client;

    public TestFounderUpdate()
    {
        _client = new HttpClient { BaseAddress = new Uri("http://localhost:5110") };
    }

    [Fact]
    public async Task UpdateFounder_ShouldReturnOK()
    {
        string createdClientINN = await CreateClientRequest.CreateClientAndGetINN("1");
        var firstFounderJsonData = new
        {
            INN = Functions.GenerateRandomNumber(10),
            Phone = Functions.GenerateRandomNumber(10),
            LastName = "Alexandr",
            FirstName = "Spektor",
            Patronymic = "",
            Email = $"founder_{Guid.NewGuid()}@example.com",
            ClientINN = createdClientINN
        };

        var firstJsonContent = new StringContent(
            JsonConvert.SerializeObject(firstFounderJsonData),
            Encoding.UTF8,
            "application/json"
        );
        var firstResponse = await _client.PostAsync("/Founders/Create", firstJsonContent);
        var firstContent = await firstResponse.Content.ReadAsStringAsync();

        Assert.True(firstResponse.IsSuccessStatusCode, firstContent);

        var updatedJsonData = new
        {
            INN = firstFounderJsonData.INN,
            Phone = "9234567890",
            LastName = "UpdatedLastName",
            FirstName = "UpdatedFirstName",
            Patronymic = "",
            Email = "UpdatedEmail@gmail.com",
            ClientINN = createdClientINN
        };

        var updatedJsonContent = new StringContent(
            JsonConvert.SerializeObject(updatedJsonData),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _client.PutAsync("/Founders/Update", updatedJsonContent);
        var content = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode, content);
    }

    [Fact]
    public async Task UpdateFounder_ButNotExistClient()
    {
        var updatedJsonData = new
        {
            INN = "1111111111",
            Phone = "9234567890",
            LastName = "UpdatedLastName",
            FirstName = "UpdatedFirstName",
            Patronymic = "",
            Email = "UpdatedEmail@gmail.com",
            ClientINN = "21312312313123131312312"
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
    public async Task UpdateFounderButOneToManyClientsWhenType1()
    {

        string createdClientINN = await CreateClientRequest.CreateClientAndGetINN("1");
        var firstFounderJsonData = new
        {
            INN = Functions.GenerateRandomNumber(10),
            Phone = Functions.GenerateRandomNumber(10),
            LastName = "Alexandr",
            FirstName = "Spektor",
            Patronymic = "",
            Email = $"founder_{Guid.NewGuid()}@example.com",
            ClientINN = createdClientINN
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
            ClientINN = await CreateClientRequest.CreateClientAndGetINN("0")
        };

        var secondJsonContent = new StringContent(
            JsonConvert.SerializeObject(secondFounderJsonData),
            Encoding.UTF8,
            "application/json"
        );

 
        var secondResponse = await _client.PostAsync("/Founders/Create", secondJsonContent);
        var secondContent = await secondResponse.Content.ReadAsStringAsync();

        Assert.True(secondResponse.IsSuccessStatusCode, secondContent);

        var updatedSecondFounderJsonData = new
        {
            INN = secondFounderJsonData.INN,
            Phone = secondFounderJsonData.Phone,
            LastName =  secondFounderJsonData.LastName,
            FirstName =  secondFounderJsonData.FirstName,
            Patronymic = "",
            Email =  secondFounderJsonData.Email,
            ClientINN = createdClientINN
        };

        var updatedFirstJsonContent = new StringContent(
            JsonConvert.SerializeObject(updatedSecondFounderJsonData),
            Encoding.UTF8,
            "application/json"
        );

        var updateFirstResponse = await _client.PutAsync("/Founders/Update", updatedFirstJsonContent);
        var updateFirstContent = await updateFirstResponse.Content.ReadAsStringAsync();

        Assert.False(updateFirstResponse.IsSuccessStatusCode, updateFirstContent);
    }
    
}
