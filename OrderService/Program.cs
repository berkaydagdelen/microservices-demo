using OrderService.Repositories;
using OrderService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection (DI) Ayarlarý
// Repository'leri kaydet (veri eriþim katmaný)
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Service'leri kaydet (iþ mantýðý katmaný)
// OrderService namespace ile çakýþtýðý için tam namespace kullanýyoruz
builder.Services.AddScoped<IOrderService, OrderService.Services.OrderService>();

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
