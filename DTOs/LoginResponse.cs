using TraineeManagement.api.Models;

namespace TraineeManagement.api.DTOs
{
    public class LoginResponse
    {
        public string Token { get; set; } = null!;

        public int ExpiresIn { get; set; }

        public UserResponse UserResponse { get; set; } = null!;

        public LoginResponse(User user, string token)
        {
            Token = token;
            ExpiresIn = 3600;
            UserResponse = new UserResponse(user);
        }

        public LoginResponse(){}
    }
}
