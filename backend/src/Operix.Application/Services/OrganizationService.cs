using Operix.Application.DTOs;
using Operix.Application.Exceptions;
using Operix.Application.Interfaces.Persistence;
using Operix.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Operix.Application.Services;

public sealed class OrganizationService
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly ILogger<OrganizationService> _logger;
    private readonly OrganizationRoleSetupService _organizationRoleSetupService;
    private readonly IApplicationDbContext _dbContext;

    public OrganizationService(
        IOrganizationRepository organizationRepository,
        ILogger<OrganizationService> logger,
        OrganizationRoleSetupService organizationRoleSetupService,
        IApplicationDbContext dbContext
        )
    {
        _organizationRepository = organizationRepository;
        _logger = logger;
        _organizationRoleSetupService = organizationRoleSetupService;
        _dbContext = dbContext;
    }

    public async Task<OrganizationDto> CreateAsync(CreateOrganizationDto dto, CancellationToken cancellationToken = default)
    {
        var exists = await _organizationRepository.ExistsByCodeAsync(dto.Code, cancellationToken);

        if (exists)
        {
            _logger.LogWarning(
                "Organization creation failed because code '{Code}' already exists.",
                dto.Code);

            throw new ConflictException($"An organization with code '{dto.Code}' already exists.");
        }

        var organization = new Organization(dto.Name, dto.Code);

        await _organizationRepository.AddAsync(organization, cancellationToken);
        await _organizationRoleSetupService.SetupAsync(organization, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Organization created successfully. OrganizationId: {OrganizationId}, Code: {Code}",
            organization.Id,
            organization.Code);

        return new OrganizationDto(
            organization.Id,
            organization.Name,
            organization.Code,
            organization.IsActive);
    }

    public async Task<OrganizationDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var organization = await _organizationRepository.GetByIdAsync(id, cancellationToken);

        if (organization == null)
        {
            return null;
        }

        return new OrganizationDto(
            organization.Id,
            organization.Name,
            organization.Code,
            organization.IsActive);
    }

    public async Task<IReadOnlyList<OrganizationDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var organizations = await _organizationRepository.GetAllAsync(cancellationToken);

        return organizations.Select(o => new OrganizationDto(
            o.Id,
            o.Name,
            o.Code,
            o.IsActive)).ToList();
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

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new OrganizationDto(
            organization.Id,
            organization.Name,
            organization.Code,
            organization.IsActive);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var organization = await _organizationRepository.GetTrackedByIdAsync(id, cancellationToken);

        if (organization is null)
        {
            throw new NotFoundException($"Organization with ID '{id}' does not exist.");
        }

        await _organizationRepository.DeleteAsync(organization, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}