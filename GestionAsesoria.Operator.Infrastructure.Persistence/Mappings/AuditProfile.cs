using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.Audits.Response;
using GestionAsesoria.Operator.Domain.Entities.Audit;

namespace GestionAsesoria.Operator.Infrastructure.Mappings
{
    public class AuditProfile : Profile
    {
        public AuditProfile()
        {
            CreateMap<AuditResponse, Audit>().ReverseMap();
        }
    }
}