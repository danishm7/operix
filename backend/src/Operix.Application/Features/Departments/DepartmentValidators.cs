using FluentValidation;

namespace Operix.Application.Features.Departments;

public sealed class CreateDepartmentValidator : AbstractValidator<CreateDepartmentDto>
{
    public CreateDepartmentValidator()
    {
        RuleFor(x => x.OrganizationId)
            .GreaterThan(0);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ParentDepartmentId)
            .GreaterThan(0)
            .When(x => x.ParentDepartmentId.HasValue);
    }
}

public sealed class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentDto>
{
    public UpdateDepartmentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Code)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ParentDepartmentId)
            .GreaterThan(0)
            .When(x => x.ParentDepartmentId.HasValue);
    }
}