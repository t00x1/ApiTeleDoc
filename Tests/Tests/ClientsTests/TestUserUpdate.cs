using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Library.Requsets;
using Library.Utils;
using Global;

using Domain.ModelsDTO;

public class TestClientUpdate
{
    private readonly HttpClient _client;
    public TestClientUpdate()
    {
        _client = new HttpClient { BaseAddress = GlobalVariables.URL };
    }
    [Fact]
    public async Task UpdateClient()
    {
        ClientDto jsonData = new ClientDto()
        {
            INN = Functions.GenerateRandomNumber(10),
            Type = 1,
            Phone = Functions.GenerateRandomNumber(10),
            Status = 1,
            Email =  $"test_{Guid.NewGuid()}@example.com"
        };
        await _client.PostAsync("/Clients/Create", JsonProcessing.ToStringJsonForBody<ClientDto>(jsonData));

        ClientDto updatedJsonData = new ClientDto()
        {
            INN = jsonData.INN,
            Type = 1,
            Phone = Functions.GenerateRandomNumber(10),
            Status = 1,
            Email =  $"test_{Guid.NewGuid()}@example.com"
        };
        var response = await _client.PutAsync("/Clients/Update",  JsonProcessing.ToStringJsonForBody<ClientDto>(updatedJsonData));
        Assert.True(response.IsSuccessStatusCode);
    }
}