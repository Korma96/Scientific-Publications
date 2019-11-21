using System.Xml.Serialization;

namespace ScientificPublications.Common.Models
{
    [XmlRoot("user")]
    public class UserDto
    {
        [XmlElement("username")]
        public string Username { get; set; }

        [XmlElement("role")]
        public string Role { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("city")]
        public string City { get; set; }

        [XmlElement("country")]
        public string Country { get; set; }
    }
}
