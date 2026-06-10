using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Models;
using TraineeManagement.api.Data;

namespace TraineeManagement.api.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;

        private readonly JwtService _jwtService;

        public AuthService(AppDbContext AppDbContext, JwtService jwtService)
        {
            _appDbContext = AppDbContext;
            _jwtService = jwtService;
        }

        public async Task<LoginResponse> Register(LoginRequest loginUser)
        {
            User user = new User
            {
                Username = loginUser.Username,
                Email = "admin@email.com",
                Role = "admin",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };
            user.SetPassword(loginUser.Password);
            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();
            return new LoginResponse(user);
        }

        public async Task<LoginResponse?> Login(LoginRequest loginUser)
        {
            User? user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Username==loginUser.Username);
            if (user==null)
            {
                return null;
            }
            bool result = BCrypt.Net.BCrypt.Verify(loginUser.Password, user.PasswordHash);
            if (result==false)
            {
                return null;
            }
            string token = _jwtService.GenerateToken(user.Id, user.Username, user.Role);
            return new LoginResponse(user, token);
        }
    }
}
