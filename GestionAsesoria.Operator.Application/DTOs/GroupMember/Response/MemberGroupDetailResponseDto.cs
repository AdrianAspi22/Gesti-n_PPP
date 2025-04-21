using System;

namespace GestionAsesoria.Operator.Application.DTOs.GroupMember.Response
{
    public class GroupMemberDetailResponseDto
    {
        public int Id { get; set; }
        public string RoleFirstName { get; set; }
        public string RoleLastName { get; set; }
        public string ActorType { get; set; }
        public string MembershipType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
