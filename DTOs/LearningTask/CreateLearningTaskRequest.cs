using System.ComponentModel.DataAnnotations;

namespace TraineeManagement.api.DTOs
{
    public class CreateLearningTaskRequest
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Max length 50 characters")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(200, ErrorMessage = "Max length 50 characters")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "ExpectedTechStack is required")]
        [StringLength(50, ErrorMessage = "Max length 50 characters")]
        public string ExpectedTechStack { get; set; } = null!;

        [Required(ErrorMessage = "DueDate is required")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [AllowedValues(["Draft","Published","Closed"], ErrorMessage = "Invalid status")]
        public string Status { get; set; } = null!;
    }
}
