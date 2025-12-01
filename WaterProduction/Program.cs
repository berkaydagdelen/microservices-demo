using Microsoft.EntityFrameworkCore;
using WaterProduction.Data;
using WaterProduction.Repositories;
using WaterProduction.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Entity Framework Core - DbContext Kaydý (Code First)
// Connection string appsettings.json'dan okunur
builder.Services.AddDbContext<WaterProductionDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// HttpClient Factory - ÝZBB API çaðrýlarý için
builder.Services.AddHttpClient();

// Dependency Injection (DI) Ayarlarý - N-Tier Mimari
// Repository'leri kaydet (Veri Eriþim Katmaný)
builder.Services.AddScoped<IWaterProductionRepository, WaterProductionRepository>();

// Service'leri kaydet (Ýþ Mantýðý Katmaný)
builder.Services.AddScoped<IWaterProductionService, WaterProductionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
