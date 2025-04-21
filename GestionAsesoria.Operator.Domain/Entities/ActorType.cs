using GestionAsesoria.Operator.Domain.Auditable;
using System.Collections.Generic;

namespace GestionAsesoria.Operator.Domain.Entities
{
    public class ActorType : AuditableEntity<int>
    {
        public ActorType()
        {
            RoleByActorTypes = new HashSet<RoleByActorType>();
        }
        public string Name { get; set; }
        public string FirstLabel { get; set; }
        public string SecondLabel { get; set; }
        public string? ThirdLabel { get; set; }

        public virtual ICollection<RoleByActorType> RoleByActorTypes { get; set; }

    }
}
