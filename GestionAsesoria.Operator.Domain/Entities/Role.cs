using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GestionAsesoria.Operator.Domain.Entities
{
    public class Role : AuditableEntity<int>
    {
        public Role()
        {
            ActorSecondaryRoles = new HashSet<ActorSecondaryRole>();
            RoleByActorTypes = new HashSet<RoleByActorType>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsDefault { get; set; }
        public int? ParentId { get; set; }
        public virtual Role Parent { get; set; }
        [Comment("Representa los subroles de un Role padre")]
        public virtual ICollection<Role> Children { get; set; }
        [Comment("Servirá para la asignación de 2 a más Roles a un determinado Actor")]
        public virtual ICollection<ActorSecondaryRole> ActorSecondaryRoles { get; set; }

        public virtual ICollection<RoleByActorType> RoleByActorTypes { get; set; }
    }
}
