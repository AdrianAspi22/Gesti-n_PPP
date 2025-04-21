using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.AdvisoringRequests.Response;
using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Domain.Entities;
using GestionAsesoria.Operator.Shared.Static;
using GestionAsesoria.Operator.Shared.Wrapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Features.AdvisoringRequests.Queries.GetByIdAdvisoringRequests
{
    public class GetAdvisoringRequestByIdQuery : IRequest<Result<AdvisoringRequestByIdResponseDto>>
    {
        public int advisoringRequestId { get; set; }
    }
    internal class GetAdvisoringRequestByIdQueryHandler : IRequestHandler<GetAdvisoringRequestByIdQuery, Result<AdvisoringRequestByIdResponseDto>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAdvisoringRequestByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AdvisoringRequestByIdResponseDto>> Handle(GetAdvisoringRequestByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var advisoringRequest = await _unitOfWork.Repository<AdvisoringRequest>().GetByIdAsync(request.advisoringRequestId);

                if (advisoringRequest is not null)
                {
                    var response = _mapper.Map<AdvisoringRequestByIdResponseDto>(advisoringRequest);

                    return await Result<AdvisoringRequestByIdResponseDto>.SuccessAsync(response, ReplyMessage.MESSAGE_QUERY);
                }
                else
                {
                    return await Result<AdvisoringRequestByIdResponseDto>.FailAsync(ReplyMessage.MESSAGE_QUERY_EMPTY);
                }
            }
            catch (Exception)
            {
                return await Result<AdvisoringRequestByIdResponseDto>.FailAsync(ReplyMessage.MESSAGE_EXCEPTION);
            }
        }
    }
}
