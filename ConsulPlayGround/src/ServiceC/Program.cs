using ServiceC;
using Steeltoe.Discovery.Client;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDiscoveryClient(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

ServiceCInfo.Number = Random.Shared.Next(0, 1000);
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation($"Service C:{ServiceCInfo.Number}");

app.MapGet("/health", () => Results.Ok("Healthy"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
