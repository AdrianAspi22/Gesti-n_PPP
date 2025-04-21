namespace GestionAsesoria.Operator.Application.DTOs.Actor.Response.ActorResearchGroup
{
    public class ActorResearchGroupDetailsDto
    {
        public int ResearchGroupId { get; set; }
        public string ResearchGroupAcronym { get; set; }
        public int ResearchLineId { get; set; }
        public string ResearchLineName { get; set; }
        public int ResearchAreaId { get; set; }
        public string ResearchAreaName { get; set; }
        public int TeacherId { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherSecondName { get; set; }
        public int? ActorType { get; set; }
    }
}