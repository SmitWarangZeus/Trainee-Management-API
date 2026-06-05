using System.ComponentModel.DataAnnotations;

namespace TraineeManagement.api.DTOs
{
    public class CreateTraineeRequest
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "Max length 50 characters")]
        public string FirstName { get; set; } = "";

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Max length 50 characters")]
        public string LastName { get; set; } = "";

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Techstack is required")]
        public string TechStack { get; set; } = "";

        [Required(ErrorMessage = "Name is required")]
        [AllowedValues(["Active","Inactive","Completed"], ErrorMessage = "Invalid status")]
        public string Status { get; set; } = "";
    }
}
