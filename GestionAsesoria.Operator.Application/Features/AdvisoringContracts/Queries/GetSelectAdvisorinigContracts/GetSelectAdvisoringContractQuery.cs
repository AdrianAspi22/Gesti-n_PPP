using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.AdvisoringContracts.Response;
using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Shared.Static;
using GestionAsesoria.Operator.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Features.AdvisoringContracts.Queries.GetSelectAdvisorinigContracts
{
    public class GetSelectAdvisoringContractQuery : IRequest<Result<IEnumerable<AdvisoringContractSelectResponseDto>>>
    {
    }

    internal class GetSelectAdvisoringContractQueryHandler : IRequestHandler<GetSelectAdvisoringContractQuery, Result<IEnumerable<AdvisoringContractSelectResponseDto>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetSelectAdvisoringContractQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<AdvisoringContractSelectResponseDto>>> Handle(GetSelectAdvisoringContractQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var contracts = await _unitOfWork.AdvisoringContractRepository.GetActiveContractsAsync();
                var response = _mapper.Map<IEnumerable<AdvisoringContractSelectResponseDto>>(contracts);
                if (response.Count() <= 0)
                {
                    return await Result<IEnumerable<AdvisoringContractSelectResponseDto>>.FailAsync(new List<AdvisoringContractSelectResponseDto>(), ReplyMessage.MESSAGE_QUERY_EMPTY);
                }
                return await Result<IEnumerable<AdvisoringContractSelectResponseDto>>.SuccessAsync(response ?? new List<AdvisoringContractSelectResponseDto>(), ReplyMessage.MESSAGE_QUERY);
            }
            catch (Exception)
            {
                return await Result<IEnumerable<AdvisoringContractSelectResponseDto>>.FailAsync(new List<AdvisoringContractSelectResponseDto>(), ReplyMessage.MESSAGE_EXCEPTION);
            }
        }
    }
}
