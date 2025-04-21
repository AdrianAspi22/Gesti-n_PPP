using GestionAsesoria.Operator.Domain.Entities.ProjectIDI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Interfaces.Repositories.Projects
{
    public interface IProjectRepositoryAsync : IGenericRepositoryAsync<Project, int> 
    {
    }
}
