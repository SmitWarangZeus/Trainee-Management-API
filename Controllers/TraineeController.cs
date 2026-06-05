using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.Models;
using TraineeManagement.api.DTOs;
namespace TraineeManagement.api.Controllers;

[ApiController]
[Route("/api/trainee")]
public class TraineeController : ControllerBase
{
    private static List<Trainee> trainees = new List<Trainee>
    {
        new Trainee {Id=0,FirstName="Smit",LastName="Warang",Email="smit@gmail.com",TechStack="CSS",Status="Active",CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now}
    };

    [HttpGet]
    public IActionResult GetAll()
    {
        var traineesDTO = trainees.Select(t => new TraineeResponse
        {
            FirstName = t.FirstName,
            LastName = t.LastName,
            Email = t.Email,
            TechStack = t.TechStack,
            Status = t.Status,
            CreatedDate = t.CreatedDate,
            UpdatedDate = t.UpdatedDate
        }).ToList();
        return Ok(traineesDTO);
    }

    [HttpGet("{Id:int}")]
    public IActionResult GetById(int Id)
    {
        var trainee = trainees.FirstOrDefault(t => t.Id == Id);
        if (trainee==null)
        {
            return NotFound();
        }
        TraineeResponse traineeDTO = new TraineeResponse
        {
            FirstName = trainee.FirstName,
            LastName = trainee.LastName,
            Email = trainee.Email,
            TechStack = trainee.TechStack,
            Status = trainee.Status,
            CreatedDate = trainee.CreatedDate,
            UpdatedDate = trainee.UpdatedDate
        };
        return Ok(traineeDTO);
    }

    [HttpPost]
    public IActionResult Create(Trainee trainee)
    {
        trainee.Id = trainees.Max(t => t.Id) + 1;
        trainee.CreatedDate = DateTime.Now;
        trainee.UpdatedDate = DateTime.Now;
        trainees.Add(trainee);
        CreateTraineeRequest createdTrainee = new CreateTraineeRequest
        {
            FirstName = trainee.FirstName,
            LastName = trainee.LastName,
            Email = trainee.Email,
            TechStack = trainee.TechStack,
            Status = trainee.Status
        };
        return Ok(createdTrainee);
    }
}
