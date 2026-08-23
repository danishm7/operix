using Operix.Application.Exceptions;
using Operix.Application.Interfaces.Persistence;
using Operix.Domain.Entities;

namespace Operix.Application.Features.Roles;

public sealed class RoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IApplicationDbContext _dbContext;

    public RoleService(IRoleRepository roleRepository, IApplicationDbContext dbContext)
    {
        _roleRepository = roleRepository;
        _dbContext = dbContext;
    }

    public async Task<RoleDto> CreateAsync(CreateRoleDto dto, CancellationToken cancellationToken = default)
    {
        var organizationExists = await _roleRepository.OrganizationExistsAsync(dto.OrganizationId.Value, cancellationToken);

        if (!organizationExists)
        {
            throw new NotFoundException($"Organization with ID '{dto.OrganizationId}' does not exist.");
        }

        var exists = await _roleRepository.ExistsByNameAsync(dto.OrganizationId.Value, dto.Name, cancellationToken);

        if (exists)
        {
            throw new ConflictException($"A role with name '{dto.Name}' already exists in this organization.");
        }

        var role = new Role(dto.OrganizationId, dto.Name, dto.Description);

        await _roleRepository.AddAsync(role, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return MapToDto(role);
    }

    public async Task<RoleDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var role = await _roleRepository.GetByIdAsync(id, cancellationToken);

        return role is null ? null : MapToDto(role);
    }

    public async Task<IReadOnlyList<RoleDto>> GetAllAsync(int organizationId, CancellationToken cancellationToken = default)
    {
        var roles = await _roleRepository.GetAllAsync(organizationId, cancellationToken);

        return [.. roles.Select(MapToDto)];
    }

    public async Task<RoleDto?> UpdateAsync(int id, UpdateRoleDto dto, CancellationToken cancellationToken = default)
    {
        var role = await _roleRepository.GetTrackedByIdAsync(id, cancellationToken);

        if (role is null)
        {
            return null;
        }

        var exists = await _roleRepository.ExistsByNameAsync(role.OrganizationId.Value, dto.Name, cancellationToken);

        if (exists && !string.Equals(role.Name, dto.Name, StringComparison.OrdinalIgnoreCase))
        {
            throw new ConflictException($"A role with name '{dto.Name}' already exists in this organization.");
        }

        role.Update(dto.Name, dto.Description, dto.IsActive);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return MapToDto(role);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var role = await _roleRepository.GetTrackedByIdAsync(id, cancellationToken);

        if (role is null)
        {
            throw new NotFoundException($"Role with ID '{id}' does not exist.");
        }

        await _roleRepository.DeleteAsync(role, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static RoleDto MapToDto(Role role)
    {
        return new RoleDto(
            role.Id,
            role.OrganizationId,
            role.Name,
            role.Description,
            role.IsActive);
    }
}