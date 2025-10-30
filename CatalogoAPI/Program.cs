using APICatalogo.Filters;
using CatalogoAPI.Context;
using CatalogoAPI.Extensions;
using CatalogoAPI.Filters;
using CatalogoAPI.Logging;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter));
}).AddJsonOptions(options =>
        options.JsonSerializerOptions
            .ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddOpenApi(); // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var password = builder.Configuration["MYSQL_root_PASS"];

var fullConnectionString = $"{connectionString}Password={password}";

builder.Services.AddDbContext<CatalogoAPIContext>(options =>
    options.UseMySql(fullConnectionString,
    ServerVersion.AutoDetect(fullConnectionString)));

builder.Services.AddTransient<ApiLoggingFilter>();
builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Debug,
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();