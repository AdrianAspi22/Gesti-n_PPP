using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.Project.Request;
using GestionAsesoria.Operator.Domain.Entities.ProjectIDI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Mappings.Projects
{
    public class ProjectMappingProfile: Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<CreateProjectRequestDto, Project>();
        }
        
    }
}
