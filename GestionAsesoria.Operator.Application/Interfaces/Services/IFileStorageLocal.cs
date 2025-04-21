using GestionAsesoria.Operator.Application.DTOs.Files.Request;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Interfaces.Services
{
    public interface IFileStorageLocal
    {
        Task<byte[]> GetFile(string container, string route);
        Task<string> SaveFile(string container, UploadRequest file, string scheme, string host);
        Task<string> EditFile(string container, UploadRequest file, string route, string scheme, string host);
        Task RemoveFile(string route, string container);
    }
}
