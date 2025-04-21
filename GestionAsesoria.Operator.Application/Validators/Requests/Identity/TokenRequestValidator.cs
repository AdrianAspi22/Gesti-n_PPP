using GestionAsesoria.Operator.Application.DTOs.Identities.Request;
using FluentValidation;

namespace GestionAsesoria.Operator.Application.Validators.Requests.Identity
{
    public class TokenRequestValidator : AbstractValidator<TokenRequest>
    {
        public TokenRequestValidator()
        {
            RuleFor(request => request.UserName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => "Email is required");
            RuleFor(request => request.Password)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => "Password is required!");
        }
    }
}
