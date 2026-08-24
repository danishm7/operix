using Microsoft.Extensions.Logging;
using Operix.Application.Exceptions;
using Operix.Application.Interfaces.Persistence;
using Operix.Domain.Entities;

namespace Operix.Application.Features.Departments;

public sealed class DepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly ILogger<DepartmentService> _logger;
    private readonly IApplicationDbContext _dbContext;

    public DepartmentService(IDepartmentRepository departmentRepository, ILogger<DepartmentService> logger, IApplicationDbContext dbContext)
    {
        _departmentRepository = departmentRepository;
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto, CancellationToken cancellationToken = default)
    {
        var organizationExists = await _departmentRepository.OrganizationExistsAsync(dto.OrganizationId, cancellationToken);

        if (!organizationExists)
        {
            throw new NotFoundException($"Organization with id '{dto.OrganizationId}' does not exist.");
        }

        var exists = await _departmentRepository.ExistsByCodeAsync(dto.OrganizationId, dto.Code, cancellationToken);

        if (exists)
        {
            _logger.LogWarning(
                "Department creation failed because code '{Code}' already exists in OrganizationId: {OrganizationId}.",
                dto.Code,
                dto.OrganizationId);

            throw new ConflictException($"A department with code '{dto.Code}' already exists in this organization.");
        }

        // Validate parent department exists and belongs to the same organization
        if (dto.ParentDepartmentId.HasValue)
        {
            var parentDepartment = await _departmentRepository.GetByIdAsync(dto.ParentDepartmentId.Value, cancellationToken);

            if (parentDepartment == null)
            {
                throw new NotFoundException($"Parent department with ID '{dto.ParentDepartmentId}' does not exist.");
            }

            if (parentDepartment.OrganizationId != dto.OrganizationId)
            {
                throw new ConflictException($"Parent department must belong to the same organization.");
            }
        }

        var department = new Department(dto.OrganizationId, dto.Name, dto.Code);

        await _departmentRepository.AddAsync(department, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Department created successfully. DepartmentId: {DepartmentId}, OrganizationId: {OrganizationId}, Code: {Code}",
            department.Id,
            department.OrganizationId,
            department.Code);

        return MapToDto(department);
    }

    public async Task<DepartmentDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var department = await _departmentRepository.GetByIdAsync(id, cancellationToken);

        return department == null ? null : MapToDto(department);
    }

    public async Task<IReadOnlyList<DepartmentDto>> GetAllAsync(int organizationId, CancellationToken cancellationToken = default)
    {
        var departments = await _departmentRepository.GetAllAsync(organizationId, cancellationToken);

        return departments.Select(MapToDto).ToList();
    }

    public async Task<DepartmentDto?> UpdateAsync(int id, UpdateDepartmentDto dto, CancellationToken cancellationToken = default)
    {
        var department = await _departmentRepository.GetTrackedByIdAsync(id, cancellationToken);

        if (department == null)
        {
            return null;
        }

        var exists = await _departmentRepository.ExistsByCodeAsync(department.OrganizationId, dto.Code, cancellationToken);

        if (exists && !string.Equals(department.Code, dto.Code, StringComparison.OrdinalIgnoreCase))
        {
            throw new ConflictException($"A department with code '{dto.Code}' already exists in this organization.");
        }

        // Validate parent department exists and belongs to the same organization
        if (dto.ParentDepartmentId.HasValue)
        {
            if (dto.ParentDepartmentId.Value == id)
            {
                _logger.LogWarning(
                    "Department update failed because DepartmentId: {DepartmentId} cannot be its own parent.",
                    id);

                throw new ConflictException("A department cannot be its own parent.");
            }

            var parentDepartment = await _departmentRepository.GetByIdAsync(dto.ParentDepartmentId.Value, cancellationToken);

            if (parentDepartment == null)
            {
                throw new NotFoundException($"Parent department with ID '{dto.ParentDepartmentId}' does not exist.");
            }

            if (parentDepartment.OrganizationId != department.OrganizationId)
            {
                throw new ConflictException($"Parent department must belong to the same organization.");
            }
        }

        department.Update(dto.ParentDepartmentId, dto.Name, dto.Code, dto.IsActive);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return MapToDto(department);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var department = await _departmentRepository.GetTrackedByIdAsync(id, cancellationToken);

        if (department == null)
        {
            throw new NotFoundException($"Department with ID '{id}' does not exist.");
        }

        await _departmentRepository.DeleteAsync(department, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static DepartmentDto MapToDto(Department department)
    {
        return new DepartmentDto(
            department.Id,
            department.OrganizationId,
            department.ParentDepartmentId,
            department.Name,
            department.Code,
            department.IsActive);
    }
}