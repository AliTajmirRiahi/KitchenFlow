using Restaurant.Presentation;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Restaurant.Integration.Test")]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();


// Call Infrastructure DI setup
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplications();
builder.Services.AddRepositories();
builder.Services.AddMappers();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
