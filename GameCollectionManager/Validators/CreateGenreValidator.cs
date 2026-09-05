using FluentValidation;
using GameCollectionManager.DTOs;

namespace GameCollectionManager.Validators;

public class CreateGenreValidator : AbstractValidator<CreateGenreDto>
{
    public CreateGenreValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Genre name is required")
            .MaximumLength(100).WithMessage("Genre name cannot exceed 100 characters");
    }
}
