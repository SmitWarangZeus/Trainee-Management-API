using Microsoft.AspNetCore.Mvc;

namespace TraineeManagement.api.Controllers;

[ApiController]
[Route("/api/health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new {
            Status = "running",
            Application = "Trainee Management API",
            Timestamp = DateTime.UtcNow,
        });
    }
}
