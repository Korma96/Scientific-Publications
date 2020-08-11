using System.Xml.Serialization;

namespace ScientificPublications.DataAccess.Model
{
    [XmlRoot("statusDto")]
    public class StatusDto
    {
        [XmlElement("status")]
        public string Status { get; set; }
    }
}
