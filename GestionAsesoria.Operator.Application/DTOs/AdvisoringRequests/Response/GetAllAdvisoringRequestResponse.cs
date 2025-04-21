using System;

namespace GestionAsesoria.Operator.Application.DTOs.AdvisoringRequests.Response
{
    public class GetAllAdvisoringRequestResponse
    {
        public int Id { get; set; }
        public string UserSubject { get; set; }
        public string UserMessage { get; set; }
        public DateTime DateRequest { get; set; }
        public int AdvisoringRequestStatus { get; set; }
        public string StatusName { get; set; }
        
        // Información del solicitante
        public int RequesterActorId { get; set; }
        public string RequesterName { get; set; }
        public string RequesterLastName { get; set; }
        public string RequesterEmail { get; set; }
        
        // Información del asesor
        public int AdvisorActorId { get; set; }
        public string AdvisorName { get; set; }
        public string AdvisorLastName { get; set; }
        public string AdvisorEmail { get; set; }
        
        // Información del tipo de servicio
        public int ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
    }
} 