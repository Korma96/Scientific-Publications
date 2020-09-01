using ScientificPublications.Common;
using System.Xml.Schema;

namespace ScientificPublications.DataAccess.Model
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = Constants.Namespaces.Evaluation)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = Constants.Namespaces.Evaluation, IsNullable = false)]
    public partial class evaluation
    {

        private evaluationComments commentsField;

        private evaluationGrades gradesField;

        private string authorField;

        private string publicationIdField;

        /// <remarks/>
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

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = XmlSchemaForm.Unqualified)]
        public string author
        {
            get
            {
                return this.authorField;
            }
            set
            {
                this.authorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(Form = XmlSchemaForm.Unqualified)]
        public string publicationId
        {
            get
            {
                return this.publicationIdField;
            }
            set
            {
                this.publicationIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = Constants.Namespaces.Evaluation)]
    public partial class evaluationComments
    {

        private string originalityField;

        private string abstractField;

        private string methodologyField;

        private string resultsField;

        private string bibliographyField;

        private string generalCommentField;

        /// <remarks/>
        public string originality
        {
            get
            {
                return this.originalityField;
            }
            set
            {
                this.originalityField = value;
            }
        }

        /// <remarks/>
        public string @abstract
        {
            get
            {
                return this.abstractField;
            }
            set
            {
                this.abstractField = value;
            }
        }

        /// <remarks/>
        public string methodology
        {
            get
            {
                return this.methodologyField;
            }
            set
            {
                this.methodologyField = value;
            }
        }

        /// <remarks/>
        public string results
        {
            get
            {
                return this.resultsField;
            }
            set
            {
                this.resultsField = value;
            }
        }

        /// <remarks/>
        public string bibliography
        {
            get
            {
                return this.bibliographyField;
            }
            set
            {
                this.bibliographyField = value;
            }
        }

        /// <remarks/>
        public string generalComment
        {
            get
            {
                return this.generalCommentField;
            }
            set
            {
                this.generalCommentField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = Constants.Namespaces.Evaluation)]
    public partial class evaluationGrades
    {

        private byte originalityField;

        private byte abstractField;

        private byte methodologyField;

        private byte resultsField;

        private byte bibliographyField;

        private byte generalGradeField;

        /// <remarks/>
        public byte originality
        {
            get
            {
                return this.originalityField;
            }
            set
            {
                this.originalityField = value;
            }
        }

        /// <remarks/>
        public byte @abstract
        {
            get
            {
                return this.abstractField;
            }
            set
            {
                this.abstractField = value;
            }
        }

        /// <remarks/>
        public byte methodology
        {
            get
            {
                return this.methodologyField;
            }
            set
            {
                this.methodologyField = value;
            }
        }

        /// <remarks/>
        public byte results
        {
            get
            {
                return this.resultsField;
            }
            set
            {
                this.resultsField = value;
            }
        }

        /// <remarks/>
        public byte bibliography
        {
            get
            {
                return this.bibliographyField;
            }
            set
            {
                this.bibliographyField = value;
            }
        }

        /// <remarks/>
        public byte generalGrade
        {
            get
            {
                return this.generalGradeField;
            }
            set
            {
                this.generalGradeField = value;
            }
        }
    }


}
