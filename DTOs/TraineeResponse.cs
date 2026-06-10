using TraineeManagement.api.Models;

namespace TraineeManagement.api.DTOs
{
    public class TraineeResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string TechStack { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public TraineeResponse(Trainee trainee)
        {
            Id = trainee.Id;
            FirstName = trainee.FirstName;
            LastName = trainee.LastName;
            Email = trainee.Email;
            TechStack = trainee.TechStack;
            Status = trainee.Status;
            CreatedDate = trainee.CreatedDate;
            UpdatedDate = trainee.UpdatedDate;
        }

        public TraineeResponse(){}
    }
}
