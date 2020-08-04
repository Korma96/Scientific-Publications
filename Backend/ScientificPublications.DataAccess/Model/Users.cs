using System.Collections.Generic;
using System.Xml.Serialization;

namespace ScientificPublications.DataAccess.Model
{
    [XmlRoot("users")]
    public class Users
    {
        [XmlElement("user")]
        public List<User> UsersList { get; set; }
    }
}
