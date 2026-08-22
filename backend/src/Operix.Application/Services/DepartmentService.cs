using Operix.Application.DTOs.Department;
using Operix.Application.Exceptions;
using Operix.Application.Interfaces.Persistence;
using Operix.Domain.Entities;

namespace Operix.Application.Services;

public sealed class DepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
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
        await _departmentRepository.SaveChangesAsync(cancellationToken);

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
        await _departmentRepository.SaveChangesAsync(cancellationToken);

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
        await _departmentRepository.SaveChangesAsync(cancellationToken);
    }

    private static DepartmentDto MapToDto(Department department)
    {
        return new DepartmentDto
        {
            Id = department.Id,
            OrganizationId = department.OrganizationId,
            ParentDepartmentId = department.ParentDepartmentId,
            Name = department.Name,
            Code = department.Code,
            IsActive = department.IsActive
        };
    }
}