using System.Xml.Serialization;

namespace ScientificPublications.Publication
{
    public class DenyPublicationDto
    {
        [XmlElement("authorEmail")]
        public string AuthorEmail { get; set; }

        [XmlElement("reason")]
        public string Reason { get; set; }
    }
}
