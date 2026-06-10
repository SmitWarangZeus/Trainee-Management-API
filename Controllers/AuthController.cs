using Microsoft.AspNetCore.Mvc;
using TraineeManagement.api.DTOs;
using TraineeManagement.api.Services;

namespace TraineeManagement.api.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController : ControllerBase
{
    public readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("/register")]
    public async Task<IActionResult> Register(LoginRequest loginUser)
    {
        LoginResponse loginResponse = await _service.Register(loginUser);
        return Ok(loginResponse);
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login(LoginRequest loginUser)
    {
        LoginResponse? loginResponse = await _service.Login(loginUser);
        return (loginResponse==null) ? Unauthorized("Incorrect username or password") : Ok(loginResponse);
    }
}
