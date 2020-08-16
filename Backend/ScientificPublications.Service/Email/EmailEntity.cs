using System.Xml.Serialization;

namespace ScientificPublications.Service.Email
{
    [XmlRoot("mail")]
    public class EmailEntity
    {
        [XmlElement("to")]
        public string To { get; set; }

        [XmlElement("subject")]
        public string Subject { get; set; }

        [XmlElement("body")]
        public string Body { get; set; }
    }
}
