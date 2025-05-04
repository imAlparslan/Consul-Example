using ServiceB;
using Steeltoe.Common.Http.Discovery;
using Steeltoe.Discovery.Client;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDiscoveryClient(builder.Configuration);

builder.Services.AddHttpClient("client-c", client => client.BaseAddress = new Uri("http://service-c"))
    .AddServiceDiscovery();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

ServiceBInfo.Number = Random.Shared.Next(0, 1000);
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation($"Service B:{ServiceBInfo.Number}");

app.MapGet("/health", () => Results.Ok("Healthy"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
