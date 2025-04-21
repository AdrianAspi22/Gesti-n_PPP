using AutoMapper;
using GestionAsesoria.Operator.Application.DTOs.Audits.Response;
using GestionAsesoria.Operator.Application.Interfaces.Services;
using GestionAsesoria.Operator.Infrastructure.Persistence.Contexts;
using GestionAsesoria.Operator.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Infrastructure.Services
{
    public class AuditService : IAuditService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        

        public AuditService(
            IMapper mapper,
            ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<IResult<IEnumerable<AuditResponse>>> GetCurrentUserTrailsAsync(string userId)
        {
            try
            {
                var trails = await _context.AuditTrails.Where(a => a.UserId == userId).OrderByDescending(a => a.Id).Take(250).ToListAsync();
                var mappedLogs = _mapper.Map<List<AuditResponse>>(trails);
                return await Result<IEnumerable<AuditResponse>>.SuccessAsync(mappedLogs);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return await Result<IEnumerable<AuditResponse>>.FailAsync(string.Format("Algo salió mal."));
            }
            
        }

       
    }
}