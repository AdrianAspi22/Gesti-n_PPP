using System.ComponentModel.DataAnnotations;

namespace GestionAsesoria.Operator.Application.DTOs.Identities.Response
{
    public class RoleResponse
    {
        public string? Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}