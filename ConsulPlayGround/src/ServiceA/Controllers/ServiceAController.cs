using Microsoft.AspNetCore.Mvc;

namespace ServiceA.Controllers;

[ApiController]
[Route("service-a")]
public class ServiceAController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<ServiceAController> _logger;

    public ServiceAController(IHttpClientFactory httpClientFactory, ILogger<ServiceAController> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    [HttpGet("info")]
    public async Task<IActionResult> GetServiceAInfo()
    {
        return await Task.FromResult(Ok(ServiceAInfo.GetServiceInfo));
    }

    [HttpGet("get-info-b")]
    public async Task<IActionResult> GetInfoFromB()
    {
        HttpClient client = _httpClientFactory.CreateClient("client-b");
        _logger.LogInformation(client.BaseAddress!.ToString());
        var response = await client.GetAsync("service-b/info");
        var result = await response.Content.ReadAsStringAsync();
        _logger.LogInformation(result);
        return Ok(result);
    }

    [HttpGet("get-info-c-from-b")]
    public async Task<IActionResult> GetInfoCFromB()
    {
        HttpClient client = _httpClientFactory.CreateClient("client-b");
        var response = await client.GetAsync("service-b/get-info-c");
        var result = await response.Content.ReadAsStringAsync();
        return Ok(result);
    }
}
