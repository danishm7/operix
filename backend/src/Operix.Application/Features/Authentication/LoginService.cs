using Operix.Application.Interfaces;
using Operix.Application.Interfaces.Persistence;

namespace Operix.Application.Features.Authentication;

public sealed class LoginService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly ITokenService _tokenService;

    public LoginService(
        IUserRepository userRepository,
        IPasswordHasherService passwordHasherService,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasherService = passwordHasherService;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null || !user.IsActive)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var isPasswordValid = _passwordHasherService.Verify(request.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var accessToken = _tokenService.GenerateToken(user.Id, user.Email);

        return new LoginResponse(accessToken);
    }
}