using FluentValidation;
using GameCollectionManager.DTOs;

namespace GameCollectionManager.Validators;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(dto => dto.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Please provide a valid email address");

        RuleFor(dto => dto.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)")
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, and one number");

        RuleFor(dto => dto.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required")
            .Equal(dto => dto.Password).WithMessage("Passwords do not match");

        RuleFor(dto => dto.FirstName)
            .MaximumLength(100).WithMessage("First name cannot exceed 100 characters")
            .When(dto => !string.IsNullOrWhiteSpace(dto.FirstName));

        RuleFor(dto => dto.LastName)
            .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters")
            .When(dto => !string.IsNullOrWhiteSpace(dto.LastName));
    }
}
