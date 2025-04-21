using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Contrato de Asesoria, en el cual se asume responsabilidades entre el asesor y asesorado por un tiempo limitado")]
    public class AdvisoringContract : AuditableEntity<int>
    {
        public AdvisoringContract()
        {
            ThesisContracts = new HashSet<ThesisByAdvisoringContract>();
            MentoringContracts = new HashSet<MentoringByAdvisoringContract>();
            PreProfessionalInternshipContracts = new HashSet<PreProfessionalInternshipByAdvisoringContract>();
            AdvisingSessions = new HashSet<AdvisingSession>();
        }

        [Comment("Asunto, se refiere al título del tema principal a la cual se va a consultar al remitente")]
        public string Subject { get; set; }

        [Comment("Se explica a detalle el motivo por el cual se está realizando el contrato")]
        public string Description { get; set; }

        [Comment("Fecha de registro del contrato")]
        public DateTime RegistrationDate { get; set; }

        [Comment("Fecha de finalización del contrato, puede ser nula si el contrato está en curso")]
        public DateTime? EndDate { get; set; }

        [Comment("Este atributo nos permite verificar si el usuario cuenta con un contrato activo")]
        public bool IsActived { get; set; }

        [Comment("Número de contrato")]
        public string ContractNumber { get; set; }

        // Estado del contrato
        [Comment("Estado actual del contrato, relacionado con MasterDataValue.")]
        public int ContractStatusId { get; set; }
        [Comment("Estado actual del contrato, relacionado con MasterDataValue.")]
        public virtual MasterDataValue ContractStatus { get; set; }

        // Relaciones con Actor para estudiante y asesor
        [Comment("Entidad Actor asociado al contrato de Asesoría")]
        public int StudentId { get; set; }
        [Comment("Entidad Actor asociado al contrato de Asesoría")]
        public virtual Actor StudentActor { get; set; }

        [Comment("Entidad Actor asociado al contrato de Asesoría")]
        public int AdvisorId { get; set; }
        [Comment("Entidad Actor asociado al contrato de Asesoría")]
        public virtual Actor AdvisorActor { get; set; }

        // Referencias a actores que representan grupos, líneas y áreas de investigación
        [Comment("Tipo de contrato, relacionado con MasterDataValue.")]
        public int? ResearchGroupId { get; set; }
        [Comment("Tipo de contrato, relacionado con MasterDataValue.")]
        public virtual Actor ResearchGroupActor { get; set; }

        [Comment("Tipo de contrato, relacionado con MasterDataValue.")]
        public int? ResearchLineId { get; set; }
        [Comment("Tipo de contrato, relacionado con MasterDataValue.")]
        public virtual Actor ResearchLineActor { get; set; }

        [Comment("Tipo de contrato, relacionado con MasterDataValue.")]
        public int? ResearchAreaId { get; set; }
        [Comment("Tipo de contrato, relacionado con MasterDataValue.")]
        public virtual Actor ResearchAreaActor { get; set; }

        // Relación con tipo de servicio
        [Comment("Tipo de contrato, relacionado con MasterDataValue.")]
        public int ServiceTypeId { get; set; }
        [Comment("Tipo de contrato, relacionado con MasterDataValue.")]
        public virtual MasterDataValue ServiceType { get; set; }

        // Relación con solicitud de asesoría
        [Comment("Entidad de Solicitud de Asesoría asociado al contrato de Asesoría")]
        public int? AdvisoringRequestId { get; set; }
        [Comment("Entidad de Solicitud de Asesoría asociado al contrato de Asesoría")]
        public virtual AdvisoringRequest AdvisoringRequest { get; set; }

        [Comment("Colección de contratos de Tesis asociados")]
        public virtual ICollection<ThesisByAdvisoringContract> ThesisContracts { get; set; }

        [Comment("Colección de contratos de Tutoría asociados")]
        public virtual ICollection<MentoringByAdvisoringContract> MentoringContracts { get; set; }

        [Comment("Colección de contratos de Prácticas Pre Profesionales asociados")]
        public virtual ICollection<PreProfessionalInternshipByAdvisoringContract> PreProfessionalInternshipContracts { get; set; }

        [Comment("Colección de sesiones de asesoría asociadas")]
        public virtual ICollection<AdvisingSession> AdvisingSessions { get; set; }
    }
}
