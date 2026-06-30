using System.ComponentModel.DataAnnotations;

namespace TraineeManagement.api.Models
{
    public class PaginationParams
    {
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be greater than 0")]
        public int PageNumber { get; set; } = 1;

        [Range(1, int.MaxValue, ErrorMessage = "Page size must be greater than 0")]
        public int PageSize { get; set; } = 10;

        public string SearchTerm { get; set; } = null!;

        public string? Status { get; set; }
    }
}