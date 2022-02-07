using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using HiQ.Leap.Samples.FunctionAppExample.FakeDatabases;
using HiQ.Leap.Samples.FunctionAppExample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace HiQ.Leap.Samples.FunctionAppExample;

public class AddPersonToGroupFunction
{
    private readonly FakeGroupDatabase _fakeGroupDatabase;
    private readonly FakePersonDatabase _fakePersonDatabase;

    public AddPersonToGroupFunction(FakeGroupDatabase fakeGroupDatabase, FakePersonDatabase fakePersonDatabase)
    {
        _fakeGroupDatabase = fakeGroupDatabase;
        _fakePersonDatabase = fakePersonDatabase;
    }

    [FunctionName("AddPersonToGroup")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req, ILogger logger)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        logger.LogInformation($"Function AddPersonToGroupFunction triggered. Body -> {requestBody}");
        var request = JsonSerializer.Deserialize<AddPersonToGroupRequest>(requestBody);

        var person = await _fakePersonDatabase.GetPersonAsync(request.PersonId);

        await _fakeGroupDatabase.AddMemberToGroupAsync(request.GroupId, person);
        return new OkResult();
    }
}