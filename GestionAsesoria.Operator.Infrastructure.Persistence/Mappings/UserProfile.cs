using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.Identities.Response;
using GestionAsesoria.Operator.Domain.Entities.Identity;

namespace GestionAsesoria.Operator.Infrastructure.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AcademicUser, UserResponse>()
                //.ForMember(x => x.ReportUser, x => x.MapFrom(y => y.User.UserName))
                .ForMember(x => x.UserStatus, x => x.MapFrom(y => y.IsActive ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<AcademicUser, UserByIdResponse>()
               .ReverseMap();


            CreateMap<AcademicUser, UserSelectResponse>()
               .ForMember(x => x.UserFullName, x => x.MapFrom(y => $"{y.FirstName} {y.LastName}"))
               .ReverseMap();

        }
    }
}