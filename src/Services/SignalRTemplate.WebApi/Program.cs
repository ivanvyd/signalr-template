using Microsoft.AspNetCore.Mvc;
using SignalRTemplate.Domain;
using SignalRTemplate.WebApi.Models;
using SignalRTemplate.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

builder.Services.AddSingleton<IRepository, Repository>();
builder.Services.AddScoped<IService, Service>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

// Map demo endpoints.
app.MapGet("/items", (IService service) =>
{
    var items = service.GetItems();

    return Results.Ok(items);
})
.WithSummary("Get Items")
.Produces<List<Item>>(StatusCodes.Status200OK)
.WithOpenApi();

app.MapDelete("/items/{itemId}", (IService service, [FromRoute] int itemId) =>
{
    service.DeleteItem(itemId);

    return Results.NoContent();
})
.WithSummary("Delete Item by Id")
.Produces(StatusCodes.Status204NoContent)
.WithOpenApi();

app.MapPost("/items", (IService service, [FromBody] CreateItemModel data) =>
{
    service.CreateItem(data.Name);

    return Results.NoContent();
})
.WithSummary("Create Item")
.Produces(StatusCodes.Status204NoContent)
.WithOpenApi();

app.Run();