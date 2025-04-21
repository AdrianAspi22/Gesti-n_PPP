using System;

namespace GestionAsesoria.Operator.Application.DTOs.Identities.Response
{
    public class TokenResponse
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public int? ActorId { get; set; }
    }
}