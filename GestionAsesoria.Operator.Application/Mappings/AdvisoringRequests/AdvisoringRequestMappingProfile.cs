using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.AdvisoringRequest.Response;
using GestionAsesoria.Operator.Application.DTOs.AdvisoringRequests.Response;
using GestionAsesoria.Operator.Domain.Entities;

namespace GestionAsesoria.Operator.Application.Mappings.AdvisoringRequests
{
    public class AdvisoringRequestMappingProfile : Profile
    {
        public AdvisoringRequestMappingProfile()
        {
            CreateMap<AdvisoringRequest, AdvisoringRequestByIdResponseDto>()
                .ForMember(x => x.AdvisoringRequestId, x => x.MapFrom(y => y.Id))
                .ReverseMap();

            CreateMap<AdvisoringRequest, AdvisoringRequestSelectResponseDto>()
                .ForMember(x => x.AdvisoringRequestId, x => x.MapFrom(y => y.Id))
                .ReverseMap();


        }
    }
}
