using GestionAsesoria.Operator.Application.DTOs.Identities.Request;
using FluentValidation;

namespace GestionAsesoria.Operator.Application.Validators.Requests.Identity
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {

            RuleFor(request => request.FirstName).Length(4, 255).WithMessage("The {PropertyName} has {TotalLength} characters. Must be between {MinLength} and {MaxLength} characters.")
               .NotEmpty().WithMessage("{PropertyName} is required")
               .NotNull().WithMessage("{PropertyName} is required");


            RuleFor(request => request.LastName).Length(4, 255).WithMessage("The {PropertyName} has {TotalLength} characters. Must be between {MinLength} and {MaxLength} characters.")
               .NotEmpty().WithMessage("{PropertyName} is required")
               .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(request => request.UserName).Length(4, 255).WithMessage("The {PropertyName} has {TotalLength} characters. Must be between {MinLength} and {MaxLength} characters.")
               .NotEmpty().WithMessage("{PropertyName} is required")
               .NotNull().WithMessage("{PropertyName} is required");

            RuleFor(request => request.Email)
              .NotEmpty().WithMessage("{PropertyName} is required")
              .NotNull().WithMessage("{PropertyName} is required")
              .EmailAddress().WithMessage(x => "{PropertyName} is not correct");

            RuleFor(request => request.Password).Length(8, 16).WithMessage("The {PropertyName} has {TotalLength} characters. Must be between {MinLength} and {MaxLength} characters.")
               .NotEmpty().WithMessage("{PropertyName} is required")
               .NotNull().WithMessage("{PropertyName} is required")
               .Matches(@"[A-Z]").WithMessage("Password must contain at least one capital letter")
               .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
               .Matches(@"[0-9]").WithMessage("Password must contain at least one numeric digit")
               .Matches(@"[@$!%*?&#]").WithMessage("Password must contain at least one special character");

            RuleFor(request => request.ConfirmPassword)
               .NotEmpty().WithMessage("{PropertyName} is required")
               .NotNull().WithMessage("{PropertyName} is required")
               .Equal(request => request.Password).WithMessage(x => "Passwords don't match");
        }
    }
}