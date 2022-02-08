using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace HiQ.Leap.Samples.Tests.UnitTests;

public class PersonControllerTests
{
    [Fact]
    public async Task Test1()
    {
        var application = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                // ... Configure test services
            });

        var client = application.CreateClient();
        var response = await client.GetAsync("/Persons");

        response.EnsureSuccessStatusCode();
    }
}