using System.Xml.Serialization;

namespace ScientificPublications.Service.Email
{
    [XmlRoot("mail")]
    public class EmailEntity
    {
        [XmlElement("subject")]
        public string Subject { get; set; }

        [XmlElement("body")]
        public string Body { get; set; }
    }
}
