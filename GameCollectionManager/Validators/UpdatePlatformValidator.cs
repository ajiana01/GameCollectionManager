using FluentValidation;
using GameCollectionManager.DTOs;

namespace GameCollectionManager.Validators;

public class UpdatePlatformValidator : AbstractValidator<UpdatePlatformDto>
{
    public UpdatePlatformValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Platform name is required")
            .MaximumLength(100).WithMessage("Platform name cannot exceed 100 characters");
    }
}
