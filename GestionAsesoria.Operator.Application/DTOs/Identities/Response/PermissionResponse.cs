using System.Collections.Generic;

namespace GestionAsesoria.Operator.Application.DTOs.Identities.Response
{
    public class PermissionResponse
    {
        public string? RoleId { get; set; }
        public string? RoleName { get; set; }
        public List<RoleClaimResponse>? RoleClaims { get; set; }
    }
}