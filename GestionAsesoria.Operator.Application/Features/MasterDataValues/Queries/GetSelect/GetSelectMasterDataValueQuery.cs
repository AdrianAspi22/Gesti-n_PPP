using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.MasterDataValues;
using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Shared.Static;
using GestionAsesoria.Operator.Shared.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Features.MasterDataValues.Queries.GetSelect
{
    public class GetSelectMasterDataValueQuery : IRequest<Result<IEnumerable<MasterDataValueSelectResponseDto>>>
    {
    }
    internal class GetSelectMasterDataValueQueryHandler : IRequestHandler<GetSelectMasterDataValueQuery, Result<IEnumerable<MasterDataValueSelectResponseDto>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        public GetSelectMasterDataValueQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<MasterDataValueSelectResponseDto>>> Handle(GetSelectMasterDataValueQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var masterDataValue = await _unitOfWork.MasterDataValueRepository.GetMasterDataValuesSelectAsync();
                var response = _mapper.Map<IEnumerable<MasterDataValueSelectResponseDto>>(masterDataValue);
                if (!response.Any())
                {
                    return await Result<IEnumerable<MasterDataValueSelectResponseDto>>.FailAsync(new List<MasterDataValueSelectResponseDto>(), ReplyMessage.MESSAGE_QUERY_EMPTY);
                }
                return await Result<IEnumerable<MasterDataValueSelectResponseDto>>.SuccessAsync(response ?? new List<MasterDataValueSelectResponseDto>(), ReplyMessage.MESSAGE_QUERY);
            }
            catch (Exception)
            {
                return await Result<IEnumerable<MasterDataValueSelectResponseDto>>.FailAsync(new List<MasterDataValueSelectResponseDto>(), ReplyMessage.MESSAGE_EXCEPTION);
            }
        }
    }
}
