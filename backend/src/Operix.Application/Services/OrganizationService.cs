using Operix.Application.DTOs.Organization;
using Operix.Application.Exceptions;
using Operix.Application.Interfaces.Persistence;
using Operix.Domain.Entities;

namespace Operix.Application.Services;

public sealed class OrganizationService
{
    private readonly IOrganizationRepository _organizationRepository;

    public OrganizationService(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<OrganizationDto> CreateAsync(CreateOrganizationDto dto, CancellationToken cancellationToken = default)
    {
        var exists = await _organizationRepository.ExistsByCodeAsync(dto.Code, cancellationToken);

        if (exists)
        {
            throw new ConflictException($"An organization with code '{dto.Code}' already exists.");
        }

        var organization = new Organization(dto.Name, dto.Code);

        await _organizationRepository.AddAsync(organization, cancellationToken);
        await _organizationRepository.SaveChangesAsync(cancellationToken);

        return new OrganizationDto
        {
            Id = organization.Id,
            Name = organization.Name,
            Code = organization.Code,
            IsActive = organization.IsActive
        };
    }

    public async Task<OrganizationDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var organization = await _organizationRepository.GetByIdAsync(id, cancellationToken);

        if (organization == null)
        {
            return null;
        }

        return new OrganizationDto
        {
            Id = organization.Id,
            Name = organization.Name,
            Code = organization.Code,
            IsActive = organization.IsActive
        };
    }

    public async Task<IReadOnlyList<OrganizationDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var organizations = await _organizationRepository.GetAllAsync(cancellationToken);

        return organizations.Select(o => new OrganizationDto
        {
            Id = o.Id,
            Name = o.Name,
            Code = o.Code,
            IsActive = o.IsActive
        }).ToList();
    }

    public async Task<OrganizationDto?> UpdateAsync(int id, UpdateOrganizationDto dto, CancellationToken cancellationToken = default)
    {
        var organization = await _organizationRepository.GetTrackedByIdAsync(id, cancellationToken);

        if (organization is null)
        {
            return null;
        }

        var codeExists = await _organizationRepository.ExistsByCodeAsync(dto.Code, cancellationToken);

        if (codeExists && organization.Code != dto.Code)
        {
            throw new ConflictException($"An organization with code '{dto.Code}' already exists.");
        }

        organization.Update(dto.Name, dto.Code, dto.IsActive);

        await _organizationRepository.SaveChangesAsync(cancellationToken);

        return new OrganizationDto
        {
            Id = organization.Id,
            Name = organization.Name,
            Code = organization.Code,
            IsActive = organization.IsActive
        };
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var organization = await _organizationRepository.GetTrackedByIdAsync(id, cancellationToken);

        if (organization is null)
        {
            throw new NotFoundException($"Organization with ID '{id}' does not exist.");
        }

        await _organizationRepository.DeleteAsync(organization, cancellationToken);
        await _organizationRepository.SaveChangesAsync(cancellationToken);
    }
}