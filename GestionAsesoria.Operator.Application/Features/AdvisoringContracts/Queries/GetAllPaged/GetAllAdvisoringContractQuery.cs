using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.AdvisoringContracts.Response;
using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Shared.Wrapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Features.AdvisoringContracts.Queries.GetAllPaged
{
    public class GetAllAdvisoringContractQuery : IRequest<Result<List<GetAllAdvisoringContractResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
    }

    internal class GetAllAdvisoringContractQueryHandler : IRequestHandler<GetAllAdvisoringContractQuery, Result<List<GetAllAdvisoringContractResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAdvisoringContractQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<List<GetAllAdvisoringContractResponse>>> Handle(GetAllAdvisoringContractQuery request, CancellationToken cancellationToken)
        {
            // Usar el método no paginado y aplicar paginación manualmente
            var allContracts = await _unitOfWork.AdvisoringContractRepository.GetAllAdvisoringContractsAsync(request.SearchString);
            
            // Aplicar paginación manualmente
            var pagedContracts = allContracts
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();
                
            return await Result<List<GetAllAdvisoringContractResponse>>.SuccessAsync(pagedContracts);
        }
    }
}
