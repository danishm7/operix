using Operix.Domain.Entities;

namespace Operix.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}