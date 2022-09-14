using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using TODOApplication.Models.AppDbContext.DbContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//version = 8.0.30
var connectionString = "server=localhost;user=root;password=fLDjdRGTYPENJCYL8E4jCTo8P;database=TODOdb";

builder.Services.AddDbContext<AppDbContext>(
dbContextOptions => dbContextOptions
        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        // The following three options help with debugging, but should
        // be changed or removed for production.
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(
    x => x.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
