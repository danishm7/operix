namespace Operix.Application.Interfaces;

public interface ICurrentUserService
{
    int? UserId { get; }
    bool IsAuthenticated { get; }
}