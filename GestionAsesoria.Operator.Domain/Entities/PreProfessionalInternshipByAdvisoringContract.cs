using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Es una tabla intermedia el cual representa un contrato de Práctica Pre Profesional, incluyendo referencias a la Práctica Pre Profesional y al contrato de asesoría asociados.")]
    public class PreProfessionalInternshipByAdvisoringContract : AuditableEntity<int>
    {
        [Comment("Identificador de la Práctica Pre Profesional asociada.")]
        public int PreProfessionalInternshipId { get; set; }

        [Comment("Identificador del contrato de asesoría asociado.")]
        public int AdvisoringContractId { get; set; }

        [Comment("Este atributo nos permite verificar si el usuario cuenta con un contrato de PPP activo")]
        public bool IsActived { get; set; }

        [Comment("Contrato de asesoría asociado al contrato de tutoría.")]
        public virtual AdvisoringContract AdvisoringContract { get; set; }

        [Comment("Práctica Pre Profesional asociada al contrato.")]
        public virtual PreProfessionalInternship PreProfessionalInternship { get; set; }
    }
}
