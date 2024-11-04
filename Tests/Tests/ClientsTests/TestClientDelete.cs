using Xunit;
using Library.Utils;
using Global;
public class TestClientDelete
{
    private readonly HttpClient _client;
    public TestClientDelete()
    {
         _client = new HttpClient { BaseAddress = GlobalVariables.URL };
    }
    [Fact]
    public async Task DeleteAllClients()
    {
        var response = await _client.DeleteAsync("/Clients/DeleteAll");
        Assert.True(response.IsSuccessStatusCode);

        var result = await _client.DeleteAsync("/Clients/DeleteAll");
        Assert.True(result.IsSuccessStatusCode);
    }
}