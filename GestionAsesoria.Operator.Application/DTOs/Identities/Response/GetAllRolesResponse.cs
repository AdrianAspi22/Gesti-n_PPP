using System.Collections.Generic;

namespace GestionAsesoria.Operator.Application.DTOs.Identities.Response
{
    public class GetAllRolesResponse
    {
        public IEnumerable<RoleResponse> Roles { get; set; }
    }
}