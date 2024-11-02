using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

public class TestFounderDelete
{
    private readonly HttpClient _client;

    public TestFounderDelete()
    {
        _client = new HttpClient { BaseAddress = new Uri("http://localhost:5110") };
    }

    [Fact]
    public async Task DeleteAllFounders_ShouldReturnOk()
    {
        var response = await _client.DeleteAsync("/Founders/DeleteAll");
        var content = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode, content);
    }
}
