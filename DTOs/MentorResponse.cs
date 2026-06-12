using TraineeManagement.api.Models;

namespace TraineeManagement.api.DTOs
{
    public class MentorResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Expertise { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public MentorResponse(Mentor mentor)
        {
            Id = mentor.Id;
            FirstName = mentor.FirstName;
            LastName = mentor.LastName;
            Email = mentor.Email;
            Expertise = mentor.Expertise;
            Status = mentor.Status;
            CreatedDate = mentor.CreatedDate;
            UpdatedDate = mentor.UpdatedDate;
        }

        public MentorResponse(){}
    }
}
