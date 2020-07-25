namespace ScientificPublications.Common
{
    public static class Constants
    {
        public const string XmlContentType = "application/xml";

        public const string AccessToken = "access-token";

        public const string SessionInfo = "session-info";

        public const string Authorization = "Authorization";

        public const string SingleSpace = " ";

        public static class Namespaces
        {
            public const string CoverLetter = "http://ftn.uns.ac.rs/xml2019/coverletter";

            public const string Publication = "http://ftn.uns.ac.rs/xml2019/publication";
        }

        public static class ExceptionMessages
        {
            public const string EmptyValue = "Emtpy value";

            public const string EmptyFile = "Emtpy file";

            public const string InvalidUsernameOrPassword = "Invalid username and/or password";

            public const string DatabaseException = "Database exception";

            public const string UsernameAlreadyExists = "Username already exists";
        }

        public static class JavaController
        {
            public const string User = "user";

            public const string Publication = "publication";

            public const string CoverLetter = "coverletter";
        }
    }
}
