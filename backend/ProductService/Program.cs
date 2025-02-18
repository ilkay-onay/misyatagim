using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductService.Application.Interfaces;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.Repositories;
using ProductService.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// CORS'u ekle
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// MediatR'ı ekle
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Redis'i ekle
builder.Services.AddSingleton<RedisService>();

// DbContext'i ekle
builder.Services.AddDbContext<ProductDbContext>((options) =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
     if (!string.IsNullOrEmpty(connectionString))
     {
          options.UseSqlServer(connectionString);
     } else{
       options.UseSqlServer(builder.Configuration.GetConnectionString("PostgreConnection"));
     }
});

// Repository'leri ekle
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Diğer servisler
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowAll");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();