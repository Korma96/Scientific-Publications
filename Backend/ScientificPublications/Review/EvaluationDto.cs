using ScientificPublications.Common;
using ScientificPublications.DataAccess.Model;

namespace ScientificPublications.Review
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = Constants.Namespaces.Evaluation)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = Constants.Namespaces.Evaluation, IsNullable = false)]
    public partial class EvaluationDto
    {
        private evaluationComments commentsField;

        private evaluationGrades gradesField;

        public evaluationComments comments
        {
            get
            {
                return this.commentsField;
            }
            set
            {
                this.commentsField = value;
            }
        }

        /// <remarks/>
        public evaluationGrades grades
        {
            get
            {
                return this.gradesField;
            }
            set
            {
                this.gradesField = value;
            }
        }
    }
}
