using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace HiQ.Leap.Samples.Tests.IntegrationTests;

public class PersonControllerTests
{
    [Fact]
    public async Task Test1()
    {
        await using var application = new IntegrationTestApplicationHost();
        var httpClient = application.HttpClient;

        var response = await httpClient.GetAsync("/Persons");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Test2()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(_ => {});

        var client = application.CreateClient();

        var response = await client.GetAsync("/Persons");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}