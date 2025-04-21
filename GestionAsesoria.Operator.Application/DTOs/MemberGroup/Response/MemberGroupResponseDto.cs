using System;

namespace GestionAsesoria.Operator.Application.DTOs.MemberGroup.Response
{
    public class MemberGroupResponseDto
    {
        public string ActorCode { get; set; }
        public string ActorFirstName { get; set; }
        public string ActorLastName { get; set; }
        public string RoleActorType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ResearchGroupId { get; set; }
        public int ActorId { get; set; }
        public bool IsActive { get; set; }
        public string StateMemberGroup { get; set; }
    }
}
