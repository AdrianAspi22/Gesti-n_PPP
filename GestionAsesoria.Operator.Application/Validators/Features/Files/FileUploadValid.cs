using GestionAsesoria.Operator.Application.DTOs.Files.Request;
using System;
using System.IO;
using System.Linq;

namespace GestionAsesoria.Operator.Application.Validators.Features.Files
{
    public class FileUploadValid : IFileUploadValid
    {
        private const int MaxFileSizeInBytes = 5 * 1024 * 1024; // 5 MB

        private const int MaxFileCSVSizeInBytes = 20 * 1024 * 1024; // 20 MB


        public bool IsFileTypeValid(UploadRequest file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
            var fileExtension = Path.GetExtension(file.FileName);
            return allowedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
        }

        public bool IsFileSizeValid(UploadRequest file)
        {
            var fileSizeInBytes = file.Data.Length;
            return fileSizeInBytes <= MaxFileSizeInBytes;
        }


        public bool IsFileCSVTypeValid(UploadRequest file)
        {
            var allowedExtensions = ".csv" ;
            var fileExtension = Path.GetExtension(file.FileName);
            return allowedExtensions.Equals(fileExtension.ToLower());
        }

        public bool IsFileCSVSizeValid(UploadRequest file)
        {
            var fileSizeInBytes = file.Data.Length;
            return fileSizeInBytes <= MaxFileCSVSizeInBytes;
        }

        public bool IsFileTypeImageValid(UploadRequest file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName);
            return allowedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
        }
    }
}
