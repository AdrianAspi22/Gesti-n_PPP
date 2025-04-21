using System;

namespace GestionAsesoria.Operator.Application.DTOs.MemberGroup.Request
{
    public class CreateMemberGroupRequestDto
    {
        public int ActorId { get; set; }
        public int ResearchGroupId { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsCoordinator { get; set; }
    }
}
