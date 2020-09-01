using ScientificPublications.Common;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ScientificPublications.DataAccess.Model
{
    [XmlRoot("evaluations")]
    public class Evaluations
    {
        [XmlElement("evaluation", Namespace = Constants.Namespaces.Evaluation)]
        public List<evaluation> EvaluationList { get; set; }
    }
}
