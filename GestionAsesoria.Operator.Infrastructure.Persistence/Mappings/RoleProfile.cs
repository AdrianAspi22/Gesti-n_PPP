using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.Identities.Response;
using GestionAsesoria.Operator.Domain.Entities.Identity;

namespace GestionAsesoria.Operator.Infrastructure.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleResponse, AcademicRole>().ReverseMap();
        }
    }
}