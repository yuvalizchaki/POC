using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using POC.Api.Controllers.ClientControllers;
using POC.Api.Conventions;
using POC.Api.Helpers;
using POC.Api.Hubs;
using POC.App.Behaviors;
using POC.Infrastructure;
using POC.Infrastructure.Adapters;
using POC.Infrastructure.Common;
using POC.Infrastructure.IRepositories;
using POC.Infrastructure.Repositories;
using POC.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Logging.AddConsole();

// ========== Add services to the container. ==========

// add authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = AuthSettings.Issuer,
            ValidAudience = AuthSettings.Audience,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthSettings.PrivateKey)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            
        };
    });
// Register IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// add authorization
builder.Services.AddAuthorization();

builder.Services.AddLogging();

// when accepting a request, accept request.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:5173") // Client URL
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Register the LoggingBehavior for MediatR
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
builder.Services.AddControllers()
    .AddJsonOptions(options => { JsonOptionsConfigurator.ConfigureJsonOptions(options.JsonSerializerOptions); });
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "JWT Token Authentication API",
        Description = ".NET 8 Web API"
    });
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}

        }
    });
});

builder.Services.AddSignalR()
    .AddJsonProtocol(options => { JsonOptionsConfigurator.ConfigureJsonOptions(options.PayloadSerializerOptions); });

// Register in-memory repositories
// builder.Services.AddSingleton<ScreenProfileRepository>();
// builder.Services.AddSingleton<ScreenRepository>();
builder.Services.AddSingleton<IGuestConnectionRepository, CachedGuestConnectionRepository>();
builder.Services.AddSingleton<ScreenConnectionRepository>();
builder.Services.AddSingleton<ScreenHub>();
builder.Services.AddSingleton<GuestHub>();
builder.Services.AddSingleton<AdminHub>();

// Add the custom route convention
builder.Services.AddControllers(options => { options.Conventions.Add(new KebabCaseRouteConvention()); });

builder.Services.AddHttpClient<CrmTokenAdapter>("CrmAuthClient");
builder.Services.AddHttpClient<CrmAdapter>("CrmApiClient");

// Register adapters
builder.Services.AddSingleton<CrmTokenAdapter>();
builder.Services.AddSingleton<CrmAdapter>();

// Register MediatR and handlers
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

//Cache
builder.Services.AddMemoryCache();

//register the repository
builder.Services.AddScoped<ScreenProfileRepository>();
builder.Services.AddScoped<ScreenRepository>();
builder.Services.AddScoped<AdminRepository>();

builder.Services.AddScoped<IOrderRepository, InMemoryOrderRepository>();
builder.Services.AddScoped<IOrderAdapter, CrmAdapter>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddHostedService<OrderReplicationHostedService>();

//Register the DbContext
builder.Services.AddDbContext<OurDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});
builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    redisOptions.Configuration = builder.Configuration.GetConnectionString("Redis"); ;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Map SignalR hubs
app.MapHub<ScreenHub>("/screen-hub");
app.MapHub<GuestHub>("/guest-hub");
app.MapHub<AdminHub>("/admin-hub");

app.MapControllers();

app.Run();
