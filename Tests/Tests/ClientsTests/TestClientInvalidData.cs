using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Library.Utils;
using Global;
using Domain.ModelsDTO;

public class TestClientInvalidData
{
    private readonly HttpClient _client;

    public TestClientInvalidData()
    {
        _client = new HttpClient { BaseAddress = GlobalVariables.URL };
    }

    [Fact]
    public async Task CreateWithInvalidEmail()
    {
        ClientDto jsonData = new ClientDto()
        {
            INN = Functions.GenerateRandomNumber(10),
            Type = 1,
            Phone = Functions.GenerateRandomNumber(10),
            Status = 1,
            Email = $"test_{Guid.NewGuid()}example.com"
        };
        var response = await _client.PostAsync("/Clients/Create", JsonProcessing.ToStringJsonForBody<ClientDto>(jsonData));
        var content = await response.Content.ReadAsStringAsync();
        Assert.False(response.IsSuccessStatusCode, content);
    }

    [Fact]
    public async Task CreateWithInvalidPhone()
    {
        ClientDto jsonData = new ClientDto()
        {
            INN = Functions.GenerateRandomNumber(10),
            Type = 1,
            Phone = Functions.GenerateRandomNumber(1),
            Status = 1,
            Email = $"test_{Guid.NewGuid()}@example.com"
        };
        var response = await _client.PostAsync("/Clients/Create",  JsonProcessing.ToStringJsonForBody<ClientDto>(jsonData));
        var content = await response.Content.ReadAsStringAsync();
        Assert.False(response.IsSuccessStatusCode, content);
    }

    [Fact]
    public async Task CreateWithInvalidINN()
    {
        ClientDto jsonData = new ClientDto()
        {
            INN = Functions.GenerateRandomNumber(0),
            Type = 1,
            Phone = Functions.GenerateRandomNumber(10),
            Status = 1,
            Email = $"test_{Guid.NewGuid()}@example.com"
        };

        
        var response = await _client.PostAsync("/Clients/Create", JsonProcessing.ToStringJsonForBody<ClientDto>(jsonData));
        var content = await response.Content.ReadAsStringAsync();
        Assert.False(response.IsSuccessStatusCode, content);
    }

    [Fact]
    public async Task UpdateClientInvalidEmail()
    {
        ClientDto jsonData = new ClientDto()
        {
            INN = Functions.GenerateRandomNumber(10),
            Type = 1,
            Phone = Functions.GenerateRandomNumber(10),
            Status = 1,
            Email = $"test_{Guid.NewGuid()}@example.com"
        };
        await _client.PostAsync("/Clients/Create", JsonProcessing.ToStringJsonForBody<ClientDto>(jsonData));

        ClientDto updatedJsonData = new ClientDto()
        {
            INN = jsonData.INN,
            Type = 1,
            Phone = Functions.GenerateRandomNumber(10),
            Status = 1,
            Email = "UpdatedEmailgmail.com"
        };
        var response = await _client.PutAsync("/Clients/Update", JsonProcessing.ToStringJsonForBody<ClientDto>(updatedJsonData));
        var content = await response.Content.ReadAsStringAsync();
        Assert.False(response.IsSuccessStatusCode, content);
    }

    [Fact]
    public async Task UpdateClientInvalidINN()
    {
        ClientDto jsonData = new ClientDto()
        {
            INN = Functions.GenerateRandomNumber(10),
            Type = 1,
            Phone = Functions.GenerateRandomNumber(10),
            Status = 1,
            Email = $"test_{Guid.NewGuid()}@example.com"
        };
        await _client.PostAsync("/Clients/Create", JsonProcessing.ToStringJsonForBody<ClientDto>(jsonData));

        ClientDto updatedJsonData = new ClientDto()
        {
            INN = "123123",
            Type = 1,
            Phone = Functions.GenerateRandomNumber(10),
            Status = 1,
            Email = $"test_{Guid.NewGuid()}@example.com"
        };
        var response = await _client.PutAsync("/Clients/Update", JsonProcessing.ToStringJsonForBody<ClientDto>(updatedJsonData));
        var content = await response.Content.ReadAsStringAsync();
        Assert.False(response.IsSuccessStatusCode, content);
    }

    [Fact]
    public async Task UpdateClientInvalidPhone()
    {
        ClientDto jsonData = new ClientDto()
        {
            INN = Functions.GenerateRandomNumber(10),
            Type = 1,
            Phone = Functions.GenerateRandomNumber(10),
            Status = 1,
            Email = $"test_{Guid.NewGuid()}@example.com"
        };
        await _client.PostAsync("/Clients/Create", JsonProcessing.ToStringJsonForBody<ClientDto>(jsonData));

        ClientDto updatedJsonData = new ClientDto()
        {
            INN = jsonData.INN,
            Type = 1,
            Phone = Functions.GenerateRandomNumber(9),
            Status = 1,
            Email = $"test_{Guid.NewGuid()}@example.com"
        };
        var response = await _client.PutAsync("/Clients/Update", JsonProcessing.ToStringJsonForBody<ClientDto>(updatedJsonData));
        var content = await response.Content.ReadAsStringAsync();
        Assert.False(response.IsSuccessStatusCode, content);
    }
    [Fact]
    public async Task UpdateClientInvalidWithSameDataAlreadyExist()
    {

        ClientDto jsonData = new ClientDto()
        {
            INN = "2234567890",
            Type = 1,
            Phone = "2234567890",
            Status = 1,
            Email = $"test_{Guid.NewGuid()}@example.com"
        };
        await _client.PostAsync("/Clients/Create", JsonProcessing.ToStringJsonForBody<ClientDto>(jsonData));

     
        ClientDto jsonDataSecond = new ClientDto()
        {
            INN = "0987654323",
            Type = 1,
            Phone = "0987654323",
            Status = 1,
            Email = $"test_{Guid.NewGuid()}@example.com"
        };
        await _client.PostAsync("/Clients/Create", JsonProcessing.ToStringJsonForBody<ClientDto>(jsonDataSecond));

       
        ClientDto updatedJsonData = new ClientDto()
        {
            INN = jsonDataSecond.INN,
            Type = jsonData.Type,
            Phone = jsonDataSecond.Phone,
            Status = jsonData.Status,
            Email = jsonData.Email
        };
        var response = await _client.PutAsync("/Clients/Update", JsonProcessing.ToStringJsonForBody<ClientDto>(updatedJsonData));
        var content = await response.Content.ReadAsStringAsync();
        Assert.False(response.IsSuccessStatusCode, content);
    }
}