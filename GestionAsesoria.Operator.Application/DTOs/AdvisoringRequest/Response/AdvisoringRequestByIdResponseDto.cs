using System;

namespace GestionAsesoria.Operator.Application.DTOs.AdvisoringRequests.Response
{
    public class AdvisoringRequestByIdResponseDto
    {
        public int AdvisoringRequestId { get; set; }
        public DateTime DateRequest { get; set; }
        public DateTime? DateResponseAdvisor { get; set; }
        public string UserMessage { get; set; }
        public string ResponseAdvisor { get; set; }
        public int AdvisoringRequestStatus { get; set; }
        public int ServiceTypeId { get; set; }
        public int AdvisorActorId { get; set; }
        public int UserActorId { get; set; }
        public int RequesterActorId { get; set; }
        public bool IsActive { get; set; }

    }
}
