using System.IO;
using System.Threading.Tasks;
using HiQ.Leap.Samples.FunctionAppExample.FakeDatabases;
using HiQ.Leap.Samples.FunctionAppExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace HiQ.Leap.Samples.FunctionAppExample;

public class CreatePersonFunction
{
    private readonly FakePersonDatabase _fakePersonDatabase;

    public CreatePersonFunction(FakePersonDatabase fakePersonDatabase)
    {
        _fakePersonDatabase = fakePersonDatabase;
    }

    [FunctionName("CreatePerson")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger logger)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        logger.LogInformation($"Function CreatePersonFunction triggered. Body -> {requestBody}");
        var personCreateRequest = JsonSerializer.Deserialize<PersonCreateRequest>(requestBody);
        var personCreated = await _fakePersonDatabase.AddAsync(personCreateRequest);
        return new OkObjectResult(personCreated);
    }
}