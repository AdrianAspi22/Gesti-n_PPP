namespace GestionAsesoria.Operator.Application.DTOs.Actor.Response
{
    public class ActorByIdResponseDto
    {
        public int ActorId { get; set; }
        public string Code { get; set; }
        public string CalendarSettings { get; set; }
        public int ActorTypeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
