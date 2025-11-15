using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

using FluentValidation;

using ProjectFollowUp.BFF.Application;
using ProjectFollowUp.BFF.WebApi.Controllers.Projects;
using ProjectFollowUp.BFF.WebApi.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddValidation();

builder.Services.AddApplication();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddScoped<IValidator<CreateInput>, CreateInputValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(options =>
    options
        .AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(_ => true)
        .AllowCredentials());

app.Run();

namespace ProjectFollowUp.BFF.WebApi
{
    [CompilerGenerated]
    public class WebApiProgram
    {
    }
}