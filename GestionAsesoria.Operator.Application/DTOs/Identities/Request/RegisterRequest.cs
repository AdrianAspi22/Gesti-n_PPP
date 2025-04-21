namespace GestionAsesoria.Operator.Application.DTOs.Identities.Request
{
    public class RegisterRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public int? ActorId { get; set; }
        public string? PhoneNumber { get; set; }
        public string UserId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool EmailConfirmed { get; set; } = true;
    }
}