using System.Xml.Serialization;

namespace ScientificPublications.DataAccess.Model
{
    [XmlRoot("user")]
    public class User
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlElement("username")]
        public string Username { get; set; }

        [XmlElement("password")]
        public string Password { get; set; }

        [XmlElement("salt")]
        public string Salt { get; set; }

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
