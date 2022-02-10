using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using HiQ.Leap.Samples.Domain.Models;
using HiQ.Leap.Samples.Domain.RequestModels;
using HiQ.Leap.Samples.RandomIntegration.Contracts;
using HiQ.Leap.Samples.Tests.IntegrationTests.Helpers;
using HiQ.Leap.Samples.Tests.IntegrationTests.Stubs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HiQ.Leap.Samples.Tests.IntegrationTests;

public class PersonControllerTests
{
    private static readonly JsonSerializerOptions CaseInsensitivityOption = new()
    {
        PropertyNameCaseInsensitive = true
    };

    [Fact]
    public async Task Get_Persons_Return_Empty_List_Success_StatusCode()
    {
        // Arrange
        await using var application = new BasicIntegrationTestApplicationHost();
        var httpClient = application.HttpClient;

        // Act
        var response = await httpClient.GetAsync("/Persons");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseBodyString = await response.Content.ReadAsStringAsync();
        var personList = JsonSerializer.Deserialize<List<Person>>(responseBodyString, CaseInsensitivityOption);
        Assert.Empty(personList);
    }

    [Fact]
    public async Task Post_Person_Return_Expected_Values_Created_StatusCode()
    {
        // Arrange - Override implementation of IRandomIntegration to a stub defined in Stubs folder
        var applicationHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IRandomIntegration, RandomIntegrationStub>();
            });
        });

        var httpClient = applicationHost.CreateClient();
        var apiKey = IntegrationTestTokenHelper.GetIntegrationTestToken();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        // Act
        var request = new PersonCreateRequest()
        {
            GivenName = "Test",
            SurName = "Testsson"
        };
        var response = await httpClient.PostAsJsonAsync("/Persons", request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var responseBodyString = await response.Content.ReadAsStringAsync();
        var person = JsonSerializer.Deserialize<Person>(responseBodyString, CaseInsensitivityOption);
        Assert.NotNull(person);
        Assert.Equal("Test", person.GivenName);
        Assert.Equal("Testsson", person.SurName);
    }

    [Fact]
    public async Task Get_Person_By_Id_After_Successful_Post_Return_Person()
    {
        // Arrange
        var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();

        // Act
        var response = await client.GetAsync("/Persons");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Post_Persons_Unauthorized_Client_Return_Unauthorized_StatusCode()
    {
        // Arrange
        var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();

        // Act
        var response = await client.GetAsync("/Persons");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}