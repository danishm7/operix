namespace Operix.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(int userId, string email);
}