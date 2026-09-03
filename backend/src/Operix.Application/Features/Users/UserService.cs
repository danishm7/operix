using Operix.Application.Exceptions;
using Operix.Application.Features.Roles;
using Operix.Application.Interfaces;
using Operix.Application.Interfaces.Persistence;
using Operix.Domain.Entities;

namespace Operix.Application.Features.Users;

public sealed class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly IApplicationDbContext _dbContext;

    public UserService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUserRoleRepository userRoleRepository,
        IPasswordHasherService passwordHasherService,
        IApplicationDbContext dbContext)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _passwordHasherService = passwordHasherService;
        _dbContext = dbContext;
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
        await _dbContext.SaveChangesAsync(cancellationToken);

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

        await _dbContext.SaveChangesAsync(cancellationToken);

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
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AssignRolesAsync(int userId, AssignRolesDto dto, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetTrackedByIdAsync(userId, cancellationToken);

        if (user is null)
            throw new NotFoundException($"User with ID '{userId}' does not exist.");

        var roleIds = dto.RoleIds.Distinct().ToList();

        var roles = await _roleRepository.GetByIdsAsync(roleIds, cancellationToken);

        if (roles.Count != roleIds.Count)
            throw new NotFoundException("One or more roles do not exist or are inactive.");

        if (roles.Any(x => x.OrganizationId != user.OrganizationId))
            throw new ConflictException("One or more roles do not belong to the user's organization.");

        var existingUserRoles = await _userRoleRepository.GetByUserIdAsync(userId, cancellationToken);

        foreach (var userRole in existingUserRoles)
        {
            await _userRoleRepository.DeleteAsync(userRole, cancellationToken);
        }

        foreach (var role in roles)
        {
            await _userRoleRepository.AddAsync(new UserRole(userId, role.Id), cancellationToken);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<RoleDto>> GetRolesAsync(int userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

        if (user is null)
            throw new NotFoundException($"User with ID '{userId}' does not exist.");

        var userRoles = await _userRoleRepository.GetByUserIdAsync(userId, cancellationToken);

        return [.. userRoles.Select(x => new RoleDto(
            x.Role.Id,
            x.Role.OrganizationId,
            x.Role.Name,
            x.Role.Description,
            x.Role.IsActive))
        ];
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