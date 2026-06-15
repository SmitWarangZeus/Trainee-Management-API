using System.ComponentModel.DataAnnotations;

namespace TraineeManagement.api.DTOs
{
    public class UpdateTaskAssignmentRequest
    {
        [Required(ErrorMessage = "Status is required")]
        [AllowedValues(["Assigned","InProgress","Submitted","Reviewed","Completed"], ErrorMessage = "Invalid status")]
        public string Status { get; set; } = null!;
    }
}
