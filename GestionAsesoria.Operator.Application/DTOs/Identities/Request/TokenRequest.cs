using System.ComponentModel.DataAnnotations;

namespace GestionAsesoria.Operator.Application.DTOs.Identities.Request
{
    public class TokenRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}