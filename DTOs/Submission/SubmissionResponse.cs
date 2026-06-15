using TraineeManagement.api.Models;

namespace TraineeManagement.api.DTOs
{
    public class SubmissionResponse
    {
        public int Id { get; set; }

        public int TaskAssignmentId { get; set; }

        public string SubmissionUrl { get; set; } = null!;

        public string Notes { get; set; } = null!;

        public DateTime SubmittedDate { get; set; }

        public DateTime DueDate { get; set; }

        public string Status { get; set; } = null!;

        public SubmissionResponse(Submission submission)
        {
            Id = submission.Id;
            TaskAssignmentId = submission.TaskAssignmentId;
            SubmissionUrl = submission.SubmissionUrl;
            Notes = submission.Notes;
            SubmittedDate = submission.SubmittedDate;
            Status = submission.Status;
        }

        public SubmissionResponse(){}
    }
}
