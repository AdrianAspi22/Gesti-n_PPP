using Azure.Core;
using GestionAsesoria.Operator.Application.DTOs.Files.Request;
using GestionAsesoria.Operator.Application.Interfaces.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Infrastructure.Services
{
    public class FileStorageLocal : IFileStorageLocal
    {
        public async Task<byte[]> GetFile(string container, string route)
        {
            try
            {
                if (string.IsNullOrEmpty(route))
                {
                    return null;
                }

                var fileName = Path.GetFileName(route);

                var directoryFile = Path.Combine("wwwroot", container, fileName);

                if (File.Exists(directoryFile))


                if (!File.Exists(directoryFile))
                {
                    return null;
                }

                using (var stream = new FileStream(directoryFile, FileMode.Open, FileAccess.Read))
                {
                    var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }
        public async Task<string> SaveFile(string container, UploadRequest file,  string scheme, string host)
        {

            try
            {
                if (file.Data == null) return string.Empty;

                var streamData = new MemoryStream(file.Data);

                if (streamData.Length > 0)
                {
                    var extension = Path.GetExtension(file.FileName);

                    var fileName = file.FileName ?? $"file_{Guid.NewGuid()}{extension}";

                    string folder = Path.Combine("wwwroot", container);

                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    string path = Path.Combine(folder, fileName);


                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await streamData.CopyToAsync(stream);
                    }


                    var currentUrl = $"{scheme}://{host}";
                    var pathDb = Path.Combine(currentUrl, container, fileName).Replace("\\", "/");
                    return pathDb;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
           
        }

        public async Task<string> EditFile(string container, UploadRequest file, string route, string scheme, string host)
        {
            await RemoveFile(route, container);

            return await SaveFile(container, file, scheme, host);
        }

        public Task RemoveFile(string route, string container)
        {
            if (string.IsNullOrEmpty(route))
                return Task.CompletedTask;

            var fileName = Path.GetFileName(route);

            var directoryFile = Path.Combine("wwwroot", container, fileName);

            if (File.Exists(directoryFile))
                File.Delete(directoryFile);

            return Task.CompletedTask;
        }
    }
}
