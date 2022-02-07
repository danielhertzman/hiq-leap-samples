using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using HiQ.Leap.Samples.FunctionAppExample.FakeDatabases;
using HiQ.Leap.Samples.FunctionAppExample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HiQ.Leap.Samples.FunctionAppExample;

public class CreateGroupFunction
{
    private readonly FakeGroupDatabase _fakeGroupDatabase;

    public CreateGroupFunction(FakeGroupDatabase fakeGroupDatabase)
    {
        _fakeGroupDatabase = fakeGroupDatabase;
    }

    [FunctionName("CreateGroup")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger logger)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        logger.LogInformation($"Function CreatePersonFunction triggered. Body -> {requestBody}");
        var groupCreateRequest = JsonSerializer.Deserialize<CreateGroupRequest>(requestBody);
        var groupCreated = await _fakeGroupDatabase.CreateGroupAsync(groupCreateRequest);
        return new OkObjectResult(groupCreated);
    }
}