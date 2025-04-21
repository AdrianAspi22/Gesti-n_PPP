using GestionAsesoria.Operator.Application.DTOs.Identities.Request;
using FluentValidation;

namespace GestionAsesoria.Operator.Application.Validators.Requests.Identity
{
    public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
    {
        public UpdateProfileRequestValidator()
        {
            RuleFor(request => request.FirstName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x => "First Name is required");
            RuleFor(request => request.LastName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(x =>"Last Name is required");
        }
    }
}