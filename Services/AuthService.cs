using Microsoft.EntityFrameworkCore;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Models;
using TraineeManagement.api.Data;
using TraineeManagement.api.Exceptions;

namespace TraineeManagement.api.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;

        private readonly JwtService _jwtService;

        private readonly ILogger<AuthService> _logger;

        public AuthService(AppDbContext AppDbContext, JwtService jwtService, ILogger<AuthService> logger)
        {
            _appDbContext = AppDbContext;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<UserResponse> Register(LoginRequest loginUser)
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
            return new UserResponse(user);
        }

        public async Task<LoginResponse?> Login(LoginRequest loginUser)
        {
            User? user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Username==loginUser.Username);
            if (user==null)
            {
                _logger.LogInformation("Username {} not found", loginUser.Username);
                throw new NotFoundException("User not found");
            }
            bool result = BCrypt.Net.BCrypt.Verify(loginUser.Password, user.PasswordHash);
            if (result==false)
            {
                _logger.LogInformation("Password incorrect");
                return null;
            }
            string token = _jwtService.GenerateToken(user.Id, user.Username, user.Role);
            _logger.LogInformation("Login successful");
            return new LoginResponse(user, token);
        }
    }
}
