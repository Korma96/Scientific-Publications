namespace ScientificPublications.Common.Settings
{
    public class PathSettings
    {
        public string BasePath { get; set; }

        public string EditorDeniedMail { get; set; }

        public string EditorAcceptedMail { get; set; }

        public string AuthorUploadedMail { get; set; }

        public string AuthorWithdrawnMail { get; set; }

        public string EditorShouldReviseMail { get; set; }

        public string EditorInReviewMail { get; set; }

        public string AuthorRevisedMail { get; set; }

        public string ReviewerReviewed { get; set; }

        public string ReviewerDenied { get; set; }

        public string ReviewerAccepted { get; set; }

        public string EditorAssigned { get; set; }

        public string ReviewersDenied { get; set; }

        public string PublicationXsd { get; set; }

        public string UserDtoXsd { get; set; }

        public string CoverLetterXsd { get; set; }

        public string WorkFlowXsd { get; set; }
    }
}
