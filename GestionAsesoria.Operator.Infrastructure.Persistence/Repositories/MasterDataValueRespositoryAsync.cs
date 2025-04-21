using GestionAsesoria.Operator.Application.DTOs.Generic.Response;
using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Domain.Entities;
using GestionAsesoria.Operator.Infrastructure.Persistence.Contexts;
using GestionAsesoria.Operator.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Infrastructure.Persistence.Repositories
{
    public class MasterDataValueRespositoryAsync : GenericRepositoryAsync<MasterDataValue, int>, IMasterDataValueRepositoryAsync
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<MasterDataValue> _masterDataValue;
        public MasterDataValueRespositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
            _masterDataValue = _context.Set<MasterDataValue>();
        }

        public async Task<MasterDataValue> GetByCodeAsync(string code)
        {
            return await _context.MasterDataValue.FirstOrDefaultAsync(md => md.Code == code && md.IsActived);
        }

        public async Task<List<GetNameAndValueDTO>> GetListMasterDataValue()
        {
            var getAllAsync = await _masterDataValue
           .Where(x => x.IsActived == true)
           .AsNoTracking()
           .Select(x => new GetNameAndValueDTO
           {
               Id = x.Id,
               Code = x.Code,
               Name = x.Name,
               Value = x.Value
           })
           .ToListAsync();
            return getAllAsync;
        }

        public async Task<IEnumerable<GetNameAndValueDTO>> GetMasterDataValuesSelectAsync()
        {
            return await _masterDataValue
                .Select(masterDataValue => new GetNameAndValueDTO
                {
                    Id = masterDataValue.Id,
                    Code = masterDataValue.Code,
                    Name = masterDataValue.Name,
                    Value = masterDataValue.Value,
                })
                .ToListAsync();  // Convierte el resultado a lista
        }

    }
}
