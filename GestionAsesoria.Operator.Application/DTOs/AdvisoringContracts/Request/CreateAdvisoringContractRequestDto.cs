using System;

namespace GestionAsesoria.Operator.Application.DTOs.AdvisoringContracts.Request
{
    /// <summary>
    /// DTO para la solicitud de creación de contrato de asesoría.
    /// </summary>
    public class CreateAdvisoringContractRequestDto
    {
        public int EstudianteId { get; set; }
        public int DocenteId { get; set; }
        public int ResearchGroupId { get; set; }
        public int ResearchLineId { get; set; }
        public int ResearchAreaId { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
}
