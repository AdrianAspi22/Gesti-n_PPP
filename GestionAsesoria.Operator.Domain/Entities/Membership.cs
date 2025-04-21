using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Representa un miembro de un grupo de investigación, incluyendo detalles sobre su rol y las tesis asociadas.")]
    public class Membership : AuditableEntity<int>
    {
        public Membership()
        {
            TesistaThesis = new HashSet<Thesis>();
            MainAdvisorThesis = new HashSet<Thesis>();
            Secondary1AdvisorThesis = new HashSet<Thesis>();
            Secondary2AdvisorThesis = new HashSet<Thesis>();
        }

        [Comment("Fecha de inicio de la membresía.")]
        public DateTime StartDate { get; set; }

        [Comment("Fecha de finalización de la membresía, puede ser nula si aún está activo.")]
        public DateTime? EndDate { get; set; }

        // Cambiamos los nombres para evitar conflictos
        public int ActorId { get; set; }
        public virtual Actor MemberActor { get; set; }

        public int OrganizationActorId { get; set; }
        public virtual Actor OrganizationActor { get; set; }

        public int MembershipTypeId { get; set; }
        public virtual MasterDataValue MembershipType { get; set; }

        public bool IsActived { get; set; }

        [Comment("Colección de tesis en las que el miembro es tesista.")]
        public virtual ICollection<Thesis> TesistaThesis { get; set; }

        [Comment("Colección de tesis en las que el miembro es asesor principal.")]
        public virtual ICollection<Thesis> MainAdvisorThesis { get; set; }

        [Comment("Colección de tesis en las que el miembro es asesor secundario 1.")]
        public virtual ICollection<Thesis> Secondary1AdvisorThesis { get; set; }

        [Comment("Colección de tesis en las que el miembro es asesor secundario 2.")]
        public virtual ICollection<Thesis> Secondary2AdvisorThesis { get; set; }
    }
}
