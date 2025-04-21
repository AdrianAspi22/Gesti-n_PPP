using GestionAsesoria.Operator.Domain.Enums;
using System;

namespace GestionAsesoria.Operator.Application.DTOs.AdvisoringRequest.Request
{
    public class RespondToAdvisoringContractRequestDto
    {
        public int AdvisoringRequestId { get; set; }
        public AdvisoringRequestStatus ResponseStatus { get; set; }
        public string ResponseAdvisor { get; set; }
        public DateTime? DateResponseAdvisor { get; set; }
    }
}
