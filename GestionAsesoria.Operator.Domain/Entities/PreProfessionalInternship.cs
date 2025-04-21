using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Representa una Práctica Pre Profesional, incluyendo los contratos de Prácticas Pre Profesionales asociadas.")]
    public class PreProfessionalInternship : AuditableEntity<int>
    {
        public PreProfessionalInternship()
        {
            PreProfessionalInternshipContracts = new HashSet<PreProfessionalInternshipByAdvisoringContract>();
        }

        [Comment("Colección de contratos de Prácticas Pre Profesionales asociadas.")]
        public virtual ICollection<PreProfessionalInternshipByAdvisoringContract> PreProfessionalInternshipContracts { get; set; }
    }
}
