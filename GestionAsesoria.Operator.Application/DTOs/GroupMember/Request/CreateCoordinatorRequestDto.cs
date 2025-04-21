using System;

namespace GestionAsesoria.Operator.Application.DTOs.GroupMember.Request
{
    public class CreateCoordinatorRequestDto
    {
        public int GroupMemberId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
