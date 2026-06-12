using System.ComponentModel.DataAnnotations;

namespace TraineeManagement.api.DTOs
{
    public class UpdateMentorRequest
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "Max length 50 characters")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Max length 50 characters")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Valid email is required")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Expertise is required")]
        public string Expertise { get; set; } = null!;

        [Required(ErrorMessage = "Status is required")]
        [AllowedValues(["Active","Inactive"], ErrorMessage = "Invalid status")]
        public string Status { get; set; } = null!;
    }
}
