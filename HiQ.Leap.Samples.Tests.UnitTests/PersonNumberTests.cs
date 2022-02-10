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
        var result = PersonNumberValidator.IsValidPersonNumber("197210047168");
        Assert.True(result);
    }

    [Theory]
    [InlineData("197210047168")]
    [InlineData("19721004-7168")]
    [InlineData("7210047168")]
    [InlineData("721004-7168")]
    [InlineData("72-10047168")]
    public void Test(string input)
    {
        var result = PersonNumberValidator.IsValidPersonNumber(input);
        Assert.True(result);
    }

    [Theory]
    [InlineData("4")]
    [InlineData("19721104-7168")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData(" ")]
    [InlineData("721004-27168")]
    [InlineData("hellohello")]
    [InlineData("721404-7168")]
    public void Test2(string input)
    {
        var result = PersonNumberValidator.IsValidPersonNumber(input);
        Assert.False(result);
    }
}