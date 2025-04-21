using GestionAsesoria.Operator.Application.DTOs.Identities.Request;
using FluentValidation;

namespace GestionAsesoria.Operator.Application.Validators.Requests.Identity
{
    public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordRequestValidator()
        {
            RuleFor(request => request.Email)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x =>"Email is required")
                .EmailAddress().WithMessage(x => "Email is not correct");
        }
    }
}