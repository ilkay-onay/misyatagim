using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityService.Infrastructure.Data;
using MediatR;
using IdentityService.Application.Interfaces;
using IdentityService.Infrastructure.Repositories;
using IdentityService;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using IdentityService.Domain.Entities;
using Duende.IdentityServer.AspNetIdentity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Replace with your specific origin(s)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});

// DbContext'i ekle
builder.Services.AddDbContext<IdentityDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("PostgreConnection");
    options.UseNpgsql(connectionString);
});

// Identity'yi ekle
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "MisyatagimCookie";
    options.LoginPath = "/api/auth/login";
     options.Events = new CookieAuthenticationEvents
     {
        OnRedirectToLogin = context =>
       {
         context.Response.StatusCode = 401;
        return Task.CompletedTask;
      }
     };
});

  // Cookie auth ekle
builder.Services.AddAuthentication(options =>
{
     options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

builder.Services.AddScoped<IUserRepository, UserRepository>();

// MediatR'ı ekle
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// IdentityServer'ı ekle
builder.Services.AddIdentityServer(options =>
{
options.Events.RaiseErrorEvents = true;
options.Events.RaiseInformationEvents = true;
options.Events.RaiseFailureEvents = true;
options.Events.RaiseSuccessEvents = true;
options.EmitStaticAudienceClaim = true;
})
.AddInMemoryApiResources(Config.ApiResources)
.AddInMemoryApiScopes(Config.ApiScopes)
.AddInMemoryClients(Config.Clients)
.AddInMemoryIdentityResources(Config.IdentityResources)
.AddAspNetIdentity<User>()
.AddDeveloperSigningCredential();

      
builder.Services.AddAuthentication()
         .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
      
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
app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();