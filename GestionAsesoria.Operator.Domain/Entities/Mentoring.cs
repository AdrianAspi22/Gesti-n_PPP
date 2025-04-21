using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Representa una Tutoría, incluyendo los contratos de tutoría asociadas.")]
    public class Mentoring : AuditableEntity<int>
    {
        public Mentoring()
        {
            MentoringContracts = new HashSet<MentoringByAdvisoringContract>();
        }

        [Comment("Colección de contratos de tutorías asociadas.")]
        public virtual ICollection<MentoringByAdvisoringContract> MentoringContracts { get; set; }
    }
}
