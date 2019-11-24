namespace ScientificPublications.Common.Settings
{
    public class AppSettings
    {
        public SmtpSettings Smtp { get; set; }

        public PathSettings Paths { get; set; }

        public JwtSettings Jwt { get; set; }

        public string DbProxyUrl { get; set; }
    }
}
