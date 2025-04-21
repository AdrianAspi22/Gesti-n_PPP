using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Representa un contrato de tesis, el cual representa una tabla intermedia donde incluye referencias a la tesis y al contrato de asesoría asociados.")]
    public class ThesisByAdvisoringContract : AuditableEntity<int>
    {
        [Comment("Identificador de la tesis asociada.")]
        public int ThesisId { get; set; }

        [Comment("Identificador del contrato de asesoría asociado.")]
        public int AdvisoringContractId { get; set; }

        [Comment("Este atributo nos permite verificar si el usuario cuenta con un contrato de Tesis activo")]
        public bool IsActived { get; set; }

        [Comment("Tesis asociada al contrato de tesis.")]
        public virtual Thesis Thesis { get; set; }

        [Comment("Contrato de asesoría asociado al contrato de tesis.")]
        public virtual AdvisoringContract AdvisoringContract { get; set; }
    }
}
