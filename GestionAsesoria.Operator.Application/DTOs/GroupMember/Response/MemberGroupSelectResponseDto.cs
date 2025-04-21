using System;

namespace GestionAsesoria.Operator.Application.DTOs.GroupMember.Response
{
    public class GroupMemberSelectResponseDto
    {
        public int GroupMemberId { get; set; }
        public string ActorFirstName { get; set; }
        public string ActorLastName { get; set; }
        public int ActorType { get; set; }
        public string MembershipType { get; set; }
        public string AcronymGroup { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
