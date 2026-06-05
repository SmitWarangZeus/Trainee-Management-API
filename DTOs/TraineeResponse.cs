using TraineeManagement.api.Models;

namespace TraineeManagement.api.DTOs
{
    public class TraineeResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = "";

        public string LastName { get; set; } = "";

        public string Email { get; set; } = "";

        public string TechStack { get; set; } = "";

        public string Status { get; set; } = "";

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
