using System;

namespace GestionAsesoria.Operator.Application.DTOs.GroupMember.Request
{
    public class CreateGroupMemberRequestDto
    {
        public int ActorId { get; set; }
        public int ResearchGroupId { get; set; }
        public int MembershipTypeId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
