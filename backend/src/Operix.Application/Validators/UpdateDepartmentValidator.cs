using FluentValidation;
using Operix.Application.DTOs;


namespace Operix.Application.Validators;


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