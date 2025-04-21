using System;

namespace GestionAsesoria.Operator.Application.DTOs.AdvisoringContracts.Response
{
    public class AdvisoringContractDetailResponseDto
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ActorId { get; set; }
        public int ContractStatusId { get; set; }
        public int ServiceTypeId { get; set; }
        public int AdvisoringRequestId { get; set; }
        public bool IsActive { get; set; }
    }
}
