using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.DTOs;

namespace TraineeManagement.api.Models;

public class Trainee
{
    public int Id { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "Max length 50 characters")]
    public string FirstName { get; set; } = "";

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, ErrorMessage = "Max length 50 characters")]
    public string LastName { get; set; } = "";

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Techstack is required")]
    public string TechStack { get; set; } = "";

    [Required(ErrorMessage = "Name is required")]
    [AllowedValues(["Active","Inactive","Completed"], ErrorMessage = "Invalid status")]
    public string Status { get; set; } = "";

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
