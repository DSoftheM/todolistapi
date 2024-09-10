using Microsoft.EntityFrameworkCore;
using Todolist.DAL;
using TodoList.Service.EmployeeService;
using TodoList.Service.TaskService;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddCors();

builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"connectionString = {connectionString}");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
    options.EnableSensitiveDataLogging();
});

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();
app.UseCors(corsPolicyBuilder =>
{
    corsPolicyBuilder.AllowAnyOrigin();
    corsPolicyBuilder.AllowAnyHeader();
    corsPolicyBuilder.AllowAnyMethod();
});

app.Run();