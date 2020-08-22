using ScientificPublications.Common;
using System.Xml.Serialization;

namespace ScientificPublications.DataAccess.Model
{
    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = Constants.Namespaces.WorkFlow)]
    [XmlRoot(Namespace = Constants.Namespaces.WorkFlow, IsNullable = false)]
    public partial class workflow
    {
        public string publicationId { get; set; }

        [XmlArrayItem("reviewer", IsNullable = false)]
        public workflowReviewer[] reviewers { get; set; }

        public string editor { get; set; }
    }

    [System.Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = Constants.Namespaces.WorkFlow)]
    public partial class workflowReviewer
    {
        [XmlAttribute()]
        public string status { get; set; }

        [XmlText()]
        public string username { get; set; }
    }
}
