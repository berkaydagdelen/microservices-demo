using UserService.Data;
using UserService.Repositories;
using UserService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Entity Framework Core - DbContext Kaydý
// Connection string appsettings.json'dan okunur
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection (DI) Ayarlarý
// Repository'leri kaydet (veri eriþim katmaný)
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Service'leri kaydet (iþ mantýðý katmaný)
builder.Services.AddScoped<IUserService, UserService.Services.UserService>();

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
