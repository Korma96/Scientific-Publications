
package com.xml.JavaProxy.model;

import java.util.ArrayList;
import java.util.List;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlAttribute;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlSchemaType;
import javax.xml.bind.annotation.XmlType;
import javax.xml.datatype.XMLGregorianCalendar;


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
 *         &lt;element name="header">
 *           &lt;complexType>
 *             &lt;complexContent>
 *               &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *                 &lt;sequence>
 *                   &lt;element name="publicationDate" type="{http://www.w3.org/2001/XMLSchema}date"/>
 *                   &lt;element name="revisionNumber" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *                   &lt;element name="status" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *                 &lt;/sequence>
 *               &lt;/restriction>
 *             &lt;/complexContent>
 *           &lt;/complexType>
 *         &lt;/element>
 *         &lt;element ref="{http://ftn.uns.ac.rs/xml2019/publication}title"/>
 *         &lt;element name="authors">
 *           &lt;complexType>
 *             &lt;complexContent>
 *               &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *                 &lt;sequence maxOccurs="unbounded">
 *                   &lt;element name="author" type="{http://ftn.uns.ac.rs/xml2019/publication}author"/>
 *                 &lt;/sequence>
 *               &lt;/restriction>
 *             &lt;/complexContent>
 *           &lt;/complexType>
 *         &lt;/element>
 *         &lt;element name="abstract">
 *           &lt;complexType>
 *             &lt;complexContent>
 *               &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *                 &lt;sequence>
 *                   &lt;element name="purpose" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *                   &lt;element name="problem" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *                   &lt;element name="method" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *                   &lt;element name="results" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *                   &lt;element name="conclusions" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *                 &lt;/sequence>
 *               &lt;/restriction>
 *             &lt;/complexContent>
 *           &lt;/complexType>
 *         &lt;/element>
 *         &lt;element name="keywords" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *         &lt;element ref="{http://ftn.uns.ac.rs/xml2019/publication}section" maxOccurs="unbounded"/>
 *         &lt;element name="bibliography">
 *           &lt;complexType>
 *             &lt;complexContent>
 *               &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *                 &lt;sequence>
 *                   &lt;element ref="{http://ftn.uns.ac.rs/xml2019/publication}reference" maxOccurs="unbounded"/>
 *                 &lt;/sequence>
 *               &lt;/restriction>
 *             &lt;/complexContent>
 *           &lt;/complexType>
 *         &lt;/element>
 *         &lt;element ref="{http://ftn.uns.ac.rs/xml2019/publication}comment" minOccurs="0"/>
 *       &lt;/sequence>
 *       &lt;attribute ref="{http://ftn.uns.ac.rs/xml2019/publication}id"/>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "header",
    "title",
    "authors",
    "_abstract",
    "keywords",
    "section",
    "bibliography",
    "comment"
})
@XmlRootElement(name = "publication", namespace = "http://ftn.uns.ac.rs/xml2019/publication")
public class Publication {

    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication", required = true)
    protected Publication.Header header;
    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication", required = true)
    protected String title;
    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication", required = true)
    protected Publication.Authors authors;
    @XmlElement(name = "abstract", namespace = "http://ftn.uns.ac.rs/xml2019/publication", required = true)
    protected Publication.Abstract _abstract;
    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication", required = true)
    protected String keywords;
    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication", required = true)
    protected List<Section> section;
    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication", required = true)
    protected Publication.Bibliography bibliography;
    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication")
    protected String comment;
    @XmlAttribute(name = "id", namespace = "http://ftn.uns.ac.rs/xml2019/publication")
    protected String id;

    /**
     * Gets the value of the header property.
     * 
     * @return
     *     possible object is
     *     {@link Publication.Header }
     *     
     */
    public Publication.Header getHeader() {
        return header;
    }

    /**
     * Sets the value of the header property.
     * 
     * @param value
     *     allowed object is
     *     {@link Publication.Header }
     *     
     */
    public void setHeader(Publication.Header value) {
        this.header = value;
    }

    /**
     * Gets the value of the title property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getTitle() {
        return title;
    }

    /**
     * Sets the value of the title property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setTitle(String value) {
        this.title = value;
    }

    /**
     * Gets the value of the authors property.
     * 
     * @return
     *     possible object is
     *     {@link Publication.Authors }
     *     
     */
    public Publication.Authors getAuthors() {
        return authors;
    }

    /**
     * Sets the value of the authors property.
     * 
     * @param value
     *     allowed object is
     *     {@link Publication.Authors }
     *     
     */
    public void setAuthors(Publication.Authors value) {
        this.authors = value;
    }

    /**
     * Gets the value of the abstract property.
     * 
     * @return
     *     possible object is
     *     {@link Publication.Abstract }
     *     
     */
    public Publication.Abstract getAbstract() {
        return _abstract;
    }

    /**
     * Sets the value of the abstract property.
     * 
     * @param value
     *     allowed object is
     *     {@link Publication.Abstract }
     *     
     */
    public void setAbstract(Publication.Abstract value) {
        this._abstract = value;
    }

    /**
     * Gets the value of the keywords property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKeywords() {
        return keywords;
    }

    /**
     * Sets the value of the keywords property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKeywords(String value) {
        this.keywords = value;
    }

    /**
     * Gets the value of the section property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the section property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getSection().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link Section }
     * 
     * 
     */
    public List<Section> getSection() {
        if (section == null) {
            section = new ArrayList<Section>();
        }
        return this.section;
    }

    /**
     * Gets the value of the bibliography property.
     * 
     * @return
     *     possible object is
     *     {@link Publication.Bibliography }
     *     
     */
    public Publication.Bibliography getBibliography() {
        return bibliography;
    }

    /**
     * Sets the value of the bibliography property.
     * 
     * @param value
     *     allowed object is
     *     {@link Publication.Bibliography }
     *     
     */
    public void setBibliography(Publication.Bibliography value) {
        this.bibliography = value;
    }

    /**
     * Gets the value of the comment property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getComment() {
        return comment;
    }

    /**
     * Sets the value of the comment property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setComment(String value) {
        this.comment = value;
    }

    /**
     * Gets the value of the id property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getId() {
        return id;
    }

    /**
     * Sets the value of the id property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setId(String value) {
        this.id = value;
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
     *         &lt;element name="purpose" type="{http://www.w3.org/2001/XMLSchema}string"/>
     *         &lt;element name="problem" type="{http://www.w3.org/2001/XMLSchema}string"/>
     *         &lt;element name="method" type="{http://www.w3.org/2001/XMLSchema}string"/>
     *         &lt;element name="results" type="{http://www.w3.org/2001/XMLSchema}string"/>
     *         &lt;element name="conclusions" type="{http://www.w3.org/2001/XMLSchema}string"/>
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
        "purpose",
        "problem",
        "method",
        "results",
        "conclusions"
    })
    public static class Abstract {

        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication", required = true)
        protected String purpose;
        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication", required = true)
        protected String problem;
        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication", required = true)
        protected String method;
        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication", required = true)
        protected String results;
        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication", required = true)
        protected String conclusions;

        /**
         * Gets the value of the purpose property.
         * 
         * @return
         *     possible object is
         *     {@link String }
         *     
         */
        public String getPurpose() {
            return purpose;
        }

        /**
         * Sets the value of the purpose property.
         * 
         * @param value
         *     allowed object is
         *     {@link String }
         *     
         */
        public void setPurpose(String value) {
            this.purpose = value;
        }

        /**
         * Gets the value of the problem property.
         * 
         * @return
         *     possible object is
         *     {@link String }
         *     
         */
        public String getProblem() {
            return problem;
        }

        /**
         * Sets the value of the problem property.
         * 
         * @param value
         *     allowed object is
         *     {@link String }
         *     
         */
        public void setProblem(String value) {
            this.problem = value;
        }

        /**
         * Gets the value of the method property.
         * 
         * @return
         *     possible object is
         *     {@link String }
         *     
         */
        public String getMethod() {
            return method;
        }

        /**
         * Sets the value of the method property.
         * 
         * @param value
         *     allowed object is
         *     {@link String }
         *     
         */
        public void setMethod(String value) {
            this.method = value;
        }

        /**
         * Gets the value of the results property.
         * 
         * @return
         *     possible object is
         *     {@link String }
         *     
         */
        public String getResults() {
            return results;
        }

        /**
         * Sets the value of the results property.
         * 
         * @param value
         *     allowed object is
         *     {@link String }
         *     
         */
        public void setResults(String value) {
            this.results = value;
        }

        /**
         * Gets the value of the conclusions property.
         * 
         * @return
         *     possible object is
         *     {@link String }
         *     
         */
        public String getConclusions() {
            return conclusions;
        }

        /**
         * Sets the value of the conclusions property.
         * 
         * @param value
         *     allowed object is
         *     {@link String }
         *     
         */
        public void setConclusions(String value) {
            this.conclusions = value;
        }

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
     *       &lt;sequence maxOccurs="unbounded">
     *         &lt;element name="author" type="{http://ftn.uns.ac.rs/xml2019/publication}author"/>
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
        "author"
    })
    public static class Authors {

        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication", required = true)
        protected List<Author> author;

        /**
         * Gets the value of the author property.
         * 
         * <p>
         * This accessor method returns a reference to the live list,
         * not a snapshot. Therefore any modification you make to the
         * returned list will be present inside the JAXB object.
         * This is why there is not a <CODE>set</CODE> method for the author property.
         * 
         * <p>
         * For example, to add a new item, do as follows:
         * <pre>
         *    getAuthor().add(newItem);
         * </pre>
         * 
         * 
         * <p>
         * Objects of the following type(s) are allowed in the list
         * {@link Author }
         * 
         * 
         */
        public List<Author> getAuthor() {
            if (author == null) {
                author = new ArrayList<Author>();
            }
            return this.author;
        }

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
     *         &lt;element ref="{http://ftn.uns.ac.rs/xml2019/publication}reference" maxOccurs="unbounded"/>
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
        "reference"
    })
    public static class Bibliography {

        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication", required = true)
        protected List<Reference> reference;

        /**
         * Gets the value of the reference property.
         * 
         * <p>
         * This accessor method returns a reference to the live list,
         * not a snapshot. Therefore any modification you make to the
         * returned list will be present inside the JAXB object.
         * This is why there is not a <CODE>set</CODE> method for the reference property.
         * 
         * <p>
         * For example, to add a new item, do as follows:
         * <pre>
         *    getReference().add(newItem);
         * </pre>
         * 
         * 
         * <p>
         * Objects of the following type(s) are allowed in the list
         * {@link Reference }
         * 
         * 
         */
        public List<Reference> getReference() {
            if (reference == null) {
                reference = new ArrayList<Reference>();
            }
            return this.reference;
        }

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
     *         &lt;element name="publicationDate" type="{http://www.w3.org/2001/XMLSchema}date"/>
     *         &lt;element name="revisionNumber" type="{http://www.w3.org/2001/XMLSchema}string"/>
     *         &lt;element name="status" type="{http://www.w3.org/2001/XMLSchema}string"/>
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
        "publicationDate",
        "revisionNumber",
        "status"
    })
    public static class Header {

        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication", required = true)
        @XmlSchemaType(name = "date")
        protected XMLGregorianCalendar publicationDate;
        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication", required = true)
        protected String revisionNumber;
        @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication", required = true)
        protected String status;

        /**
         * Gets the value of the publicationDate property.
         * 
         * @return
         *     possible object is
         *     {@link XMLGregorianCalendar }
         *     
         */
        public XMLGregorianCalendar getPublicationDate() {
            return publicationDate;
        }

        /**
         * Sets the value of the publicationDate property.
         * 
         * @param value
         *     allowed object is
         *     {@link XMLGregorianCalendar }
         *     
         */
        public void setPublicationDate(XMLGregorianCalendar value) {
            this.publicationDate = value;
        }

        /**
         * Gets the value of the revisionNumber property.
         * 
         * @return
         *     possible object is
         *     {@link String }
         *     
         */
        public String getRevisionNumber() {
            return revisionNumber;
        }

        /**
         * Sets the value of the revisionNumber property.
         * 
         * @param value
         *     allowed object is
         *     {@link String }
         *     
         */
        public void setRevisionNumber(String value) {
            this.revisionNumber = value;
        }

        /**
         * Gets the value of the status property.
         * 
         * @return
         *     possible object is
         *     {@link String }
         *     
         */
        public String getStatus() {
            return status;
        }

        /**
         * Sets the value of the status property.
         * 
         * @param value
         *     allowed object is
         *     {@link String }
         *     
         */
        public void setStatus(String value) {
            this.status = value;
        }

    }

}
