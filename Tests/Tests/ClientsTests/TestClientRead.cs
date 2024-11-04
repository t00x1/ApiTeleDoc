using Domain.Models;
using Domain.ModelsDTO;
using Global;
using Xunit;
using Library.Utils;
public class TestClientRead
{
    private readonly HttpClient _client;
    public TestClientRead(){
         _client = new HttpClient { BaseAddress = GlobalVariables.URL };
    }
    [Fact]
    public async Task ReadAllClients()
    {
        ClientDto jsonData = new ClientDto()
        {
            INN = "1234567892",
            Type = 1,
            Phone = "1234567892",
            Status = 1,
            Email = "test2@gmagil.com"
        };

       

        await _client.PostAsync("/Clients/Create", JsonProcessing.ToStringJsonForBody<ClientDto>(jsonData));

        var response = await _client.GetAsync("/Clients/ReadAll");
        Assert.True(response.IsSuccessStatusCode);
    }
     [Fact]
    public async Task ReadClient()
    {
        ClientDto jsonData = new ClientDto()
        {
            INN = "1234567892",
            Type = 1,
            Phone = "1234567892",
            Status = 1,
            Email = "test2@gmagil.com"
        };
        await _client.PostAsync("/Clients/Create",  JsonProcessing.ToStringJsonForBody<ClientDto>(jsonData));

        var response = await _client.GetAsync($"/Clients/ReadByCondition?INN={jsonData.INN}");
        Assert.True(response.IsSuccessStatusCode);
    }
}