using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ScientificPublications.User
{
    public class LoginDto
    {
        [Required]
        [XmlElement("username")]
        public string Username { get; set; }

        [Required]
        [XmlElement("password")]
        public string Password { get; set; }
    }
}
