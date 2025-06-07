using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);

if (builder.Environment.IsDevelopment())
{
    builder.Services
        .AddOcelot(builder.Configuration);
}
else
{
    builder.Services
        .AddOcelot(builder.Configuration)
        .AddConsul();
}



builder.Services.AddOpenApi();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


await app.UseOcelot();
app.Run();
