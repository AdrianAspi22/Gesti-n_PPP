using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.AdvisoringContracts.Response;
using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Domain.Entities;
using GestionAsesoria.Operator.Shared.Static;
using GestionAsesoria.Operator.Shared.Wrapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Features.AdvisoringContracts.Queries.GetByIdAdvisoringContracts
{
    public class GetAdvisoringContractByIdQuery : IRequest<Result<AdvisoringContractByIdResponseDto>>
    {
        public int advisoringContractId { get; set; }
    }
    internal class GetAdvisoringContractByIdQueryHandler : IRequestHandler<GetAdvisoringContractByIdQuery, Result<AdvisoringContractByIdResponseDto>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetAdvisoringContractByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AdvisoringContractByIdResponseDto>> Handle(GetAdvisoringContractByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var advisoringContract = await _unitOfWork.Repository<AdvisoringContract>().GetByIdAsync(request.advisoringContractId);

                if (advisoringContract is not null)
                {
                    var response = _mapper.Map<AdvisoringContractByIdResponseDto>(advisoringContract);

                    return await Result<AdvisoringContractByIdResponseDto>.SuccessAsync(response, ReplyMessage.MESSAGE_QUERY);
                }
                else
                {
                    return await Result<AdvisoringContractByIdResponseDto>.FailAsync(ReplyMessage.MESSAGE_QUERY_EMPTY);
                }
            }
            catch (Exception)
            {
                return await Result<AdvisoringContractByIdResponseDto>.FailAsync(ReplyMessage.MESSAGE_EXCEPTION);
            }
        }
    }
}
