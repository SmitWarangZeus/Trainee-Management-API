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
        TaskAssignmentResponse? taskAssignmentResponse = await _service.GetByIdAsync(Id);
        return taskAssignmentResponse==null ? NotFound() : Ok(taskAssignmentResponse);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateTaskAssignmentRequest createTaskAssignment)
    {
        TaskAssignmentResponse? taskAssignmentResponse = await _service.CreateAsync(createTaskAssignment);
        return taskAssignmentResponse==null ? NotFound("Incorrect Id or date") : Ok(taskAssignmentResponse);
    }

    [HttpPut("{Id:int}/status")]
    public async Task<IActionResult> UpdateAsync(int Id, UpdateTaskAssignmentRequest updateTaskAssignment)
    {
        TaskAssignmentResponse? taskAssignmentResponse = await _service.UpdateAsync(Id, updateTaskAssignment);
        return taskAssignmentResponse==null ? NotFound() : Ok(taskAssignmentResponse);
    }
}
