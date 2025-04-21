namespace GestionAsesoria.Operator.Application.Configurations
{
    public class AppConfiguration
    {
        public string? Secret { get; set; }
        public string? Issuer { get; set; }
        public string? Expires { get; set; }

        public bool BehindSSLProxy { get; set; }

        public string? ProxyIP { get; set; }

        public string? ApplicationUrl { get; set; }
    }
}