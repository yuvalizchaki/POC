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
builder.Services.AddSingleton<ConnectionRepository>();
builder.Services.AddSingleton<GuestHub>();

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
// const string server = "http://localhost:8008";
// const string callback = "http://localhost:5177/webhook";
// const string topic = "order.new";
//
// var client = new HttpClient();
//
// Console.WriteLine($"Subscribing to topic {topic} with callback {callback}");
// await client.PostAsJsonAsync(server + "/webhook", new { topic, callback });
// app.MapPost("/webhook", async context =>
// {
//     var request = await context.Request.ReadFromJsonAsync<WebhookOrderAdded>();
//     context.Response.StatusCode = 200;
//     await context.Response.WriteAsync($"new order added: {request?.Body}");
// });

app.Run();

public record WebhookOrderAdded(string Header, string Body);