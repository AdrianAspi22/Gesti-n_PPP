using GestionAsesoria.Operator.Application.DTOs.Generic.Response;
using GestionAsesoria.Operator.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Interfaces.Repositories
{
    public interface IMasterDataValueRepositoryAsync : IGenericRepositoryAsync<MasterDataValue, int>
    {
        Task<List<GetNameAndValueDTO>> GetListMasterDataValue();
        Task<IEnumerable<GetNameAndValueDTO>> GetMasterDataValuesSelectAsync();
        Task<MasterDataValue> GetByCodeAsync(string code);
    }
}
