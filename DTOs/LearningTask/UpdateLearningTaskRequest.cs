using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.Data;

namespace TraineeManagement.api.DTOs
{
    public class UpdateLearningTaskRequest
    {
        [Required(ErrorMessage = Constants.TITLE_REQUIRED)]
        [StringLength(50, ErrorMessage = "Max length 50 characters")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = Constants.DESCRIPTION_REQUIRED)]
        [StringLength(200, ErrorMessage = "Max length 200 characters")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = Constants.EXPECTED_TECH_STACK)]
        [StringLength(50, ErrorMessage = "Max length 50 characters")]
        public string ExpectedTechStack { get; set; } = null!;

        [Required(ErrorMessage = Constants.DUEDATE_REQUIRED)]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = Constants.STATUS_REQUIRED)]
        [AllowedValues([Constants.STATUS_DRAFT,Constants.STATUS_PUBLISHED,Constants.STATUS_CLOSED], ErrorMessage = Constants.STATUS_INVALID)]
        public string Status { get; set; } = null!;
    }
}
