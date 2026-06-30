using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraineeManagement.api.Models;

public class Mentor
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
    public string Expertise { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(50)")]
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
