using Microsoft.AspNetCore.Mvc;

namespace TraineeManagement.api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TraineeController : ControllerBase
{
    private static List<Trainee> trainees = new List<Trainee>
    {
        new Trainee {Id=0,FirstName="Smit",LastName="Warang",Email="smit@gmail.com",TechStack="CSS",Status="Active",CreatedDate=DateTime.Now,UpdatedDate=DateTime.Now}
    };

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(trainees);
    }

    [HttpGet("{Id:int}")]
    public IActionResult GetById(int Id)
    {
        Trainee trainee = trainees.FirstOrDefault(t => t.Id == Id);
        if (trainee==null)
        {
            return NotFound();
        }
        return Ok(trainee);
    }

    [HttpPost]
    public IActionResult Create(Trainee trainee)
    {
        trainee.Id = trainees.Max(t => t.Id) + 1;
        trainee.CreatedDate = DateTime.Now;
        trainee.UpdatedDate = DateTime.Now;
        trainees.Add(trainee);
        return Ok(trainee);
    }
}
