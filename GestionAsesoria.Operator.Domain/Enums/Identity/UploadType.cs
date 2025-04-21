using System.ComponentModel;

namespace GestionAsesoria.Operator.Domain.Enums.Identity
{
    public enum UploadType : byte
    {
        [Description(@"Images\Thesis")]
        Thesis,

        [Description(@"Images\ProfilePictures")]
        ProfilePicture,

        [Description(@"Documents")]
        Document
    }
}