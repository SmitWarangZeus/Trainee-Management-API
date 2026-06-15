using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.DTOs;

namespace TraineeManagement.api.Models;

public class Submission
{
    [Key]
    public int Id { get; set; }

    public int TaskAssignmentId { get; set; }

    public string SubmissionUrl { get; set; } = null!;

    public string Notes { get; set; } = null!;

    public DateTime SubmittedDate { get; set; }

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
