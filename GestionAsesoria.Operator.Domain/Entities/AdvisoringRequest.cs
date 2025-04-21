using GestionAsesoria.Operator.Domain.Auditable;
using GestionAsesoria.Operator.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Representa una solicitud de asesoría, incluyendo detalles sobre el mensaje del usuario, la respuesta del asesor y el estado de la solicitud.")]
    public class AdvisoringRequest : AuditableEntity<int>
    {
        [Comment("Fecha en la que se realiza la solicitud de asesoría.")]
        public DateTime DateRequest { get; set; }

        [Comment("Fecha en la que se realiza la Respuesta del asesor hacia la petición del Contrato de Asesoría, puede ser nula en una primera instancia")]
        public DateTime? DateResponseAdvisor { get; set; }

        [Comment("Asunto de la razón de la solicitud del usuario")]
        public string UserSubject { get; set; }

        [Comment("Mensaje del usuario explicando la razón de la solicitud.")]
        public string UserMessage { get; set; }

        [Comment("Respuesta del asesor a la solicitud.")]
        public string ResponseAdvisor { get; set; }

        [Comment("Estado actual de la solicitud de asesoría.")]
        public AdvisoringRequestStatus AdvisoringRequestStatus { get; set; }

        // Relaciones con Actor
        public int RequesterActorId { get; set; }
        public Actor RequesterActor { get; set; }
        
        public int AdvisorActorId { get; set; }
        public Actor AdvisorActor { get; set; }
        
        public int UserActorId { get; set; }
        public Actor UserActor { get; set; }
        
        // Referencias a actores que representan grupos, líneas y áreas de investigación
        public int? ResearchGroupId { get; set; }
        public Actor ResearchGroupActor { get; set; }
        
        public int? ResearchLineId { get; set; }
        public Actor ResearchLineActor { get; set; }
        
        public int? ResearchAreaId { get; set; }
        public Actor ResearchAreaActor { get; set; }
        
        // Relación con tipo de servicio
        public int ServiceTypeId { get; set; }
        public MasterDataValue ServiceType { get; set; }
        
        // Relación con contrato de asesoría
        public AdvisoringContract AdvisoringContract { get; set; }
    }
}
