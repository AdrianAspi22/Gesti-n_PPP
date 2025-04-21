using System;

namespace GestionAsesoria.Operator.Application.DTOs.MemberGroup.Response
{
    public class MemberGroupSelectResponseDto
    {
        public int MemberGroupId { get; set; }
        public string ActorFirstName { get; set; }
        public string ActorLastName { get; set; }
        public string RoleActorType { get; set; }
        public string AcronymGroup { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
