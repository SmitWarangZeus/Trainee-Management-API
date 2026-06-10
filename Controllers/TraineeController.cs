using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;

namespace TraineeManagement.api.Controllers;

[ApiController]
[Route("/api/trainees")]
public class TraineeController : ControllerBase
{
    private readonly ITraineeService _service;

    public TraineeController(ITraineeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? search)
    {
        IEnumerable<TraineeResponse> traineeResponses = await _service.GetAllAsync(search);
        return Ok(traineeResponses);
    }

    [HttpGet("{Id:int}")]
    public async Task<IActionResult> GetByIdAsync(int Id)
    {
        TraineeResponse? traineeResponse = await _service.GetByIdAsync(Id);
        return traineeResponse==null ? NotFound() : Ok(traineeResponse);
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
        TraineeResponse? traineeResponse = await _service.UpdateAsync(Id, updateTrainee);
        return traineeResponse==null ? NotFound() : Ok(traineeResponse);
    }

    [HttpDelete("{Id:int}")]
    public async Task<IActionResult> DeleteAsync(int Id)
    {
        bool response = await _service.DeleteAsync(Id);
        return response ? NoContent() : NotFound();
    }
}
