using HiQ.Leap.Samples.APIExample.Middleware;
using HiQ.Leap.Samples.APIExample.Security;
using HiQ.Leap.Samples.RandomIntegration;
using HiQ.Leap.Samples.RandomIntegration.Contracts;
using HiQ.Leap.Samples.Repository;
using HiQ.Leap.Samples.Repository.Contracts;
using HiQ.Leap.Samples.Services;
using HiQ.Leap.Samples.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IPersonService, PersonService>();
//builder.Services.AddSingleton<IPersonService, FakePersonService>();
builder.Services.AddSingleton<IRepository, Repository>();
builder.Services.AddSingleton<IRandomIntegration, AdviceMemeIntegration>();
builder.Services.AddLogging();

// Configure security (see Security/AuthConfigurator.cs)
builder.Services.ConfigureJwtBearer();
builder.Services.ConfigurePolicies();
builder.Services.ConfigureSwaggerSecurity();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlerMiddleware>();

//app.Use(async (context, next) =>
//{
//    try
//    {
//        await next(context);
//    }
//    catch (APIException ex)
//    {
//        context.Response.ContentType = "application/json";
//        context.Response.StatusCode = (int) ex.StatusCode;

//        var errorResponse = JsonSerializer.Serialize(new
//        {
//            StatusCode = (int) ex.StatusCode,
//            Message = ex.Message,
//        });

//        await context.Response.WriteAsync(errorResponse);
//    }
//    catch (Exception)
//    {
//        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
//        context.Response.ContentType = "text/plain";
//        await context.Response.WriteAsync("An exception was thrown.");
//    }
//});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();