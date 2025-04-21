using System.ComponentModel.DataAnnotations;

namespace GestionAsesoria.Operator.Application.DTOs.Identities.Request
{
    public class UpdateProfileRequest
    {
        public string ProfileId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserLogin { get; set; }
        public int ChargeId { get; set; }
        public string UserId { get; set; }
    }
}