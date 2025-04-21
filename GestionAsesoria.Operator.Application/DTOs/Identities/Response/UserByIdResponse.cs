namespace GestionAsesoria.Operator.Application.DTOs.Identities.Response
{
    public class UserByIdResponse
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserId { get; set; }
        public bool IsActive { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
