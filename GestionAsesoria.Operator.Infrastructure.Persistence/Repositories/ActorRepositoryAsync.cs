using GestionAsesoria.Operator.Application.DTOs.Actor.Response;
using GestionAsesoria.Operator.Application.DTOs.Actor.Response.ActorResearchGroup;
using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Domain.ConfigParameters.Container;
using GestionAsesoria.Operator.Domain.Entities;
using GestionAsesoria.Operator.Infrastructure.Persistence.Contexts;
using GestionAsesoria.Operator.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;

namespace GestionAsesoria.Operator.Infrastructure.Persistence.Repositories
{
    public class ActorRepositoryAsync : GenericRepositoryAsync<Actor, int>, IActorRepositoryAsync
    {
        private readonly SettingsContainer _settingsContainer;
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Actor> _actors;
        private readonly DbSet<Membership> _memberships;

        public ActorRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _settingsContainer = LocalSettingContainer.Get();
            _context = dbContext;
            _actors = _context.Set<Actor>();
            _memberships = _context.Set<Membership>();
        }

        // Consultas LINQ puras
        public async Task<List<Actor>> GetActorsByRoleAndStatusAsync(int roleId, bool isActive)
        {
            return await _actors
                .Where(a => a.MainRoleId == roleId && a.IsActived == isActive)
                .ToListAsync();
        }

        public async Task<Actor> GetActorByIdWithDetailsAsync(int actorId)
        {
            return await _actors
                .Include(a => a.ActorType)
                .Include(a => a.MainRole)
                .Include(a => a.Parent)
                .FirstOrDefaultAsync(a => a.Id == actorId);
        }


        public async Task<List<Actor>> GetActiveResearchGroupsAsync()
        {
            return await _actors
                .Where(a => a.MainRoleId == 11 && a.IsActived) // 11 = Grupo de Investigación
                .ToListAsync();
        }

        public async Task<List<Membership>> GetActiveMembershipsByAdvisorAsync(int advisorId)
        {
            return await _memberships
                .Where(m => m.ActorId == advisorId && m.IsActived)
                .Include(m => m.MemberActor)
                .ToListAsync();
        }

        public async Task<Actor> GetResearchGroupByIdAsync(int? groupId)
        {
            if (!groupId.HasValue)
                return null;

            return await _actors
                .FirstOrDefaultAsync(a => a.Id == groupId.Value &&
                                        a.MainRoleId == 11 &&
                                        a.IsActived);
        }

        public async Task<List<ActorResponseDto>> GetChildActorsByParentAndRoleAsync(int parentId, int roleId)
        {
            return await _actors
                .Where(a => a.ParentId == parentId &&
                           a.MainRoleId == roleId &&
                           a.IsActived)
                .Select(a => new ActorResponseDto
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    MainRoleId = a.MainRoleId,
                    RoleName = a.MainRole.Name,
                    IsActived = a.IsActived
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<Actor>> GetResearchGroupWithDetailsByRolesAsync(int actorId)
        {
            var result = await _actors
                .Where(a =>
                    a.Id == actorId ||
                    (a.ParentId == actorId && a.MainRoleId == 14) ||
                    (a.ParentId == actorId && a.MainRoleId == 15) ||
                    (a.ParentId == actorId && a.MainRoleId == 16))
                .ToListAsync();

            return result;
        }

        //public async Task<List<Membership>> GetMembershipsByFiltersAsync(
        //    int? memberId = null,
        //    int? organizationId = null,
        //    bool? isActive = null)
        //{
        //    var query = _memberships.AsQueryable();

        //    if (memberId.HasValue)
        //        query = query.Where(m => m.ActorId == memberId);

        //    if (organizationId.HasValue)
        //        query = query.Where(m => m.OrganizationActorId == organizationId);

        //    if (isActive.HasValue)
        //        query = query.Where(m => m.IsActived == isActive);

        //    return await query
        //        .Include(m => m.MemberActor)
        //        .ToListAsync();
        //}

        ////public async Task<List<Membership>> GetTeacherMembershipsByGroupAsync(int groupId)
        ////{
        ////    return await _memberships
        ////        .Include(m => m.MemberActor)
        ////        .Where(m => m.OrganizationActorId == groupId &&
        ////                   m.IsActived &&
        ////                   m.MemberActor.MainRoleId == 15 &&
        ////                   m.MemberActor.IsActived)
        ////        .ToListAsync();
        ////}

        // Métodos existentes que necesitamos mantener
        public async Task<List<GetActorResearchGroupDto>> GetResearchGroupsAsync()
        {
            //var researhGroup = _settingsContainer.LocalResearchGroupSettings.ResearchGroupId;
            return await _actors
                 //.Where(a => a.MainRoleId == _settingsContainer.LocalResearchGroupSettings.ResearchGroupId && a.IsActived)
                 .Select(a => new GetActorResearchGroupDto
                 {
                     Id = a.Id,
                     SecondName = a.SecondName
                 })
                 .ToListAsync();
        }

        public async Task<Actor> GetResearchGroupWithDetailsAsync(int researchGroupId, int researchLineId, int researchAreaId, int actorId)
        {
            return await _actors
                .Where(a => a.Id == researchGroupId &&
                           a.MainRoleId == 11 &&
                           a.IsActived)
                .FirstOrDefaultAsync();
        }

        //public async Task<Actor> GetResearchGroupByAdvisorIdAsync(int advisorId)
        //{
        //    var membership = await _memberships
        //        .Where(m => m.ActorId == advisorId && m.IsActived)
        //        .FirstOrDefaultAsync();

        //    if (membership == null)
        //        return null;

        //    return await _actors
        //        .Where(a => a.Id == membership.OrganizationActorId &&
        //                   a.MainRoleId == 11 &&
        //                   a.IsActived)
        //        .FirstOrDefaultAsync();
        //}

        //public async Task<bool> IsStudentMemberOfResearchGroupAsync(int studentId, int researchGroupId)
        //{
        //    return await _memberships
        //        .AnyAsync(m => m.ActorId == studentId &&
        //                      m.OrganizationActorId == researchGroupId &&
        //                      m.IsActived);
        //}

        //public async Task AddStudentToResearchGroupAsync(int studentId, int researchGroupId, int membershipTypeId, int actorTypeId)
        //{
        //    var membership = new Membership
        //    {
        //        ActorId = studentId,
        //        OrganizationActorId = researchGroupId,
        //        MembershipTypeId = membershipTypeId,
        //        StartDate = DateTime.Now,
        //        IsActived = true
        //    };

        //    await _memberships.AddAsync(membership);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<int?> GetStudentCurrentResearchGroupAsync(int studentId)
        //{
        //    var membership = await _memberships
        //        .Where(m => m.ActorId == studentId && m.IsActived)
        //        .Select(m => m.OrganizationActorId)
        //        .FirstOrDefaultAsync();

        //    return membership;
        //}

        //public async Task UpdateStudentResearchGroupAsync(int studentId, int newResearchGroupId)
        //{
        //    var currentMemberships = await _memberships
        //        .Where(m => m.ActorId == studentId && m.IsActived)
        //        .ToListAsync();

        //    foreach (var membership in currentMemberships)
        //    {
        //        membership.IsActived = false;
        //        membership.EndDate = DateTime.Now;
        //    }

        //    var newMembership = new Membership
        //    {
        //        ActorId = studentId,
        //        OrganizationActorId = newResearchGroupId,
        //        StartDate = DateTime.Now,
        //        IsActived = true
        //    };

        //    await _memberships.AddAsync(newMembership);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task AddMembershipAsync(Membership membership)
        //{
        //    await _memberships.AddAsync(membership);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task UpdateMembershipAsync(Membership membership)
        //{
        //    _context.Entry(membership).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //}

        public async Task<Actor> GetActorWithDetailsAsync(int actorId)
        {
            return await _actors
                .Include(a => a.ActorType)
                .Include(a => a.MainRole)
                .Where(a => a.Id == actorId && a.IsActived)
                .FirstOrDefaultAsync();
        }

        //public async Task<List<ActorResearchGroupDetailsDto>> GetFilteredResearchGroupDetailsAsync(int? actorId, int? groupId, int? lineId, int? areaId)
        //{
        //    // Consulta de grupos de investigación (MainRoleId = 11)
        //    var groupsQuery = _actors
        //        .Where(g => g.MainRoleId == 11 && g.IsActived)
        //        .Where(g => !groupId.HasValue || g.Id == groupId.Value)
        //        .Where(g => !actorId.HasValue || g.Id == actorId.Value || g.ParentId == actorId.Value);

        //    // Consulta de líneas de investigación (MainRoleId = 16)
        //    var linesQuery = _actors
        //        .Where(l => l.MainRoleId == 16 && l.IsActived)
        //        .Where(l => !lineId.HasValue || l.Id == lineId.Value)
        //        .Where(l => !actorId.HasValue || l.Id == actorId.Value || l.ParentId == actorId.Value);

        //    // Consulta de áreas de investigación (MainRoleId = 14)
        //    var areasQuery = _actors
        //        .Where(a => a.MainRoleId == 14 && a.IsActived)
        //        .Where(a => !areaId.HasValue || a.Id == areaId.Value)
        //        .Where(a => !actorId.HasValue || a.Id == actorId.Value || a.ParentId == actorId.Value);

        //    // Consulta de docentes (MainRoleId = 15)
        //    var teachersQuery = _actors
        //        .Where(t => t.MainRoleId == 15 && t.IsActived);

        //    // Consulta de membresías
        //    var membershipsQuery = _memberships
        //        .Where(m => m.IsActived);

        //    // Consulta combinada
        //    var result = await (
        //        from group in groupsQuery
        //        join line in linesQuery on group.Id equals line.ParentId
        //join area in areasQuery on line.Id equals area.ParentId
        //join membership in membershipsQuery on group.Id equals membership.OrganizationActorId
        //join teacher in teachersQuery on membership.MemberActorId equals teacher.Id
        //select new ActorResearchGroupDetailsDto
        //{
        //    ResearchGroupId = group.Id,
        //    ResearchGroupAcronym = group.SecondName,
        //    ResearchLineId = line.Id,
        //    ResearchLineName = line.FirstName,
        //    ResearchAreaId = area.Id,
        //    ResearchAreaName = area.FirstName,
        //    TeacherId = teacher.Id,
        //    TeacherFirstName = teacher.FirstName,
        //    TeacherSecondName = teacher.SecondName
        //}).ToListAsync();

        //    return result;
        //}
    }
}
