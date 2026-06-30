using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraineeManagement.api.Models;

public class Submission
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int TaskAssignmentId { get; set; }

    [Required]
    [Column(TypeName = "varchar(200)")]
    public string SubmissionUrl { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(200)")]
    public string Notes { get; set; } = null!;

    public DateTime SubmittedDate { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Status { get; set; } = null!;

    public Submission(CreateSubmissionRequest createSubmission)
    {
        TaskAssignmentId = createSubmission.TaskAssignmentId;
        SubmissionUrl = createSubmission.SubmissionUrl;
        Notes = createSubmission.Notes;
        SubmittedDate = createSubmission.SubmittedDate;
        Status = createSubmission.Status;
    }

    public Submission(){}
}
