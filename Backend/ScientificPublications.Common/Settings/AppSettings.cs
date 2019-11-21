namespace ScientificPublications.Common.Settings
{
    public class AppSettings
    {
        public SmtpSettings SmtpSettings { get; set; }

        public Paths Paths { get; set; }

        public string Secret { get; set; }

        public double JwtTokenExpiresInSeconds { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }
    }
}
