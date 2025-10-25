using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infraestructure.Repositories;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API de Aprovisionamiento Multi-Cloud",
        Description = "API REST para aprovisionamiento de infraestructura en AWS, Azure, GCP y On-Premise. " +
                      "Implementa los patrones Builder, Director y Factory para la creación de máquinas virtuales.",
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddSingleton<IRepositorioInfraestructura, RepositorioInfraestructuraEnMemoria>();

builder.Services.AddScoped<IServicioAprovisionamiento, ServicioAprovisionamiento>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API Aprovisionamiento v1");
    options.RoutePrefix = string.Empty; 
    options.DocumentTitle = "API Aprovisionamiento Multi-Cloud";
});

app.UseCors("AllowAll");
app.UseExceptionHandler("/error");
app.UseAuthorization();
app.MapControllers();
app.MapGet("/swagger", () => Results.Redirect("/"))
    .ExcludeFromDescription();

app.MapGet("/info", () => new
{
    nombre = "API de Aprovisionamiento Multi-Cloud",
    version = "1.0.0",
    descripcion = "API REST para crear máquinas virtuales en múltiples proveedores de nube",
    proveedores = new[] { "AWS", "Azure", "GCP", "OnPremise" },
    tiposMaquina = new[] { "Standard", "MemoryOptimized", "ComputeOptimized" },
    documentacion = "/swagger"
}).WithName("InformacionAPI").WithTags("Info");

app.Logger.LogInformation("==============================================");
app.Logger.LogInformation("API de Aprovisionamiento Multi-Cloud iniciada");
app.Logger.LogInformation("==============================================");
app.Logger.LogInformation("Documentación Swagger: http://localhost:5000");
app.Logger.LogInformation("==============================================");

app.Run();