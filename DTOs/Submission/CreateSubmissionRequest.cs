using System.ComponentModel.DataAnnotations;

namespace TraineeManagement.api.DTOs
{
    public class CreateSubmissionRequest
    {
        [Required(ErrorMessage = "TaskAssignmentId is required")]
        public int TaskAssignmentId { get; set; }

        [Required(ErrorMessage = "SubmissionUrl is required")]
        public string SubmissionUrl { get; set; } = null!;

        [Required(ErrorMessage = "Notes is required")]
        [StringLength(200, ErrorMessage = "Max length 200 characters")]
        public string Notes { get; set; } = null!;

        [Required(ErrorMessage = "Submitted date is required")]
        public DateTime SubmittedDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [AllowedValues(["Submitted","Resubmitted"], ErrorMessage = "Invalid status")]
        public string Status { get; set; } = null!;
    }
}
