using FlightReservationsSystem.Application.Interfaces;
using FlightReservationsSystem.Application.Services;
using FlightReservationsSystem.Domain.Entities;
using FlightReservationsSystem.Domain.Interfaces;
using FlightReservationsSystem.Infrastructure.Persistence;
using FlightReservationsSystem.Infrastructure.Repositories;
using FlightReservationsSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddControllers();

// Configure JWT Authentication
UseJwt(builder);

// Configure Swagger for API documentation
UseSwagger(builder);

// Inject the application services and repositories
InjectServices(builder);

var app = builder.Build();

app.MapDefaultEndpoints();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    AppDbContext.SeedData(services);
}

// Enable Swagger UI only in Development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

// Enable Authentication and Authorization middlewares
app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

// Map controllers to the application
app.MapControllers();

app.Run();

/// <summary>
/// Configures JWT authentication.
/// </summary>
static void UseJwt(WebApplicationBuilder builder)
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");
    var secretKey = jwtSettings.GetValue<string>("SecretKey");

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
            ValidAudience = jwtSettings.GetValue<string>("Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });
}

/// <summary>
/// Configures Swagger for API documentation.
/// </summary>
static void UseSwagger(WebApplicationBuilder builder)
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlightReservationSystem API", Version = "v1" });

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Bearer {token}"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    });
}

/// <summary>
/// Registers application services and repositories.
/// </summary>
static void InjectServices(WebApplicationBuilder builder)
{
    builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IFlightRepository, FlightRepository>();
    builder.Services.AddScoped<IReservationRepository, ReservationRepository>();

    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IFlightService, FlightService>();
    builder.Services.AddScoped<IReservationService, ReservationService>();
    builder.AddSqlServerFeatures();

    builder.Services.AddTransient<IEmailService, EmailService>();
}
