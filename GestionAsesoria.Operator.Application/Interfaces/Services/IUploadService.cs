using GestionAsesoria.Operator.Application.DTOs.Files.Request;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Interfaces.Services
{
    public interface IUploadService
    {
        // string UploadAsync(UploadRequest request, string folder);
        Task<byte[]> GetFile(string container, string route);
        Task<string> SaveFile(string container, UploadRequest file);
        Task<string> EditFile(string container, UploadRequest file, string route);
        Task RemoveFile(string route, string container);
    }
}