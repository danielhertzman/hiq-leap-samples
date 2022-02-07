using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using HiQ.Leap.Samples.FunctionAppExample.FakeDatabases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace HiQ.Leap.Samples.FunctionAppExample;

public class GetGroupsWithMembers
{
    private readonly FakeGroupDatabase _fakeGroupDatabase;

    public GetGroupsWithMembers(FakeGroupDatabase fakeGroupDatabase)
    {
        _fakeGroupDatabase = fakeGroupDatabase;
    }

    [FunctionName("GetGroupsWithMembers")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetGroupsWithMembers/{groupId}")] HttpRequest req, string groupId, ILogger logger)
    {
        logger.LogInformation("Function GetGroupsWithMembers triggered");
        var group = await _fakeGroupDatabase.GetGroupAsync(int.Parse(groupId));
        return new OkObjectResult(group);
    }
}