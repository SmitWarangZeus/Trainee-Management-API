using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraineeManagement.api.Models;

public class Trainee
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string LastName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string TechStack { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(50)")]
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
        CreatedDate = DateTime.UtcNow;
        UpdatedDate = DateTime.UtcNow;
    }

    public Trainee(){}
}
