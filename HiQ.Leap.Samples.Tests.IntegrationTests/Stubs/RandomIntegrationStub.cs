using System.Threading.Tasks;
using HiQ.Leap.Samples.RandomIntegration.Contracts;

namespace HiQ.Leap.Samples.Tests.IntegrationTests.Stubs;

public class RandomIntegrationStub : IRandomIntegration
{
    public async Task InvokeRandomIntegrationAsync()
    {
        await Task.CompletedTask;
    }
}