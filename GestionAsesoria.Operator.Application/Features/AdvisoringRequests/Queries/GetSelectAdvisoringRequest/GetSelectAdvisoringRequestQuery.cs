using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.AdvisoringRequest.Response;
using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Shared.Static;
using GestionAsesoria.Operator.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Features.AdvisoringRequests.Queries.GetSelectAdvisoringRequest
{
    public class GetSelectAdvisoringRequestQuery : IRequest<Result<IEnumerable<AdvisoringRequestSelectResponseDto>>>
    {
    }

    internal class GetSelectAdvisoringRequestQueryHandler : IRequestHandler<GetSelectAdvisoringRequestQuery, Result<IEnumerable<AdvisoringRequestSelectResponseDto>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetSelectAdvisoringRequestQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<AdvisoringRequestSelectResponseDto>>> Handle(GetSelectAdvisoringRequestQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var requests = await _unitOfWork.AdvisoringRequestRepository.GetPendingAdvisoringRequestsAsync();
                var response = _mapper.Map<IEnumerable<AdvisoringRequestSelectResponseDto>>(requests);
                if (response.Count() <= 0)
                {
                    return await Result<IEnumerable<AdvisoringRequestSelectResponseDto>>.FailAsync(new List<AdvisoringRequestSelectResponseDto>(), ReplyMessage.MESSAGE_QUERY_EMPTY);
                }
                return await Result<IEnumerable<AdvisoringRequestSelectResponseDto>>.SuccessAsync(response ?? new List<AdvisoringRequestSelectResponseDto>(), ReplyMessage.MESSAGE_QUERY);
            }
            catch (Exception)
            {
                return await Result<IEnumerable<AdvisoringRequestSelectResponseDto>>.FailAsync(new List<AdvisoringRequestSelectResponseDto>(), ReplyMessage.MESSAGE_EXCEPTION);
            }
        }
    }
}
