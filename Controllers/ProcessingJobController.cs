using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.Services;
using Microsoft.AspNetCore.Authorization;

namespace TraineeManagement.api.Controllers;

[ApiController]
[Authorize]
[Route("/api/processing-jobs")]
public class ProcessingJobController : ControllerBase
{
    private readonly ProcessingJobService _service;

    public ProcessingJobController(ProcessingJobService service)
    {
        _service = service;
    }

    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetByIdAsync(int Id)
    {
        return Ok(await _service.GetByIdAsync(Id));
    }
}
