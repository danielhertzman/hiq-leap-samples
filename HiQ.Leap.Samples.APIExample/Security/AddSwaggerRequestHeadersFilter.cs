using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace HiQ.Leap.Samples.APIExample.Security;

public class AddSwaggerRequestHeadersFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var actionAttributes = context.MethodInfo.GetCustomAttributes(typeof(SwaggerRequestHeaderAttribute), false);

        foreach (var attr in actionAttributes.OfType<SwaggerRequestHeaderAttribute>())
        {
            operation.Parameters.Add(new OpenApiParameter()
            {
                Description = attr.Description,
                In = ParameterLocation.Header,
                Name = attr.Name,
                Schema = new OpenApiSchema { Description = attr.Description, Type = attr.Type, Format = attr.Format },
            });
        }
    }
}

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class SwaggerRequestHeaderAttribute : Attribute
{
    public SwaggerRequestHeaderAttribute(string name, string type, string description, string format = "")
    {
        Name = name;
        Type = type;
        Description = description;
        Format = format;
    }

    public string Name { get; }

    public string Type { get; }

    public string Description { get; }

    public string Format { get; }
}

public class SecurityRequirementsOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Policy names map to scopes
        var requiredScopesMethod = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>()
            .Distinct();

        var requiredScopesClass = context.MethodInfo.DeclaringType
            .GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>()
            .Distinct();

        if (requiredScopesMethod.Any() || requiredScopesClass.Any())
        {
            var polices = requiredScopesClass.Select(c => c.Policy).Union(requiredScopesMethod.Select(s => s.Policy)).Distinct().Where(s => !string.IsNullOrEmpty(s)).ToList();

            if (polices.Any())
            {
                string policyTest = $"Required policy: {string.Join(", ", polices)}";
                operation.Summary = string.IsNullOrEmpty(operation.Summary) ? policyTest : $" - {policyTest}";
            }

            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

            operation.Security.Add(new OpenApiSecurityRequirement
            {
                {
                    SwaggerConfiguration.SecurityScheme, polices
                },
            });
        }
    }
}

internal static class SwaggerConfiguration
{
    internal static OpenApiSecurityScheme SecurityScheme { get; } = new OpenApiSecurityScheme()
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Authorization: Bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference() { Id = "Authorization", Type = ReferenceType.SecurityScheme },
    };
}