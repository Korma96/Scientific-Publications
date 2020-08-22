using ScientificPublications.Common;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ScientificPublications.DataAccess.Model
{
    [XmlRoot("workflows")]
    public class WorkFlows
    {
        [XmlElement("workflow", Namespace = Constants.Namespaces.WorkFlow)]
        public List<workflow> WorkFlowList { get; set; }
    }
}
