using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TeloApi.Contexts;
using TeloApi.Features.Hotel.Repositories;
using TeloApi.Features.Hotel.Services;
using TeloApi.Features.Promotion.Repositories;
using TeloApi.Features.Promotion.Services;
using TeloApi.Features.Rate.Repositories;
using TeloApi.Features.Rate.Services;
using TeloApi.Features.Review.Repositories;
using TeloApi.Features.Review.Services;

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
builder.Services.AddScoped<IPromotionService, PromotionService>();
builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();


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
    // Configuración básica de Swagger
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TeloApi",
        Version = "v1"
    });

    // Habilitar anotaciones de Swagger
    c.EnableAnnotations();

    // Configuración para definir OperationId
    c.CustomOperationIds(apiDesc =>
    {
        // Usa el nombre del controlador + método de la acción para definir OperationId
        return apiDesc.ActionDescriptor.RouteValues["action"];
    });
});
var app = builder.Build();

// Usa CORS
app.UseCors("PermitirTodo");
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();