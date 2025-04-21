using System;

namespace GestionAsesoria.Operator.Application.DTOs.AdvisoringContracts.Response
{
    public class GetAllAdvisoringContractResponse
    {
        public int Id { get; set; }
        public string ContractNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActived { get; set; }
        public string Status { get; set; }
        
        // Información del estudiante
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentEmail { get; set; }
        
        // Información del asesor
        public int AdvisorId { get; set; }
        public string AdvisorName { get; set; }
        public string AdvisorLastName { get; set; }
        public string AdvisorEmail { get; set; }
        
        // Información del grupo de investigación
        public int ResearchGroupId { get; set; }
        public string ResearchGroupName { get; set; }
        public string ResearchGroupAcronym { get; set; }
        
        // Información de la línea de investigación
        public int ResearchLineId { get; set; }
        public string ResearchLineName { get; set; }
        
        // Información del área de investigación
        public int ResearchAreaId { get; set; }
        public string ResearchAreaName { get; set; }
        
        // Información del tipo de servicio
        public int ServiceTypeId { get; set; }
        public string ServiceTypeName { get; set; }
    }
} 