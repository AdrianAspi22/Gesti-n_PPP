using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Representa una tesis, incluyendo detalles sobre el título, asesores y líneas de investigación asociadas.")]
    public class Thesis : AuditableEntity<int>
    {
        public Thesis()
        {
            ThesisStatusHistories = new HashSet<ThesisStatusHistory>();
            ThesisContracts = new HashSet<ThesisByAdvisoringContract>();
        }

        [Comment("Título de la tesis.")]
        public string Title { get; set; }

        [Comment("Identificador del miembro del grupo que es tesista.")]
        public int TesistaMembershipId { get; set; }

        [Comment("Identificador del miembro del grupo que es asesor principal.")]
        public int MainAdvisorMembershipId { get; set; }

        [Comment("Identificador del miembro del grupo que es asesor secundario 1, puede ser nulo.")]
        public int? Secondary1AdvisorMembershipId { get; set; }

        [Comment("Identificador del miembro del grupo que es asesor secundario 2, puede ser nulo.")]
        public int? Secondary2AdvisorMembershipId { get; set; }

        [Comment("Miembro del grupo que es asesor principal.")]
        public virtual Membership MainAdvisorMembership { get; set; }

        [Comment("Miembro del grupo que es asesor secundario 1.")]
        public virtual Membership Secondary1AdvisorMembership { get; set; }

        [Comment("Miembro del grupo que es asesor secundario 2.")]
        public virtual Membership Secondary2AdvisorMembership { get; set; }

        [Comment("Miembro del grupo que es tesista.")]
        public virtual Membership TesistaMembership { get; set; }

        [Comment("Colección de historiales de estado de la tesis.")]
        public virtual ICollection<ThesisStatusHistory> ThesisStatusHistories { get; set; }

        [Comment("Colección de contratos de tesis asociados.")]
        public virtual ICollection<ThesisByAdvisoringContract> ThesisContracts { get; set; }
    }
}
