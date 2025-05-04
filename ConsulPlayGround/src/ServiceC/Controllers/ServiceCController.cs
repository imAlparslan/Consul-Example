using Microsoft.AspNetCore.Mvc;

namespace ServiceC.Controllers;

[ApiController]
[Route("service-c")]
public class ServiceCController : ControllerBase
{
    [HttpGet("info")]
    public async Task<IActionResult> GetServiceCInfo()
    {
        return await Task.FromResult(Ok(ServiceCInfo.GetServiceInfo));
    }
}
