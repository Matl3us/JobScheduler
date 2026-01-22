using JobScheduler.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<JobSchedulerDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure(
            5,
            TimeSpan.FromSeconds(10),
            null
        )));

var app = builder.Build();

app.MapGet("/", () => "Hello world!");

app.Run();