using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.Data;

namespace TraineeManagement.api.DTOs
{
    public class CreateMentorRequest
    {
        [Required(ErrorMessage = Constants.FIRST_NAME_REQUIRED)]
        [StringLength(50, ErrorMessage = "Max length 50 characters")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = Constants.LAST_NAME_REQUIRED)]
        [StringLength(50, ErrorMessage = "Max length 50 characters")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = Constants.EMAIL_REQUIRED)]
        [EmailAddress(ErrorMessage = Constants.VALID_EMAIL_REQUIRED)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = Constants.EXPERTISE_REQUIRED)]
        public string Expertise { get; set; } = null!;

        [Required(ErrorMessage = Constants.STATUS_REQUIRED)]
        [AllowedValues([Constants.STATUS_ACTIVE,Constants.STATUS_INACTIVE], ErrorMessage = Constants.STATUS_INVALID)]
        public string Status { get; set; } = null!;
    }
}
