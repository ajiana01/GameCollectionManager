using FluentValidation;
using GameCollectionManager.DTOs;

namespace GameCollectionManager.Validators;

public class CreateGameValidator : AbstractValidator<CreateGameDto>
{
    public CreateGameValidator()
    {
        AddGameRules();
    }

    private void AddGameRules()
    {
        RuleFor(dto => dto.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters");

        RuleFor(dto => dto.ReleaseYear)
            .InclusiveBetween(1950, 2100).WithMessage("Release year must be between 1950 and 2100");

        RuleFor(dto => dto.DeveloperId)
            .GreaterThan(0).WithMessage("Developer is required");

        RuleForEach(dto => dto.GenreIds)
            .GreaterThan(0).WithMessage("Genre ID must be greater than 0");

        RuleForEach(dto => dto.PlatformIds)
            .GreaterThan(0).WithMessage("Platform ID must be greater than 0");
    }
}
