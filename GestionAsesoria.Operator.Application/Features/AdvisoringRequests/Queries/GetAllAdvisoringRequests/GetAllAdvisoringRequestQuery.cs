using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.AdvisoringRequests.Response;
using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Shared.Wrapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Features.AdvisoringRequests.Queries.GetAllAdvisoringRequests
{
    public class GetAllAdvisoringRequestQuery : IRequest<Result<List<GetAllAdvisoringRequestResponse>>>
    {
        public GetAllAdvisoringRequestParameters parameters { get; set; } = new GetAllAdvisoringRequestParameters();
        
        // Propiedades para compatibilidad con el código existente
        public string SearchString => parameters?.SearchString;
    }

    internal class GetAllAdvisoringRequestQueryHandler : IRequestHandler<GetAllAdvisoringRequestQuery, Result<List<GetAllAdvisoringRequestResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAdvisoringRequestQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllAdvisoringRequestResponse>>> Handle(GetAllAdvisoringRequestQuery request, CancellationToken cancellationToken)
        {
            var requests = await _unitOfWork.AdvisoringRequestRepository.GetAllAdvisoringRequestsAsync(request.SearchString);
            return await Result<List<GetAllAdvisoringRequestResponse>>.SuccessAsync(requests);
        }
    }
}
