using GestionAsesoria.Operator.Application.DTOs.Files.Request;
using GestionAsesoria.Operator.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Infrastructure.Services
{
    public class UploadService : IUploadService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileStorageLocal _fileStorageLocal;
        public UploadService(IHttpContextAccessor contextAccessor, IFileStorageLocal fileStorageLocal)
        {
            _httpContextAccessor = contextAccessor;
            _fileStorageLocal = fileStorageLocal;
        }

        public async Task<string> SaveFile(string container, UploadRequest file)
        {
            var scheme = _httpContextAccessor.HttpContext!.Request.Scheme;
            var host = _httpContextAccessor.HttpContext.Request.Host;

            return await _fileStorageLocal.SaveFile(container, file, scheme, host.Value);
        }

        public async Task<string> EditFile(string container, UploadRequest file, string route)
        {
            var scheme = _httpContextAccessor.HttpContext!.Request.Scheme;
            var host = _httpContextAccessor.HttpContext.Request.Host;

            return await _fileStorageLocal.EditFile(container, file, route, scheme, host.Value);
        }

        public async Task RemoveFile(string route, string container)
        {

            await _fileStorageLocal.RemoveFile(route, container);
        }

        public async Task<byte[]> GetFile(string container, string route)
        {
            return await _fileStorageLocal.GetFile(container, route);
        }

    }
}