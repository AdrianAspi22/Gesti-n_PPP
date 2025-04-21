using System;

namespace GestionAsesoria.Operator.Application.DTOs.MemberGroup.Response
{
    public class MemberGroupDetailResponseDto
    {
        public int Id { get; set; }
        public string RoleFirstName { get; set; }
        public string RoleLastName { get; set; }
        public string RoleActorType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
