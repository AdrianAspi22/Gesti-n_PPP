using System;

namespace GestionAsesoria.Operator.Application.DTOs.MemberGroup.Response
{
    public class MemberGroupByIdResponseDto
    {
        public int MemberGroupId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ResearchGroupId { get; set; }
        public int ActorId { get; set; }
        public bool IsActive { get; set; }
    }
}
