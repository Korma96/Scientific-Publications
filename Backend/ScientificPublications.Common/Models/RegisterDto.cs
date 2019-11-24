using System.Xml.Serialization;

namespace ScientificPublications.Common.Models
{
    [XmlRoot(nameof(RegisterDto))]
    public class RegisterDto : UserDto
    {
        [XmlElement("password")]
        public string Password { get; set; }
    }
}
