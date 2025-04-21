namespace GestionAsesoria.Operator.Application.DTOs.Actor.Request
{
    public class UpdateActorRequestDto
    {
        public int ActorId { get; set; }
        public string Code { get; set; }
        public string CalendarSettings { get; set; }
        public int ActorType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
