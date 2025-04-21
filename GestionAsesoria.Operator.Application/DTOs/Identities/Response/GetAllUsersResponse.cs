using System.Collections.Generic;

namespace GestionAsesoria.Operator.Application.DTOs.Identities.Response
{
    public class GetAllUsersResponse
    {
        public IEnumerable<UserResponse>? Users { get; set; }
    }
}