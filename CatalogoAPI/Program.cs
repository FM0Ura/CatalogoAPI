using CatalogoAPI.Context;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi(); // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


// Adicionar serviços ao contêiner.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var password = builder.Configuration["MYSQL_root_PASS"];

builder.Services.AddDbContext<CatalogoAPIContext>(options =>
    options.UseMySql($"{connectionString}Password={password}",
    ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
