using FluentValidation;
using JobScheduler.Core.DTOs;
using JobScheduler.Core.Interfaces;
using JobScheduler.Core.Mapper;
using JobScheduler.Infrastructure.Data;
using JobScheduler.Infrastructure.Repositories;
using JobScheduler.Infrastructure.Services;
using JobScheduler.Infrastructure.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IValidator<CreateJobRequest>, CreateJobRequestValidator>();
builder.Services.AddScoped<IValidator<UpdateJobRequest>, UpdateJobRequestValidator>();

builder.Services.AddAutoMapper(cfg => { }, typeof(JobProfile));

builder.Services.AddDbContext<JobSchedulerDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure(
            5,
            TimeSpan.FromSeconds(10),
            null
        )));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJobManagementService, JobManagementService>();

var app = builder.Build();

app.MapGet("/", () => "Hello world!");

app.Run();