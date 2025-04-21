using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Es una tabla intermedia el cual representa un contrato de Tutoría, incluyendo referencias a la tutoría y al contrato de asesoría asociados.")]
    public class MentoringByAdvisoringContract : AuditableEntity<int>
    {
        [Comment("Identificador de la tutoría asociada.")]
        public int MentoringId { get; set; }

        [Comment("Identificador del contrato de asesoría asociado.")]
        public int AdvisoringContractId { get; set; }

        [Comment("Este atributo nos permite verificar si el usuario cuenta con un contrato de Tutoría activo")]
        public bool IsActived { get; set; }

        [Comment("Tutoría asociada al contrato.")]
        public virtual Mentoring Mentoring { get; set; }

        [Comment("Contrato de asesoría asociado al contrato de tutoría.")]
        public virtual AdvisoringContract AdvisoringContract { get; set; }
    }
}
