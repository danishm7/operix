using Microsoft.AspNetCore.Mvc;
using Operix.Application.Features.Authentication;

namespace Operix.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly LoginService _loginService;

    public AuthController(LoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await _loginService.LoginAsync(request, cancellationToken);

        return Ok(response);
    }
}