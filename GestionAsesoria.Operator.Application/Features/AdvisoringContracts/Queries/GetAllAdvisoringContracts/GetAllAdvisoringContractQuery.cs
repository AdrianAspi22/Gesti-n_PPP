using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.AdvisoringContracts.Response;
using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Shared.Wrapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Features.AdvisoringContracts.Queries.GetAllAdvisoringContracts
{
    public class GetAllAdvisoringContractQuery : IRequest<Result<List<GetAllAdvisoringContractResponse>>>
    {
        public GetAllAdvisoringContractParameters parameters { get; set; } = new GetAllAdvisoringContractParameters();
        
        // Propiedades para compatibilidad con el código existente
        public string SearchString => parameters?.SearchString;
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
            // Usar la propiedad SearchString que obtiene su valor de parameters
            var contracts = await _unitOfWork.AdvisoringContractRepository.GetAllAdvisoringContractsAsync(request.SearchString);
            return await Result<List<GetAllAdvisoringContractResponse>>.SuccessAsync(contracts);
        }
    }
}
