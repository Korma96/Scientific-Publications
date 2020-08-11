using ScientificPublications.Common;
using System;
using System.Xml.Serialization;

namespace ScientificPublications.WorkFlow
{
    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = Constants.Namespaces.WorkFlow)]
    [XmlRoot(Namespace = Constants.Namespaces.WorkFlow, IsNullable = false)]
    public class WorkFlowDto
    {
        public string publicationId { get; set; }

        [XmlArrayItem("reviewer", IsNullable = false)]
        public string[] reviewers { get; set; }
    }
}
