using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;

namespace TraineeManagement.api.Controllers;

[ApiController]
[Route("/api/trainee")]
public class TraineeController : ControllerBase
{
    public readonly ITraineeService _service;

    public TraineeController(ITraineeService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{Id:int}")]
    public IActionResult GetById(int Id)
    {
        TraineeResponse? traineeResponse = _service.GetById(Id);
        return traineeResponse==null ? NotFound() : Ok(traineeResponse);
    }

    [HttpPost]
    public IActionResult Create(CreateTraineeRequest createTrainee)
    {
        TraineeResponse traineeResponse = _service.Create(createTrainee);
        return Ok(traineeResponse);
    }
}
