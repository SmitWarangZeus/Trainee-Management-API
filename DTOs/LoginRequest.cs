using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TraineeManagement.api.DTOs
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Max length 50 characters")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [PasswordPropertyText]
        public string Password { get; set; } = null!;
    }
}
