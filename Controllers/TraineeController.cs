using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;
using TraineeManagement.api.Models;
using Microsoft.AspNetCore.Authorization;

namespace TraineeManagement.api.Controllers;

[ApiController]
// [Authorize]
[Route("/api/trainees")]
public class TraineeController : ControllerBase
{
    private readonly ITraineeService _service;

    public TraineeController(ITraineeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery(Name = "search")] PaginationParams paginationParams)
    {
        PagedResponse<TraineeResponse> traineeResponses = await _service.GetAllAsync(paginationParams);
        return Ok(traineeResponses);
    }

    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetByIdAsync(int Id)
    {
        return Ok(await _service.GetByIdAsync(Id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateTraineeRequest createTrainee)
    {
        TraineeResponse traineeResponse = await _service.CreateAsync(createTrainee);
        return Created($"api/trainees/{traineeResponse.Id}", traineeResponse);
    }

    [HttpPut("{Id:int}")]
    public async Task<IActionResult> UpdateAsync(int Id, UpdateTraineeRequest updateTrainee)
    {
        return Ok(await _service.UpdateAsync(Id, updateTrainee));
    }

    [HttpDelete("{Id:int}")]
    public async Task<IActionResult> DeleteAsync(int Id)
    {
        bool response = await _service.DeleteAsync(Id);
        return response ? NoContent() : NotFound();
    }
}
