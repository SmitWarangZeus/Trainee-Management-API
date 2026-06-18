using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.Data;

namespace TraineeManagement.api.DTOs
{
    public class CreateTaskAssignmentRequest
    {
        [Required(ErrorMessage = "TraineeId is required")]
        public int TraineeId { get; set; }

        [Required(ErrorMessage = Constants.MENTORID_REQUIRED)]
        public int MentorId { get; set; }

        [Required(ErrorMessage = "LearningTaskId is required")]
        public int LearningTaskId { get; set; }

        [Required(ErrorMessage = "Assigned date is required")]
        public DateTime AssignedDate { get; set; }

        [Required(ErrorMessage = "Due date is required")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = Constants.STATUS_REQUIRED)]
        [AllowedValues([Constants.STATUS_ASSIGNED,Constants.STATUS_INPROGRESS,Constants.STATUS_SUBMITTED,Constants.STATUS_REVIEWED,Constants.STATUS_COMPLETED], ErrorMessage = Constants.STATUS_INVALID)]
        public string Status { get; set; } = null!;

        [StringLength(50, ErrorMessage = "Max length 50 characters")]
        public string Remarks { get; set; } = null!;
    }
}
