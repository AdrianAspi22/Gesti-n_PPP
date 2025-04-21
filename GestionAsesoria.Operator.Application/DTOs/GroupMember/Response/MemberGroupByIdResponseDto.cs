using System;

namespace GestionAsesoria.Operator.Application.DTOs.GroupMember.Response
{
    public class GroupMemberByIdResponseDto
    {
        public int GroupMemberId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ResearchGroupId { get; set; }
        public int ActorId { get; set; }
        public bool IsActive { get; set; }
    }
}
