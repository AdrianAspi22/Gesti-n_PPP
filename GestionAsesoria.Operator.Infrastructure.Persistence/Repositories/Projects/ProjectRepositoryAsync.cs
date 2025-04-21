using GestionAsesoria.Operator.Application.Interfaces.Repositories.Projects;
using GestionAsesoria.Operator.Domain.Entities.ProjectIDI;
using GestionAsesoria.Operator.Infrastructure.Persistence.Contexts;
using GestionAsesoria.Operator.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GestionAsesoria.Operator.Shared.Constants.Permission.Permissions;

namespace GestionAsesoria.Operator.Infrastructure.Persistence.Repositories.Projects
{
    public class ProjectRepositoryAsync : GenericRepositoryAsync<Project, int>, IProjectRepositoryAsync
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Project> _project;
        public ProjectRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _project = _context.Set<Project>();
        }
    }
}
