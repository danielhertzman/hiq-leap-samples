using System.Net.Http;
using System.Net.Http.Headers;
using HiQ.Leap.Samples.Tests.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace HiQ.Leap.Samples.Tests.IntegrationTests;

internal class BasicIntegrationTestApplicationHost : WebApplicationFactory<Program>
{
    private readonly string _environment;

    internal HttpClient HttpClient;

    public BasicIntegrationTestApplicationHost(string environment = "IntegrationTests")
    {
        _environment = environment;
        HttpClient = CreateClient();
        var apiKey = IntegrationTestTokenHelper.GetIntegrationTestToken();
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment(_environment);

        // Add mock/test services to the builder here
        //builder.ConfigureServices(services =>
        //{
        //    services.AddSingleton<IRandomIntegration, RandomIntegrationStub>();
        //});

        return base.CreateHost(builder);
    }
}