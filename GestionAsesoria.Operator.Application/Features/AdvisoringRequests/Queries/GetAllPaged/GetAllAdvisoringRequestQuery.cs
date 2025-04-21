using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.AdvisoringRequests.Response;
using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Shared.Wrapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Features.AdvisoringRequests.Queries.GetAllPaged
{
    public class GetAllAdvisoringRequestQuery : IRequest<Result<List<GetAllAdvisoringRequestResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
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
            // Usar el método no paginado y aplicar paginación manualmente
            var allRequests = await _unitOfWork.AdvisoringRequestRepository.GetAllAdvisoringRequestsAsync(request.SearchString);
            
            // Aplicar paginación manualmente
            var pagedRequests = allRequests
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();
                
            return await Result<List<GetAllAdvisoringRequestResponse>>.SuccessAsync(pagedRequests);
        }
    }
}
