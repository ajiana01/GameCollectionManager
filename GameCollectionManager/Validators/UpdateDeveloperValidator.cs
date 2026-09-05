using FluentValidation;
using GameCollectionManager.DTOs;

namespace GameCollectionManager.Validators;

public class UpdateDeveloperValidator : AbstractValidator<UpdateDeveloperDto>
{
    public UpdateDeveloperValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Developer name is required")
            .MaximumLength(200).WithMessage("Developer name cannot exceed 200 characters");

        RuleFor(dto => dto.Location)
            .MaximumLength(200).WithMessage("Location cannot exceed 200 characters")
            .When(dto => !string.IsNullOrWhiteSpace(dto.Location));
    }
}
