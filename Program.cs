using Microsoft.OpenApi.Models;

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

// Otros servicios como controllers y swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
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