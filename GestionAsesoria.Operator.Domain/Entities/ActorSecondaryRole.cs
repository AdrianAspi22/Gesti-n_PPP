using GestionAsesoria.Operator.Domain.Auditable;
using System;

namespace GestionAsesoria.Operator.Domain.Entities
{
    public class ActorSecondaryRole : AuditableEntity<int>
    {
        public int ActorId { get; set; }
        public int RoleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActived { get; set; }
        public virtual Actor Actor { get; set; }
        public virtual Role Role { get; set; }
    }
}
