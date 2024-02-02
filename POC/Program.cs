using MediatR;
using POC.Api.Hubs;
using POC.App.Behaviors;
using POC.Infrastructure.Adapters;
using POC.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add SignalR services
builder.Services.AddSignalR();

// Register the LoggingBehavior for MediatR
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

// Register in-memory repositories
builder.Services.AddSingleton<ScreenProfileRepository>();
builder.Services.AddSingleton<ScreenRepository>();

// Register adapters
//builder.Services.AddHttpClient<CrmAdapter>();
builder.Services.AddHttpClient<CrmAdapter>("CrmApiClient");
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

app.UseHttpsRedirection();
app.UseRouting();

// Map SignalR hubs
app.MapHub<ScreenHub>("/screenHub");

app.MapControllers();
// web hooks
app.MapPost("/webhooks", async context =>
{
    var request = await context.Request.ReadFromJsonAsync<WebhookOrderAdded>();
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync("new order added");
});

app.Run();

public record WebhookOrderAdded(string Header, string Body);