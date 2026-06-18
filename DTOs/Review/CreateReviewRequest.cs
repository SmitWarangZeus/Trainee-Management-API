using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.Data;

namespace TraineeManagement.api.DTOs
{
    public class CreateReviewRequest
    {
        [Required(ErrorMessage = "SubmissionId is required")]
        public int SubmissionId { get; set; }

        [Required(ErrorMessage = Constants.MENTORID_REQUIRED)]
        public int MentorId { get; set; }

        [Required(ErrorMessage = "Feedback is required")]
        [StringLength(200, ErrorMessage = "Max length 200 characters")]
        public string Feedback { get; set; } = null!;

        public int Score { get; set; }

        [Required(ErrorMessage = "ReviewStatus is required")]
        [AllowedValues(["Accepted","ChangesRequired","Rejected"], ErrorMessage = "Invalid ReviewStatus")]
        public string ReviewStatus { get; set; } = null!;

        [Required(ErrorMessage = "Reviewed date is required")]
        public DateTime ReviewedDate { get; set; }
    }
}
