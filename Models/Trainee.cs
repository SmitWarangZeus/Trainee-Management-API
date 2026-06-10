using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.DTOs;

namespace TraineeManagement.api.Models;

public class Trainee
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string TechStack { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public Trainee(CreateTraineeRequest createTrainee)
    {
        FirstName = createTrainee.FirstName;
        LastName = createTrainee.LastName;
        Email = createTrainee.Email;
        TechStack = createTrainee.TechStack;
        Status = createTrainee.Status;
        CreatedDate = DateTime.Now;
        UpdatedDate = DateTime.Now;
    }

    public Trainee(){}
}
