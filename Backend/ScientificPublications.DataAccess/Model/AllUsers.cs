using System.Collections.Generic;
using System.Xml.Serialization;

namespace ScientificPublications.DataAccess.Model
{
    [XmlRoot("users")]
    public class AllUsers
    {
        [XmlElement("user")]
        public List<User> Users { get; set; }
    }
}
