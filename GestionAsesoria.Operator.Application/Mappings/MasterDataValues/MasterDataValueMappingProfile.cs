using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.MasterDataValues;
using GestionAsesoria.Operator.Domain.Entities;

namespace GestionAsesoria.Operator.Application.Mappings.MasterDataValues
{
    public class MasterDataValueMappingProfile : Profile
    {
        public MasterDataValueMappingProfile()
        {
            CreateMap<MasterDataValue, MasterDataValueSelectResponseDto>()
                .ForMember(x => x.MasterDataValueId, x => x.MapFrom(y => y.Id))
                .ReverseMap();
        }
    }
}
