using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddControllers();


var connectionString = Environment.GetEnvironmentVariable("TodoContext");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Database connection string is not set in environment variables.");
}

builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options =>
    // options.WithOrigins("https://delightful-bush-08e8f4500.4.azurestaticapps.net/")
    options.WithOrigins("http://localhost:3000")
           .AllowAnyMethod()
           .AllowAnyHeader()
);

app.UseAuthorization();

app.MapControllers();

app.Run();
