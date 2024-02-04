using MediatR;
using POC.Api.Conventions;
using POC.Api.Hubs;
using POC.App.Behaviors;
using POC.Infrastructure.Adapters;
using POC.Infrastructure.Common;
using POC.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

// ========== Add services to the container. ==========
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
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR()
    .AddJsonProtocol(options => { JsonOptionsConfigurator.ConfigureJsonOptions(options.PayloadSerializerOptions); });

// Register in-memory repositories
builder.Services.AddSingleton<ScreenProfileRepository>();
builder.Services.AddSingleton<ScreenRepository>();
builder.Services.AddSingleton<GuestConnectionRepository>();
builder.Services.AddSingleton<ScreenHub>();
builder.Services.AddSingleton<GuestHub>();

// Add the custom route convention
builder.Services.AddControllers(options => { options.Conventions.Add(new KebabCaseRouteConvention()); });

//builder.Services.AddHttpClient<CrmAdapter>();
builder.Services.AddHttpClient<CrmAdapter>("CrmApiClient");

// Register adapters
builder.Services.AddSingleton<CrmAdapter>();

// Register MediatR and handlers
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


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

// app.UseAuthentication(); // Add this if you have authentication
// app.UseAuthorization();  // Add this if you have authorizatio

// Map SignalR hubs
app.MapHub<ScreenHub>("/screen-hub");
app.MapHub<GuestHub>("/guest-hub");

app.MapControllers();

app.Run();
