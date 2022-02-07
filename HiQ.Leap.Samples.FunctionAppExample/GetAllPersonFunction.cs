using System.Threading.Tasks;
using HiQ.Leap.Samples.FunctionAppExample.FakeDatabases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace HiQ.Leap.Samples.FunctionAppExample;

public class GetAllPersonFunction
{
    private readonly FakePersonDatabase _fakePersonDatabase;

    public GetAllPersonFunction(FakePersonDatabase fakePersonDatabase)
    {
        _fakePersonDatabase = fakePersonDatabase;
    }

    [FunctionName("GetAllPerson")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger logger)
    {
        logger.LogInformation("Function GetAllPersonFunction triggered");
        var persons = await _fakePersonDatabase.GetPersonsAsync();
        return new OkObjectResult(persons);
    }
}