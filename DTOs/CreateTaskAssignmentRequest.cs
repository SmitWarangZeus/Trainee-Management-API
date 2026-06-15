using System.ComponentModel.DataAnnotations;

namespace TraineeManagement.api.DTOs
{
    public class CreateTaskAssignmentRequest
    {
        [Required(ErrorMessage = "TraineeId is required")]
        public int TraineeId { get; set; }

        [Required(ErrorMessage = "MentorId is required")]
        public int MentorId { get; set; }

        [Required(ErrorMessage = "LearningTaskId is required")]
        public int LearningTaskId { get; set; }

        [Required(ErrorMessage = "Assigned date is required")]
        public DateTime AssignedDate { get; set; }

        [Required(ErrorMessage = "Due date is required")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [AllowedValues(["Assigned","InProgress","Submitted","Reviewed","Completed"], ErrorMessage = "Invalid status")]
        public string Status { get; set; } = null!;

        [StringLength(50, ErrorMessage = "Max length 50 characters")]
        public string Remarks { get; set; } = null!;
    }
}
