using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.AdvisoringContracts.Request;
using GestionAsesoria.Operator.Application.DTOs.AdvisoringContracts.Response;
using GestionAsesoria.Operator.Application.DTOs.AdvisoringRequest.Request;
using GestionAsesoria.Operator.Domain.Entities;
using GestionAsesoria.Operator.Domain.Enums;
using System;

namespace GestionAsesoria.Operator.Application.Mappings.AdvisoringContracts
{
    /// <summary>
    /// Perfil de mapeo de contratos de asesoría.
    /// </summary>
    public class AdvisoringContractMappingProfile : Profile
    {
        public AdvisoringContractMappingProfile()
        {
            CreateMap<CreateAdvisoringContractRequestDto, AdvisoringRequest>()
                .ForMember(dest => dest.DateRequest, opt => opt.MapFrom(src => DateTime.UtcNow.AddHours(-5)))
                .ForMember(dest => dest.UserMessage, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.AdvisoringRequestStatus, opt => opt.MapFrom(src => AdvisoringRequestStatus.PendingRequest));

            CreateMap<CreateAdvisoringContractRequestDto, AdvisoringContract>()
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => DateTime.UtcNow.AddHours(-5)))
                .ForMember(dest => dest.IsActived, opt => opt.MapFrom(src => true));

            CreateMap<RespondToAdvisoringContractRequestDto, AdvisoringRequest>()
                .ForMember(dest => dest.AdvisoringRequestStatus, opt => opt.MapFrom(src => src.ResponseStatus))
                .ForMember(dest => dest.DateResponseAdvisor, opt => opt.MapFrom(src => DateTime.UtcNow.AddHours(-5)))
                .ForMember(dest => dest.ResponseAdvisor, opt => opt.MapFrom(src => src.ResponseAdvisor));

            CreateMap<AdvisoringContract, AdvisoringContractByIdResponseDto>()
                .ForMember(x => x.AdvisoringContractId, x => x.MapFrom(y => y.Id))
                .ReverseMap();

            CreateMap<AdvisoringContract, AdvisoringContractSelectResponseDto>()
            .ForMember(x => x.AdvisoringContractId, x => x.MapFrom(y => y.Id))
            .ReverseMap();
        }
    }
}
