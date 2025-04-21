using System.ComponentModel.DataAnnotations;

namespace GestionAsesoria.Operator.Application.DTOs.Identities.Request
{
    public class ChangePasswordRequest
    {
        public string ProfileId { get; set; }
        [Required]
        public string? Password { get; set; }

        [Required]
        public string? NewPassword { get; set; }

        [Required]
        public string? ConfirmNewPassword { get; set; }
    }
}