using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TeloApi.Contexts;
using TeloApi.Features.Rate.Repositories;
using TeloApi.Features.Rate.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Registrar RateService y RateRepository
builder.Services.AddScoped<IRateService, RateService>();
builder.Services.AddScoped<IRateRepository, RateRepository>();

// Otros servicios como controllers y swagger
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    // Configuración de Swagger
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TeloApi",
        Version = "v1"
    });

    // Configuración para definir OperationId
    c.CustomOperationIds(apiDesc =>
    {
        // Usa el nombre del controlador + método de la acción para definir OperationId
        return apiDesc.ActionDescriptor.RouteValues["controller"] + "_" + apiDesc.ActionDescriptor.RouteValues["action"];
    });
});

var app = builder.Build();

// Usa CORS
app.UseCors("PermitirTodo");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();