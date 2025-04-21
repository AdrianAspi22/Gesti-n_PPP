using GestionAsesoria.Operator.Application.DTOs.Funding.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.DTOs.Project.Request
{
    public class CreateProjectRequestDto
    {
        // Datos básicos
        public int AuthorProjectId { get; set; } // ID del Actor relacionado al usuario autenticado
        public string Title { get; set; } // Título del proyecto (obligatorio)
        public DateTime StartDate { get; set; } // Fecha de inicio (obligatorio)
        public DateTime? EndDate { get; set; } // Fecha de finalización (opcional)
        public string ExecutionPlace { get; set; } // Lugar de ejecución (obligatorio)

        // Relaciones obligatorias (IDs)
        public int ResearchGroupProjectId { get; set; } // Grupo de investigación (Actor)
        public int ResearchAreaProjectId { get; set; } // Área de investigación (Actor)
        public int ResearchLineProjectId { get; set; } // Línea de investigación (Actor)
        public int MethodProjectId { get; set; } // Método aplicado (MasterDataValue)
        public int ClassificationProjectId { get; set; } // Clasificación del proyecto (MasterDataValue)

        // Relaciones opcionales
        public int? OdsObjectiveId { get; set; } // Objetivo ODS (MasterDataValue, opcional)
        //public int? StateProjectId { get; set; } // Estado del proyecto (MasterDataValue, opcional)
    }
}
