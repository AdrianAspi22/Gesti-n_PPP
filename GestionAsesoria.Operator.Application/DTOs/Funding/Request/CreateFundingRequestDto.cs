using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.DTOs.Funding.Request
{
    public class CreateFundingRequestDto
    {
        public decimal Amount { get; set; } // Monto del financiamiento (obligatorio)
        public string FundingType { get; set; } // Tipo de financiamiento (obligatorio)
        public bool IsCompetitiveFund { get; set; } // Indica si es fondo concursable
        public string FundingName { get; set; } // Nombre del fondo (si es concursable)
        public string Organization { get; set; } // Organización asociada (si es concursable)
    }
}
