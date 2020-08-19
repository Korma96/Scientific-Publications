using ScientificPublications.Common;
using System.Xml.Serialization;

namespace ScientificPublications.Service.Email
{
    [XmlRoot("mail", Namespace = Constants.Namespaces.Notification)]
    public class EmailEntity
    {
        [XmlElement("to", Namespace = Constants.Namespaces.Notification)]
        public string To { get; set; }

        [XmlElement("subject", Namespace = Constants.Namespaces.Notification)]
        public string Subject { get; set; }

        [XmlElement("body", Namespace = Constants.Namespaces.Notification)]
        public string Body { get; set; }
    }
}
