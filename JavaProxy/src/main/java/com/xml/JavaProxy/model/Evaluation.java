
package com.xml.JavaProxy.model;

import org.checkerframework.checker.units.qual.g;

import java.util.ArrayList;
import java.util.List;
import javax.xml.bind.annotation.*;


/**
 * <p>Java class for anonymous complex type.
 *
 * <p>The following schema fragment specifies the expected content contained within this class.
 *
 *    <xs:complexType>
 *             <xs:sequence>
 *                 <xs:element ref="comments"/>
 *                 <xs:element ref="grades"/>
 *             </xs:sequence>
 *             <xs:attribute name="author" type="xs:string"/>
 *             <xs:attribute name="publicationId" type="xs:string"/>
 *         </xs:complexType>
 *     </xs:element>
 *     <xs:element name="comments">
 *         <xs:complexType>
 *             <xs:sequence>
 *                 <xs:element name="originality" type="xs:string"/>
 *                 <xs:element name="abstract" type="xs:string"/>
 *                 <xs:element name="methodology" type="xs:string"/>
 *                 <xs:element name="results" type="xs:string"/>
 *                 <xs:element name="bibliography" type="xs:string"/>
 *                 <xs:element name="generalComment" type="xs:string"/>
 *             </xs:sequence>
 *         </xs:complexType>
 *     </xs:element>
 *     <xs:element name="grades">
 *         <xs:complexType>
 *             <xs:sequence>
 *                 <xs:element name="originality" type="grade"/>
 *                 <xs:element name="abstract" type="grade"/>
 *                 <xs:element name="methodology" type="grade"/>
 *                 <xs:element name="results" type="grade"/>
 *                 <xs:element name="bibliography" type="grade"/>
 *                 <xs:element name="generalComment" type="grade"/>
 *             </xs:sequence>
 *         </xs:complexType>
 *     </xs:element>
 *     <xs:simpleType name="grade">
 *         <xs:restriction base="xs:int">
 *             <xs:minInclusive value="1"/>
 *             <xs:maxInclusive value="5"/>
 *         </xs:restriction>
 *     </xs:simpleType>
 * </pre>
 *
 *
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
        "comments",
        "grades"
})
@XmlRootElement(name = "evaluation", namespace = "http://ftn.uns.ac.rs/xml2019/evaluation")
public class Evaluation {

    @XmlAttribute(name = "author", namespace = "http://ftn.uns.ac.rs/xml2019/evaluation")
    protected String author;
    @XmlAttribute(name = "publicationId", namespace = "http://ftn.uns.ac.rs/xml2019/evaluation")
    protected String publicationId;
    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/evaluation", required = true)
    protected Evaluation.Comments comments;
    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/evaluation", required = true)
    protected Evaluation.Grades grades;
    /**
     * Gets the value of the author property.
     *
     * @return
     *     possible object is
     *     {@link String }
     *
     */
    public String getAuthor() {
        return author;
    }

    /**
     * Sets the value of the author property.
     *
     * @param value
     *     allowed object is
     *     {@link String }
     *
     */
    public void setAuthor(String value) {
        this.author = value;
    }
    /**
     * Gets the value of the publicationId property.
     *
     * @return
     *     possible object is
     *     {@link String }
     *
     */
    public String getPublicationId() {
        return publicationId;
    }

    /**
     * Sets the value of the publicationId property.
     *
     * @param value
     *     allowed object is
     *     {@link String }
     *
     */
    public void setPublicationId(String value) {
        this.publicationId = value;
    }

    /**
     * Gets the value of the comments property.
     *
     * @return
     *     possible object is
     *     {@link Evaluation.Comments }
     *
     */
    public Evaluation.Comments getComments() {
        return comments;
    }

    /**
     * Sets the value of the comments property.
     *
     * @param value
     *     allowed object is
     *     {@link Evaluation.Comments }
     *
     */
    public void setComments(Evaluation.Comments value) {
        this.comments = value;
    }

    /**
     * Gets the value of the comments property.
     *
     * @return
     *     possible object is
     *     {@link Evaluation.Grades }
     *
     */
    public Evaluation.Grades getGrades() {
        return grades;
    }

    /**
     * Sets the value of the comments property.
     *
     * @param value
     *     allowed object is
     *     {@link Evaluation.Grades }
     *
     */
    public void setGrades(Evaluation.Grades value) {
        this.grades = value;
    }

    /**
     * <p>Java class for anonymous complex type.
     *
     * <p>The following schema fragment specifies the expected content contained within this class.
     *
     * <pre>
     * &lt;complexType>
     *   &lt;complexContent>
     *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
     *       &lt;sequence>
     *         &lt;element name="reviewer" type="{http://www.w3.org/2001/XMLSchema}anyType" maxOccurs="unbounded"/>
     *       &lt;/sequence>
     *     &lt;/restriction>
     *   &lt;/complexContent>
     * &lt;/complexType>
     * </pre>
     *
     *
     */
    @XmlAccessorType(XmlAccessType.FIELD)
    @XmlType(name = "", propOrder = {
            "originality",
            "_abstract",
            "methodology",
            "results",
            "bibliography",
            "generalComment",

    })
    public static class Comments {

        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/evaluation", required = true)
        protected String originality;
        @XmlElement(name = "abstract", namespace = "http://ftn.uns.ac.rs/xml2019/evaluation", required = true)
        protected String _abstract;
        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/evaluation", required = true)
        protected String methodology;
        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/evaluation", required = true)
        protected String results;
        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/evaluation", required = true)
        protected String bibliography;
        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/evaluation", required = true)
        protected String generalComment;

        public String getOriginality() {
            return originality;
        }

        public void setOriginality(String originality) {
            this.originality = originality;
        }

        public String get_abstract() {
            return _abstract;
        }

        public void set_abstract(String _abstract) {
            this._abstract = _abstract;
        }

        public String getMethodology() {
            return methodology;
        }

        public void setMethodology(String methodology) {
            this.methodology = methodology;
        }

        public String getResults() {
            return results;
        }

        public void setResults(String results) {
            this.results = results;
        }

        public String getBibliography() {
            return bibliography;
        }

        public void setBibliography(String bibliography) {
            this.bibliography = bibliography;
        }

        public String getGeneralComment() {
            return generalComment;
        }

        public void setGeneralComment(String generalComment) {
            this.generalComment = generalComment;
        }
    }

    @XmlAccessorType(XmlAccessType.FIELD)
    @XmlType(name = "", propOrder = {
            "originality",
            "_abstract",
            "methodology",
            "results",
            "bibliography",
            "generalGrade",

    })
    public static class Grades {

        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/evaluation", required = true)
        protected String originality;
        @XmlElement(name = "abstract", namespace = "http://ftn.uns.ac.rs/xml2019/evaluation", required = true)
        protected String _abstract;
        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/evaluation", required = true)
        protected String methodology;
        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/evaluation", required = true)
        protected String results;
        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/evaluation", required = true)
        protected String bibliography;
        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/evaluation", required = true)
        protected String generalGrade;

        public String getOriginality() {
            return originality;
        }

        public void setOriginality(String originality) {
            this.originality = originality;
        }

        public String get_abstract() {
            return _abstract;
        }

        public void set_abstract(String _abstract) {
            this._abstract = _abstract;
        }

        public String getMethodology() {
            return methodology;
        }

        public void setMethodology(String methodology) {
            this.methodology = methodology;
        }

        public String getResults() {
            return results;
        }

        public void setResults(String results) {
            this.results = results;
        }

        public String getBibliography() {
            return bibliography;
        }

        public void setBibliography(String bibliography) {
            this.bibliography = bibliography;
        }

        public String getGeneralGrade() {
            return generalGrade;
        }

        public void setGeneralGrade(String generalGrade) {
            this.generalGrade = generalGrade;
        }
    }
}
