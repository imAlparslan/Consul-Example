using Microsoft.AspNetCore.Mvc;

namespace ServiceB.Controllers;

[ApiController]
[Route("service-b")]
public class ServiceBController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ServiceBController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("info")]
    public async Task<IActionResult> GetServiceAInfo()
    {
        return await Task.FromResult(Ok(ServiceBInfo.GetServiceInfo));
    }
    [HttpGet("get-info-c")]
    public async Task<IActionResult> GetInfoC()
    {
        HttpClient client = _httpClientFactory.CreateClient("client-c");
        var response = await client.GetAsync("/service-c/info");
        var result = await response.Content.ReadAsStringAsync();

        return Ok($"{ServiceBInfo.GetServiceInfo} --> {result}");
    }
}
