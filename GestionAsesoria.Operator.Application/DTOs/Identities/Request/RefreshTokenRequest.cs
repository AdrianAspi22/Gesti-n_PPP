namespace GestionAsesoria.Operator.Application.DTOs.Identities.Request
{
    public class RefreshTokenRequest
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}