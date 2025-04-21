using GestionAsesoria.Operator.Application.DTOs.Files.Request;

namespace GestionAsesoria.Operator.Application.Validators.Features.Files
{
    public interface IFileUploadValid
    {
        bool IsFileTypeValid(UploadRequest file);
        bool IsFileSizeValid(UploadRequest file);
        bool IsFileCSVTypeValid(UploadRequest file);
        bool IsFileCSVSizeValid(UploadRequest file);
        bool IsFileTypeImageValid(UploadRequest file);

    }
}
