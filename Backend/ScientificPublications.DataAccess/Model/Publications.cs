using ScientificPublications.Common;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ScientificPublications.DataAccess.Model
{
    [XmlRoot("publications")]
    public class Publications
    {
        [XmlElement("publication", Namespace = Constants.Namespaces.Publication)]
        public List<publication> PublicationList { get; set; }
    }
}
