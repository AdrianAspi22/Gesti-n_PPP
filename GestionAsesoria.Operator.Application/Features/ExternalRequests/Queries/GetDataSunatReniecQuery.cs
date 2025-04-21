using GestionAsesoria.Operator.Application.DTOs.SunatReniec.Response;
using GestionAsesoria.Operator.Application.Interfaces.Services.ExternalRequest;
using GestionAsesoria.Operator.Shared.Static;
using GestionAsesoria.Operator.Shared.Wrapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Features.ExternalRequests.Queries
{
    public class GetDataSunatReniecQuery : IRequest<Result<SunatReniecResponse>>
    {
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
    }
    internal class GetDataSunatReniecQueryHandler : IRequestHandler<GetDataSunatReniecQuery, Result<SunatReniecResponse>>
    {
        private readonly ISunatReniecService _sunatReniecService;
        public GetDataSunatReniecQueryHandler(ISunatReniecService sunatReniecService)
        {
            _sunatReniecService = sunatReniecService;
        }

        public async Task<Result<SunatReniecResponse>> Handle(GetDataSunatReniecQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _sunatReniecService.GetCompanyData(request.DocumentType, request.DocumentNumber);
              
                if (response.Succeeded)
                {
                    return response;
                }
                else
                {
                    return Result<SunatReniecResponse>.Fail(response.Messages);
                }
            }
            catch (Exception)
            {

                return Result<SunatReniecResponse>.Fail(ReplyMessage.MESSAGE_EXCEPTION);
            }
        }
    }
}
