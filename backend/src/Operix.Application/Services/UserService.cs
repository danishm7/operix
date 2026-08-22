using Operix.Application.DTOs;
using Operix.Application.Exceptions;
using Operix.Application.Interfaces;
using Operix.Application.Interfaces.Persistence;
using Operix.Domain.Entities;

namespace Operix.Application.Services;

public sealed class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasherService _passwordHasherService;

    public UserService(
        IUserRepository userRepository,
        IPasswordHasherService passwordHasherService)
    {
        _userRepository = userRepository;
        _passwordHasherService = passwordHasherService;
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken = default)
    {
        var organizationExists = await _userRepository.OrganizationExistsAsync(dto.OrganizationId, cancellationToken);

        if (!organizationExists)
        {
            throw new NotFoundException($"Organization with ID '{dto.OrganizationId}' does not exist.");
        }

        if (dto.DepartmentId.HasValue)
        {
            var departmentExists = await _userRepository.DepartmentExistsAsync(dto.DepartmentId.Value, dto.OrganizationId, cancellationToken);

            if (!departmentExists)
            {
                throw new NotFoundException($"Department with ID '{dto.DepartmentId}' does not exist or does not belong to the organization.");
            }
        }

        var emailExists = await _userRepository.ExistsByEmailAsync(dto.Email, cancellationToken);

        if (emailExists)
        {
            throw new ConflictException($"A user with email '{dto.Email}' already exists.");
        }

        var passwordHash = _passwordHasherService.Hash(dto.Password);

        var user = new User(
            dto.OrganizationId,
            dto.DepartmentId,
            dto.FirstName,
            dto.LastName,
            dto.Email,
            passwordHash);

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return MapToDto(user);
    }

    public async Task<UserDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        return user is null ? null : MapToDto(user);
    }

    public async Task<IReadOnlyList<UserDto>> GetAllAsync(int organizationId, CancellationToken cancellationToken = default)
    {
        var users = await _userRepository.GetAllAsync(organizationId, cancellationToken);

        return [.. users.Select(MapToDto)];
    }

    public async Task<UserDto?> UpdateAsync(int id, UpdateUserDto dto, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetTrackedByIdAsync(id, cancellationToken);

        if (user is null)
        {
            return null;
        }

        if (dto.DepartmentId.HasValue)
        {
            var departmentExists = await _userRepository.DepartmentExistsAsync(dto.DepartmentId.Value, user.OrganizationId, cancellationToken);

            if (!departmentExists)
            {
                throw new NotFoundException($"Department with ID '{dto.DepartmentId}' does not exist or does not belong to the organization.");
            }
        }

        var emailExists = await _userRepository.ExistsByEmailAsync(dto.Email, cancellationToken);

        if (emailExists && !string.Equals(user.Email, dto.Email, StringComparison.OrdinalIgnoreCase))
        {
            throw new ConflictException($"A user with email '{dto.Email}' already exists.");
        }

        user.Update(dto.DepartmentId, dto.FirstName, dto.LastName, dto.Email, dto.IsActive);

        await _userRepository.SaveChangesAsync(cancellationToken);

        return MapToDto(user);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetTrackedByIdAsync(id, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException($"User with ID '{id}' does not exist.");
        }

        await _userRepository.DeleteAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);
    }

    private static UserDto MapToDto(User user)
    {
        return new UserDto(
            user.Id,
            user.OrganizationId,
            user.DepartmentId,
            user.FirstName,
            user.LastName,
            user.Email,
            user.IsActive);
    }
}