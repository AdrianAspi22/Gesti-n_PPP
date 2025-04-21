using GestionAsesoria.Operator.Application.DTOs.SunatReniec.Response;
using GestionAsesoria.Operator.Shared.Wrapper;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Interfaces.Services.ExternalRequest
{
    public interface ISunatReniecService
    {
        Task<Result<SunatReniecResponse>> GetCompanyData(string documentType, string documentNumber);
    }
}
