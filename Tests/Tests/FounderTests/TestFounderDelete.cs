using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Global;
public class TestFounderDelete
{
    private readonly HttpClient _client;

    public TestFounderDelete()
    {
        _client = new HttpClient { BaseAddress = GlobalVariables.URL };
    }

    [Fact]
    public async Task DeleteAllFounders_ShouldReturnOk()
    {
        var response = await _client.DeleteAsync("/Founders/DeleteAll");
        var content = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode, content);
    }
}
