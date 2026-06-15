using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using Microsoft.AspNetCore.Authorization;

namespace TraineeManagement.api.Controllers;

[ApiController]
[Authorize]
[Route("/api/submissions")]
public class SubmissionController : ControllerBase
{
    private readonly SubmissionService _service;

    public SubmissionController(SubmissionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        List<SubmissionResponse> submissionResponses = await _service.GetAllAsync();
        return Ok(submissionResponses);
    }

    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetByIdAsync(int Id)
    {
        return Ok(await _service.GetByIdAsync(Id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateSubmissionRequest createSubmission)
    {
        SubmissionResponse submissionResponse = await _service.CreateAsync(createSubmission);
        return Created($"/api/submissions/{submissionResponse.Id}", submissionResponse);
    }
}
