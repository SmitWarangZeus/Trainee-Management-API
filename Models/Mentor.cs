using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.DTOs;

namespace TraineeManagement.api.Models;

public class Mentor
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Expertise { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public Mentor(CreateMentorRequest createMentor)
    {
        FirstName = createMentor.FirstName;
        LastName = createMentor.LastName;
        Email = createMentor.Email;
        Expertise = createMentor.Expertise;
        Status = createMentor.Status;
        CreatedDate = DateTime.UtcNow;
        UpdatedDate = DateTime.UtcNow;
    }

    public Mentor(){}
}
