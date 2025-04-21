using GestionAsesoria.Operator.Application.DTOs.Identities.Response;
using System.Collections.Generic;

namespace GestionAsesoria.Operator.Application.DTOs.Identities.Request
{
    public class UpdateUserRolesRequest
    {
        public int? ActorId { get; set; }
        public string? UserId { get; set; }
        public IList<UserRoleModel>? UserRoles { get; set; }
    }
}