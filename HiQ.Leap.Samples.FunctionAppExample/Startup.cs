using HiQ.Leap.Samples.FunctionAppExample.FakeDatabases;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(HiQ.Leap.Samples.FunctionAppExample.Startup))]

namespace HiQ.Leap.Samples.FunctionAppExample;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddSingleton<FakePersonDatabase>();
        builder.Services.AddSingleton<FakeGroupDatabase>();
    }
}