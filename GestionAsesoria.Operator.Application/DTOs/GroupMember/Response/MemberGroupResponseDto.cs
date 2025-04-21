using System;

namespace GestionAsesoria.Operator.Application.DTOs.GroupMember.Response
{
    public class GroupMemberResponseDto
    {
        public int Id { get; set; }
        public string ActorCode { get; set; }
        public string ActorFirstName { get; set; }
        public string ActorLastName { get; set; }
        public string ActorType { get; set; }
        public string MembershipType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ResearchGroupId { get; set; }
        public int ActorId { get; set; }
        public bool IsActive { get; set; }
        public string StateGroupMember { get; set; }
    }
}
