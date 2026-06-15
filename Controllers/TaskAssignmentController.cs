using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using Microsoft.AspNetCore.Authorization;

namespace TraineeManagement.api.Controllers;

[ApiController]
[Authorize]
[Route("/api/task-assignments")]
public class TaskAssignmentController : ControllerBase
{
    private readonly TaskAssignmentService _service;

    public TaskAssignmentController(TaskAssignmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        List<TaskAssignmentResponse> taskAssignmentResponses = await _service.GetAllAsync();
        return Ok(taskAssignmentResponses);
    }

    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetByIdAsync(int Id)
    {
        return Ok(await _service.GetByIdAsync(Id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateTaskAssignmentRequest createTaskAssignment)
    {
        TaskAssignmentResponse? taskAssignmentResponse = await _service.CreateAsync(createTaskAssignment);
        return taskAssignmentResponse==null ? BadRequest("Due date must be greater than Assigned date") : Created($"/api/task-assignments/{taskAssignmentResponse.Id}", taskAssignmentResponse);
    }

    [HttpPut("{Id:int}/status")]
    public async Task<IActionResult> UpdateAsync(int Id, UpdateTaskAssignmentRequest updateTaskAssignment)
    {
        return Ok(await _service.UpdateAsync(Id, updateTaskAssignment));
    }
}
