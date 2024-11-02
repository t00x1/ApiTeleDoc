// using System.Net.Http;
// using System.Text;
// using System.Threading.Tasks;
// using Newtonsoft.Json;
// using Xunit;
// using Library.Utils;
// using Library.Requsets;

// public class FoundersControllerTests
// {
//     private readonly HttpClient _client;
//     private string _createdFounderINN;

//     public FoundersControllerTests()
//     {
//         _client = new HttpClient { BaseAddress = new Uri("http://localhost:5110") };
//     }

//     [Fact]
//     public async Task CreateFounder_ShouldReturnOk()
//     {
//         string _createdClientINN = await CreateClientRequest.CreateClientAndGetINN();

//         var founderJsonData = new
//         {
//             INN = Functions.GenerateRandomNumber(10),
//             Phone = Functions.GenerateRandomNumber(10),
//             LastName = "Alexandr",
//             FirstName = "Spektor",
//             Patronymic = "",
//             Email = $"founder_{Guid.NewGuid()}@example.com",
//             ClientINN = _createdClientINN
//         };

//         var jsonContent = new StringContent(
//             JsonConvert.SerializeObject(founderJsonData),
//             Encoding.UTF8,
//             "application/json"
//         );

//         var response = await _client.PostAsync("/Founders/Create", jsonContent);
//         var content = await response.Content.ReadAsStringAsync();
        
//         Assert.True(response.IsSuccessStatusCode, content);

//         _createdFounderINN = founderJsonData.INN;
//     }

//     [Fact]
//     public async Task ReadFounder_ShouldReturnOk()
//     {
//         await CreateFounder_ShouldReturnOk();

//         var response = await _client.GetAsync($"/Founders/ReadByCondition?inn={_createdFounderINN}");
//         var content = await response.Content.ReadAsStringAsync();
        
//         Assert.True(response.IsSuccessStatusCode, content);
//     }

//     [Fact]
//     public async Task CreateWithSameINN()
//     {
//         string INNOfUser = await CreateClientRequest.CreateClientAndGetINN();
//         var jsonData = new
//         {
//             INN = INNOfUser,
//             Type = "1",
//             Phone = Functions.GenerateRandomNumber(10),
//             Status = "1",
//             Email = $"client_{Guid.NewGuid()}@example.com",
//             ClientINN = await CreateClientRequest.CreateClientAndGetINN()
//         };

//         var jsonContent = new StringContent(
//             JsonConvert.SerializeObject(jsonData),
//             Encoding.UTF8,
//             "application/json"
//         );

//         var response = await _client.PostAsync("/Clients/Create", jsonContent);
//         var content = await response.Content.ReadAsStringAsync();
        
//         Assert.False(response.IsSuccessStatusCode, content);
//     }

//     [Fact]
//     public async Task UpdateFounder_ShouldReturnOK()
//     {
//         await CreateClientRequest.CreateClientAndGetINN();

//         var updatedJsonData = new
//         {
//             INN = _createdFounderINN,
//             Phone = "9234567890",
//             LastName = "UpdatedLastName",
//             FirstName = "UpdatedFirstName",
//             Patronymic = "",
//             Email = "UpdatedEmail@gmail.com",
//             ClientINN = await CreateClientRequest.CreateClientAndGetINN() 
//         };

//         var updatedJsonContent = new StringContent(
//             JsonConvert.SerializeObject(updatedJsonData),
//             Encoding.UTF8,
//             "application/json"
//         );

//         var response = await _client.PutAsync("/Founders/Update", updatedJsonContent);
//         var content = await response.Content.ReadAsStringAsync();
        
//         Assert.False(response.IsSuccessStatusCode, content);
//     }

//     [Fact]
//     public async Task UpdateFounder_ButNotExistClient()
//     {
//         var updatedJsonData = new
//         {
//             INN = "1111111111",
//             Phone = "9234567890",
//             LastName = "UpdatedLastName",
//             FirstName = "UpdatedFirstName",
//             Patronymic = "",
//             Email = "UpdatedEmail@gmail.com",
//             ClientINN = "21312312313123131312312" 
//         };

//         var updatedJsonContent = new StringContent(
//             JsonConvert.SerializeObject(updatedJsonData),
//             Encoding.UTF8,
//             "application/json"
//         );

//         var response = await _client.PutAsync("/Founders/Update", updatedJsonContent);
//         var content = await response.Content.ReadAsStringAsync();
        
//         Assert.False(response.IsSuccessStatusCode, content);
//     }

//     [Fact]
//     public async Task DeleteAllFounders_ShouldReturnOk()
//     {
//         var response = await _client.DeleteAsync("/Founders/DeleteAll");
//         var content = await response.Content.ReadAsStringAsync();
        
//         Assert.True(response.IsSuccessStatusCode, content);
//     }

//     [Fact]
//     public async Task CreateFounderButOneToManyClientsWhenType1()
//     {
//         string _createdClientINN = await CreateClientRequest.CreateClientAndGetINN("1");

//         var firstFounderJsonData = new
//         {
//             INN = Functions.GenerateRandomNumber(10),
//             Phone = Functions.GenerateRandomNumber(10),
//             LastName = "Alexandr",
//             FirstName = "Spektor",
//             Patronymic = "",
//             Email = $"founder_{Guid.NewGuid()}@example.com",
//             ClientINN = _createdClientINN
//         };

//         var firstJsonContent = new StringContent(
//             JsonConvert.SerializeObject(firstFounderJsonData),
//             Encoding.UTF8,
//             "application/json"
//         );

//         var firstResponse = await _client.PostAsync("/Founders/Create", firstJsonContent);
//         var firstContent = await firstResponse.Content.ReadAsStringAsync();
        
//         Assert.True(firstResponse.IsSuccessStatusCode, firstContent);

//         var secondFounderJsonData = new
//         {
//             INN = Functions.GenerateRandomNumber(10),
//             Phone = Functions.GenerateRandomNumber(10),
//             LastName = "Ivanov",
//             FirstName = "Ivan",
//             Patronymic = "Petrovich",
//             Email = $"founder_{Guid.NewGuid()}@example.com",
//             ClientINN = _createdClientINN
//         };

//         var secondJsonContent = new StringContent(
//             JsonConvert.SerializeObject(secondFounderJsonData),
//             Encoding.UTF8,
//             "application/json"
//         );

//         var secondResponse = await _client.PostAsync("/Founders/Create", secondJsonContent);
//         var secondContent = await secondResponse.Content.ReadAsStringAsync();
        
//         Assert.False(secondResponse.IsSuccessStatusCode, secondContent);
//     }
// }
