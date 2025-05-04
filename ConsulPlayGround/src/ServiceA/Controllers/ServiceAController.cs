using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace ServiceA.Controllers;

[ApiController]
[Route("service-a")]
public class ServiceAController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ServiceAController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
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
        var response = await client.GetAsync("/service-b/info");
        var result = await response.Content.ReadAsStringAsync();
        return Ok(result);
    }

    [HttpGet("get-info-c-from-b")]
    public async Task<IActionResult> GetInfoCFromB()
    {
        HttpClient client = _httpClientFactory.CreateClient("client-b");
        var response = await client.GetAsync("/service-b/get-info-c");
        var result = await response.Content.ReadAsStringAsync();
        return Ok(result);
    }
}
