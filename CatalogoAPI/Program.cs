using APICatalogo.Filters;
using CatalogoAPI.Context;
using CatalogoAPI.DTOs.Mapping;
using CatalogoAPI.Extensions;
using CatalogoAPI.Filters;
using CatalogoAPI.Logging;
using CatalogoAPI.Repositories.Categorias;
using CatalogoAPI.Repositories.Generic;
using CatalogoAPI.Repositories.Produtos;
using CatalogoAPI.Repositories.Unity_of_Work;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter));
}).AddJsonOptions(options =>
        options.JsonSerializerOptions
            .ReferenceHandler = ReferenceHandler.IgnoreCycles).AddNewtonsoftJson();
builder.Services.AddOpenApi();
builder.Services.AddAuthentication("Bearer").AddJwtBearer();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var password = builder.Configuration["MYSQL_root_PASS"];

var fullConnectionString = $"{connectionString}Password={password}";

builder.Services.AddDbContext<CatalogoAPIContext>(options =>
    options.UseMySql(fullConnectionString,
    ServerVersion.AutoDetect(fullConnectionString)));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<CatalogoAPIContext>()
    .AddDefaultTokenProviders();
builder.Services.AddTransient<ApiLoggingFilter>();
builder.Services.AddScoped<ICategoriasRepository, CategoriasRepository>();
builder.Services.AddScoped<IProdutosRepository, ProdutosRepository>();
builder.Services.AddScoped(typeof(IRepositoryGeneric<>), typeof(RepositoryGeneric<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Debug,
}));

builder.Services.AddAutoMapper(cfg => { }, typeof(DTOMappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.ConfigureExceptionHandler();
}
app.UseCors(policy => policy.WithOrigins("http://localhost:50077"));
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();