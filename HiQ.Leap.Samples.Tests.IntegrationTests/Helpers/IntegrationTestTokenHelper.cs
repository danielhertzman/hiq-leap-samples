using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace HiQ.Leap.Samples.Tests.IntegrationTests.Helpers;

public static class IntegrationTestTokenHelper
{
    public static string GetIntegrationTestToken()
    {
        var exampleKey = "testKeyForHiQLeapDemoTest";

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(exampleKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var tokenHandler = new JwtSecurityTokenHandler();

        var payload = new JwtPayload
        {
            { "iss", "TokenGenerator" },
            { "iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds() },
            { "aud", "Leap Demo Test" },
            { "exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds() },
        };

        var token = new JwtSecurityToken(new JwtHeader(signingCredentials), payload);
        return tokenHandler.WriteToken(token);
    }
}