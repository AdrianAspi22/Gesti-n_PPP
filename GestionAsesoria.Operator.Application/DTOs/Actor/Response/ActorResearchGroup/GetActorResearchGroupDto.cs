using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.DTOs.Actor.Response.ActorResearchGroup
{
    public class GetActorResearchGroupDto
    {
        public int Id { get; set; }
        //public string FirstName { get; set; }  
        public string SecondName { get; set; } // Acrónimo del grupo
        //public bool IsActived { get; set; }
        //public int MainRoleId { get; set; }
    }
}
