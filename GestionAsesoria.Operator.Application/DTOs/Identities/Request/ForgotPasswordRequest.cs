using System.ComponentModel.DataAnnotations;

namespace GestionAsesoria.Operator.Application.DTOs.Identities.Request
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
    }
}