using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.Data;

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

        [Required(ErrorMessage = Constants.STATUS_REQUIRED)]
        [AllowedValues([Constants.STATUS_SUBMITTED,"Resubmitted"], ErrorMessage = Constants.STATUS_INVALID)]
        public string Status { get; set; } = null!;
    }
}
