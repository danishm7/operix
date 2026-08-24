namespace Operix.Application.Features.Authentication;

public sealed record LoginRequest(
    string Email,
    string Password);

public sealed record LoginResponse(
    string AccessToken);