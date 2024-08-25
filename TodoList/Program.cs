using Microsoft.EntityFrameworkCore;
using Todolist.DAL;
using Todolist.DAL.Interfaces;
using Todolist.DAL.Repositories;
using TodoList.Domain.Entity;
using TodoList.Service.Implementations;
using TodoList.Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddCors();

builder.Services.AddScoped<ILogger, LoggerService>();
builder.Services.AddScoped<ITestService, TestService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine(connectionString);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

app.MapControllers();
app.UseCors(corsPolicyBuilder =>
{
    corsPolicyBuilder.AllowAnyOrigin();
    corsPolicyBuilder.AllowAnyHeader();
    corsPolicyBuilder.AllowAnyMethod();
});

app.Run();