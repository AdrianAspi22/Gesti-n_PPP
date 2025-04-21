using GestionAsesoria.Operator.Application.DTOs.Actor.Response.ActorResearchGroup;
using GestionAsesoria.Operator.Shared.Wrapper;
using MediatR;
using System.Collections.Generic;

public class GetResearchGroupDetailsQuery : IRequest<Result<List<ActorResearchGroupDetailsDto>>>
{
    public int? GroupId { get; set; }
    public int? LineId { get; set; }
    public int? AreaId { get; set; }
}


//internal class GetResearchGroupDetailsQueryHandler : IRequestHandler<GetResearchGroupDetailsQuery, Result<List<ActorResearchGroupDetailsDto>>>
//{
//    private readonly IUnitOfWork<int> _unitOfWork;
//    private readonly IMessageService _messageService;

//    public GetResearchGroupDetailsQueryHandler(IUnitOfWork<int> unitOfWork, IMessageService messageService)
//    {
//        _unitOfWork = unitOfWork;
//        _messageService = messageService;
//    }

//public async Task<Result<List<ActorResearchGroupDetailsDto>>> Handle(GetResearchGroupDetailsQuery query, CancellationToken cancellationToken)
//{
//    try
//    {
//        var details = await _unitOfWork.ActorRepository.GetFilteredResearchGroupDetailsAsync(query.GroupId, query.LineId, query.AreaId);

//        if (!details.Any())
//        {
//            return await Result<List<ActorResearchGroupDetailsDto>>.SuccessAsync(
//                details,
//                _messageService.GetMessage("ResearchGroup", "Messages", "MESSAGE_NO_RESULTS"));
//        }

//        return await Result<List<ActorResearchGroupDetailsDto>>.SuccessAsync(
//            details,
//            _messageService.GetMessage("ResearchGroup", "Messages", "MESSAGE_DETAILS_SUCCESS"));
//    }
//    catch (Exception ex)
//    {
//        string errorMessage = _messageService.GetMessage("ResearchGroup", "Messages", "MESSAGE_DETAILS_ERROR");
//        return await Result<List<ActorResearchGroupDetailsDto>>.FailAsync($"{errorMessage}: {ex.Message}");
//    }
//}
//}