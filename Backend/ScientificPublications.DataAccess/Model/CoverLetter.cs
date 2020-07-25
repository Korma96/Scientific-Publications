using ScientificPublications.Common;
using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace ScientificPublications.DataAccess.Model
{
    [XmlRoot("coverLetter", Namespace = Constants.Namespaces.CoverLetter)]
    public class CoverLetter
    {
        [XmlAttribute("publicationId", Form = XmlSchemaForm.Qualified, Namespace = Constants.Namespaces.CoverLetter)]
        public string PublicationId { get; set; }

        [XmlElement("date", Namespace = Constants.Namespaces.CoverLetter, DataType = "date")]
        public DateTime Date { get; set; }

        [XmlElement("content", Namespace = Constants.Namespaces.CoverLetter)]
        public string Content { get; set; }
    }
}
