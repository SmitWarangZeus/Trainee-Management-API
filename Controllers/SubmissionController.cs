using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TraineeManagement.api.Controllers;

[ApiController]
[Authorize]
[Route("/api/submissions")]
public class SubmissionController : ControllerBase
{
    private readonly SubmissionService _service;

    private readonly IFileStorageService _fileService;

    public SubmissionController(SubmissionService service, IFileStorageService fileService)
    {
        _service = service;
        _fileService = fileService;
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

    [HttpPost("{SubmissionId:int}/files")]
    public async Task<IActionResult> UploadFileAsync([FromRoute] int SubmissionId, CreateSubmissionFileRequest createSubmissionFile)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        SubmissionFileResponse submissionFileResponse = await _fileService.SaveAsync(SubmissionId, userId, createSubmissionFile);
        return Accepted($"/api/submissions/{submissionFileResponse.Id}/files", submissionFileResponse);
    }
}
