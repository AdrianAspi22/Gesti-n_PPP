namespace GestionAsesoria.Operator.Infrastructure.Persistence.Configurations
{
    public class GoogleCalendarOptions
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string[] Scopes { get; set; }
    }
}
