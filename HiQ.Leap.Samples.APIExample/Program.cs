using System.Net;
using System.Text.Json;
using HiQ.Leap.Samples.APIExample.Middleware;
using HiQ.Leap.Samples.Domain.Exceptions;
using HiQ.Leap.Samples.Services;
using HiQ.Leap.Samples.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IStorageService, StorageService>();
builder.Services.AddLogging();

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

app.UseAuthorization();

app.MapControllers();

app.Run();