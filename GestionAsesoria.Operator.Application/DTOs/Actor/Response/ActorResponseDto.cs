namespace GestionAsesoria.Operator.Application.DTOs.Actor.Response
{
    public class ActorResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public int? MainRoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsActived { get; set; }
    }
}
