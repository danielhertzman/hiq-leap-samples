using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace HiQ.Leap.Samples.APIExample.Security;

public static class AuthConfigurator
{
    public static void ConfigureJwtBearer(this IServiceCollection services)
    {
        // Creating security using JWT's
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("testKeyForHiQLeapDemoTest"));
        JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = securityKey,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudiences = new List<string> { "Leap Demo Test", },
                ValidIssuers = new List<string> { "TokenGenerator", },
            };
            x.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = c =>
                {
                    c.NoResult();

                    c.Response.StatusCode = 401;
                    c.Response.ContentType = "text/plain";

                    return c.Response.WriteAsync(c.Exception.ToString());
                },
            };
        });
    }

    public static void ConfigurePolicies(this IServiceCollection services)
    {
        //services.AddAuthorization(options =>
        //{
        //    options.AddPolicy("Admin", policy => policy.RequireClaim("access", "Admin"));
        //});
    }

    public static void ConfigureSwaggerSecurity(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.MapType<DateTime>(() => new OpenApiSchema { Type = "string", Pattern = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff") });
            c.SwaggerDoc("v1", new OpenApiInfo() { Title = "HiQ Leap Sample", Version = "v1" });

            // Add security to swagger doc
            c.AddSecurityDefinition(SwaggerConfiguration.SecurityScheme.Name, SwaggerConfiguration.SecurityScheme);
            c.OperationFilter<SecurityRequirementsOperationFilter>();
            c.OperationFilter<AddSwaggerRequestHeadersFilter>();
        });
    }
}