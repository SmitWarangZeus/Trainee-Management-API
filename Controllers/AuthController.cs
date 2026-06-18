using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;

namespace TraineeManagement.api.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(LoginRequest loginUser)
    {
        UserResponse userResponse = await _service.Register(loginUser);
        return Ok(userResponse);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginUser)
    {
        return Ok(await _service.Login(loginUser));
    }
}
