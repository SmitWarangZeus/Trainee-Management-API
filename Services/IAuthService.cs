using TraineeManagement.api.DTOs;

namespace TraineeManagement.api.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Register(LoginRequest loginUser);

        Task<LoginResponse?> Login(LoginRequest loginUser);
    }
}
