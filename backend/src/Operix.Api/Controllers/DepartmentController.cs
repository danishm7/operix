using Microsoft.AspNetCore.Mvc;
using Operix.Application.DTOs;
using Operix.Application.Services;

namespace Operix.Api.Controllers;

[ApiController]
[Route("api/departments")]
public sealed class DepartmentsController : ControllerBase
{
    private readonly DepartmentService _departmentService;

    public DepartmentsController(DepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentDto>> Create(CreateDepartmentDto dto, CancellationToken cancellationToken)
    {
        var department = await _departmentService.CreateAsync(dto, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DepartmentDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var department = await _departmentService.GetByIdAsync(id, cancellationToken);

        if (department == null)
        {
            return NotFound();
        }

        return Ok(department);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DepartmentDto>>> GetAll([FromQuery] int organizationId, CancellationToken cancellationToken)
    {
        var departments = await _departmentService.GetAllAsync(organizationId, cancellationToken);

        return Ok(departments);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<DepartmentDto>> Update(int id, UpdateDepartmentDto dto, CancellationToken cancellationToken)
    {
        var department = await _departmentService.UpdateAsync(id, dto, cancellationToken);

        if (department == null)
        {
            return NotFound();
        }

        return Ok(department);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _departmentService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}