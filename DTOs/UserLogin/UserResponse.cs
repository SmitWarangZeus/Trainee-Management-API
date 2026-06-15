using TraineeManagement.api.Models;

namespace TraineeManagement.api.DTOs
{
    public class UserResponse
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Role { get; set; } = null!;

        public UserResponse(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Role = user.Role;
        }

        public UserResponse(){}
    }
}
