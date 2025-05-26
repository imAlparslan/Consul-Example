using ServiceA;
using Steeltoe.Common.Http.Discovery;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Consul;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddDiscoveryClient(builder.Configuration);
    builder.Services
    .AddHttpClient("client-b", client => client.BaseAddress = new Uri(builder.Configuration["ServiceAddresses:ServiceB"]!))
    .AddServiceDiscovery();
}
else
{
    builder.Services
    .AddHttpClient("client-b", client => client.BaseAddress = new Uri(builder.Configuration["ServiceAddresses:ServiceB"]!));
}


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


ServiceAInfo.Number = Random.Shared.Next(0, 1000);
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation($"{ServiceAInfo.GetServiceInfo}");

app.MapGet("/health", () => Results.Ok("Healthy"));

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

// app.Lifetime.ApplicationStopping.Register(() =>
// {
//     var discoveryClient = app.Services.GetRequiredService<IDiscoveryClient>();
//     discoveryClient.ShutdownAsync().Wait();
// });