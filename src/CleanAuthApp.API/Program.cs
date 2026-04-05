using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CleanAuthApp.Domain.Interfaces;
using CleanAuthApp.Application;
using CleanAuthApp.Application.Command;
using CleanAuthApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 🔷 Add Controllers
builder.Services.AddControllers();

// 🔷 Add Swagger (optional but useful)
builder.Services.AddEndpointsApiExplorer();

// 🔷 Register MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CleanAuthApp.Application.AssemblyReference).Assembly));

// 🔷 Register In-Memory Repository
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();

// 🔷 Register JWT Service
builder.Services.AddSingleton<IJwtService, JwtService>();

// 🔷 JWT Configuration
var key = builder.Configuration["Jwt:Key"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // for development
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero, // no delay in token expiration
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

// 🔷 Authorization (Role-based)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

// 🔷 Build App
var app = builder.Build();

// 🔷 Middleware pipeline
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

// 🔷 IMPORTANT: Order matters
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


// 🔷 (Optional) Assembly Reference Class
// Create this in Application project:
// namespace CleanAuthApp.Application;
// public class AssemblyReference { }