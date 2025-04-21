using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.Identities.Request;
using GestionAsesoria.Operator.Application.DTOs.Identities.Response;
using GestionAsesoria.Operator.Domain.Entities.Identity;

namespace GestionAsesoria.Operator.Infrastructure.Mappings
{
    public class RoleClaimProfile : Profile
    {
        public RoleClaimProfile()
        {
            CreateMap<RoleClaimResponse, AcademicRoleClaim>()
                .ForMember(nameof(AcademicRoleClaim.Id), opt => opt.MapFrom(c => c.Id))
                .ForMember(nameof(AcademicRoleClaim.RoleId), opt => opt.MapFrom(c => c.RoleId))
                .ForMember(nameof(AcademicRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(AcademicRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();

            CreateMap<RoleClaimRequest, AcademicRoleClaim>()
                .ForMember(nameof(AcademicRoleClaim.ClaimType), opt => opt.MapFrom(c => c.Type))
                .ForMember(nameof(AcademicRoleClaim.ClaimValue), opt => opt.MapFrom(c => c.Value))
                .ReverseMap();
        }
    }
}