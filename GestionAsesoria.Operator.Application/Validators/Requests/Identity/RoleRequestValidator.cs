using GestionAsesoria.Operator.Application.DTOs.Identities.Request;
using FluentValidation;

namespace GestionAsesoria.Operator.Application.Validators.Requests.Identity
{
    public class RoleRequestValidator : AbstractValidator<RoleRequest>
    {
        public RoleRequestValidator()
        {
            RuleFor(request => request.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => "Name is required");
        }
    }
}
