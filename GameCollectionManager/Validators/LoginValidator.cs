using FluentValidation;
using GameCollectionManager.DTOs;

namespace GameCollectionManager.Validators;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(dto => dto.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Please provide a valid email address");

        RuleFor(dto => dto.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}
