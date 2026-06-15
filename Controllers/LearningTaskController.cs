using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using TraineeManagement.api.Models;
using Microsoft.AspNetCore.Authorization;

namespace TraineeManagement.api.Controllers;

[ApiController]
[Authorize]
[Route("/api/learning-tasks")]
public class LearningTaskController : ControllerBase
{
    private readonly ILearningTaskService _service;

    public LearningTaskController(ILearningTaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams paginationParams)
    {
        PagedResponse<LearningTaskResponse> learningTaskResponses = await _service.GetAllAsync(paginationParams);
        return Ok(learningTaskResponses);
    }

    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetByIdAsync(int Id)
    {
        return Ok(await _service.GetByIdAsync(Id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateLearningTaskRequest createLearningTask)
    {
        LearningTaskResponse learningTaskResponse = await _service.CreateAsync(createLearningTask);
        return Created($"api/learningTasks/{learningTaskResponse.Id}", learningTaskResponse);
    }

    [HttpPut("{Id:int}")]
    public async Task<IActionResult> UpdateAsync(int Id, UpdateLearningTaskRequest updateLearningTask)
    {
        return Ok(await _service.UpdateAsync(Id, updateLearningTask));
    }

    [HttpDelete("{Id:int}")]
    public async Task<IActionResult> DeleteAsync(int Id)
    {
        bool response = await _service.DeleteAsync(Id);
        return response ? NoContent() : NotFound();
    }
}
