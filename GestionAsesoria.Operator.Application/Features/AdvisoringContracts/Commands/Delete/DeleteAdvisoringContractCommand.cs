using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Application.Interfaces.Services;
using GestionAsesoria.Operator.Shared.Static;
using GestionAsesoria.Operator.Shared.Wrapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Features.AdvisoringContracts.Commands.Delete
{
    public class DeleteAdvisoringContractCommand : IRequest<Result<int>>
    {
        public int AdvisoringContractId { get; set; }
    }

    internal class DeleteAdvisoringContractCommandHandler : IRequestHandler<DeleteAdvisoringContractCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        public DeleteAdvisoringContractCommandHandler(IUnitOfWork<int> unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Result<int>> Handle(DeleteAdvisoringContractCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var AdvisoringContract = await _unitOfWork.AdvisoringContractRepository.GetByIdAsync(request.AdvisoringContractId);

                if (AdvisoringContract is null)
                {
                    return await Result<int>.FailAsync(ReplyMessage.MESSAGE_QUERY_EMPTY);
                }

                await _unitOfWork.AdvisoringContractRepository.DeleteAsync(AdvisoringContract);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(AdvisoringContract.Id, ReplyMessage.MESSAGE_DELETE);
            }
            catch (Exception)
            {
                return await Result<int>.FailAsync(ReplyMessage.MESSAGE_EXCEPTION);
            }
        }
    }
}
