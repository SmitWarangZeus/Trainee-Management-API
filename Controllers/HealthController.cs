using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

namespace TraineeManagement.api.Controllers;

[ApiController]
[Route("/health")]
public class HealthController : ControllerBase
{
    private readonly HealthCheckService _service;

    public HealthController(HealthCheckService service)
    {
        _service = service;
    }

    [HttpGet("live")]
    public IActionResult Get()
    {
        return Ok(new {
            Status = "running",
            Application = "Trainee Management API",
            Timestamp = DateTime.UtcNow,
        });
    }

    [HttpGet("ready")]
    public async Task GetAsync()
    {
        var report = await _service.CheckHealthAsync();

        Response.StatusCode = report.Status==HealthStatus.Healthy ? StatusCodes.Status200OK : StatusCodes.Status503ServiceUnavailable;

        await UIResponseWriter.WriteHealthCheckUIResponse(HttpContext, report);
    }
}
