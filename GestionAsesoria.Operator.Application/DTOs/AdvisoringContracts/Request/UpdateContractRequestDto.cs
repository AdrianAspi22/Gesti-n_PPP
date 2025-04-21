using System;

namespace GestionAsesoria.Operator.Application.DTOs.AdvisoringContracts.Request
{
    public class UpdateContractRequestDto
    {
        public int ContractId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int ContractStatusId { get; set; }
        public string SendEmail { get; set; }
        public int ActorId { get; set; }
    }
}
