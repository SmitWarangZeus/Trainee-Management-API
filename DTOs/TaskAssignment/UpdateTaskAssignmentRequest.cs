using System.ComponentModel.DataAnnotations;
using TraineeManagement.api.Data;

namespace TraineeManagement.api.DTOs
{
    public class UpdateTaskAssignmentRequest
    {
        [Required(ErrorMessage = Constants.STATUS_REQUIRED)]
        [AllowedValues([Constants.STATUS_ASSIGNED,Constants.STATUS_INPROGRESS,Constants.STATUS_SUBMITTED,Constants.STATUS_REVIEWED,Constants.STATUS_COMPLETED], ErrorMessage = Constants.STATUS_INVALID)]
        public string Status { get; set; } = null!;
    }
}
