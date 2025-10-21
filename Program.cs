using Microsoft.EntityFrameworkCore;
using TrilhaNetAzureDesafio.Context;

var builder = WebApplication.CreateBuilder(args);

// Adiciona DbContext com SQL Server
builder.Services.AddDbContext<RHContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

// Adiciona controllers
builder.Services.AddControllers();

// Configura Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// **Configura CORS para permitir requisições do Swagger e outros clientes**
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// Ativa Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Ativa CORS
app.UseCors("AllowAll");

// Redirecionamento HTTPS
app.UseHttpsRedirection();

// Autorização
app.UseAuthorization();

// Mapeia Controllers
app.MapControllers();

app.Run();
