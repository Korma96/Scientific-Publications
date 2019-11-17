using System.Xml.Serialization;

namespace ScientificPublications.Common.Models
{
    [XmlRoot("session")]
    public class SessionDto
    {
        [XmlElement("username")]
        public string Username { get; set; }

        [XmlElement("role")]
        public string Role { get; set; }
    }
}
