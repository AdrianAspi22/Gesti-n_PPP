using GestionAsesoria.Operator.Application.Features.ExternalRequests.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.WebApi.Controllers.v1
{

    public class SunatReniecController : BaseApiController<SunatReniecController>
    {

        [HttpGet("GetData")]
        public async Task<IActionResult> GetData(string documentType, string documentNumber)
        {
            var response = await _mediator.Send(new GetDataSunatReniecQuery() { DocumentType = documentType, DocumentNumber = documentNumber });

            return Ok(response);
        }
    }
}
