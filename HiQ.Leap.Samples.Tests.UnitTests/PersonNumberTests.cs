using System.Text.Json;
using HiQ.Leap.Samples.Services.Helpers;
using Xunit;

namespace HiQ.Leap.Samples.Tests.UnitTests;

public class PersonNumberTests
{
    private static readonly JsonSerializerOptions CaseInsensitivityOption = new()
    {
        PropertyNameCaseInsensitive = true
    };

    [Fact]
    public void PersonNumberValidator_IsValidPersonNumber_Expect_Valid_True_Simple_Test()
    {
        var result = PersonNumberValidator.IsValidPersonNumber("7210047168");
        Assert.True(result);
    }
}