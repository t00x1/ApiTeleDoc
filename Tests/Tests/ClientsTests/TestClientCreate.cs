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

public class TestClientCreate
{
    private readonly HttpClient _client;

    public TestClientCreate()
    {
         _client = new HttpClient { BaseAddress = GlobalVariables.URL };
    }
    [Fact]
    public async Task CreateClient()
    {
        ClientDto jsonData = new ClientDto
        {
            INN = Functions.GenerateRandomNumber(10),
            Type = 1,
            Phone = Functions.GenerateRandomNumber(10),
            Status = 1,
            Email = $"test_{Guid.NewGuid()}@example.com"
        };
        var response = await _client.PostAsync("/Clients/Create",  JsonProcessing.ToStringJsonForBody<ClientDto>(jsonData));
        var content = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode, content);
    }
    [Fact]
    public async Task CreateClientWithSameINN()
    {
        string _createdClientINN = await CreateClientRequest.CreateClientAndGetINN();
        ClientDto jsonData = new ClientDto()
        {
            INN = _createdClientINN,
            Type = 1,
            Phone = Functions.GenerateRandomNumber(10),
            Status = 1,
            Email = $"test_{Guid.NewGuid()}@example.com"
        };


        var response = await _client.PostAsync("/Clients/Create",  JsonProcessing.ToStringJsonForBody<ClientDto>(jsonData));
        var content = await response.Content.ReadAsStringAsync();
        Assert.False(response.IsSuccessStatusCode, content);
    }
    
    
    
}