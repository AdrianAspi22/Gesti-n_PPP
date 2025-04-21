using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Application.Interfaces.Services;
using GestionAsesoria.Operator.Shared.Static;
using GestionAsesoria.Operator.Shared.Wrapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Features.AdvisoringRequests.Commands.Delete
{
    public class DeleteAdvisoringRequestCommand : IRequest<Result<int>>
    {
        public int AdvisoringRequestId { get; set; }
    }

    internal class DeleteAdvisoringRequestCommandHandler : IRequestHandler<DeleteAdvisoringRequestCommand, Result<int>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        public DeleteAdvisoringRequestCommandHandler(IUnitOfWork<int> unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Result<int>> Handle(DeleteAdvisoringRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var AdvisoringRequest = await _unitOfWork.AdvisoringContractRepository.GetByIdAsync(request.AdvisoringRequestId);

                if (AdvisoringRequest is null)
                {
                    return await Result<int>.FailAsync(ReplyMessage.MESSAGE_QUERY_EMPTY);
                }

                await _unitOfWork.AdvisoringContractRepository.DeleteAsync(AdvisoringRequest);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(AdvisoringRequest.Id, ReplyMessage.MESSAGE_DELETE);
            }
            catch (Exception)
            {
                return await Result<int>.FailAsync(ReplyMessage.MESSAGE_EXCEPTION);
            }
        }
    }
}
