using System;

namespace GestionAsesoria.Operator.Application.DTOs.GroupMember.Response
{
    public class CoordinatorHistoryResponseDto
    {
        public int GroupMemberId { get; set; }
        public string ActorFirstName { get; set; }
        public string ActorLastName { get; set; }
        // public ActorType ActorType { get; set; }
        public string ResearchGroupName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
