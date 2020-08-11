﻿using ScientificPublications.Common;
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
        public string[] reviewers { get; set; }

        public string editor { get; set; }
    }
}
