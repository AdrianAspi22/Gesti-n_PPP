using System.Collections.Generic;

namespace GestionAsesoria.Operator.Application.DTOs.Identities.Response
{
    public class UserWithSubordinatesAndProspectsResponseDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }

        public IEnumerable<UserWithSubordinatesAndProspectsResponseDto> Subordinates { get; set; }
      
    }
}
