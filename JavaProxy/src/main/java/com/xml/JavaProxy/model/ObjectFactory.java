
package com.xml.JavaProxy.model;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlElementDecl;
import javax.xml.bind.annotation.XmlRegistry;
import javax.xml.namespace.QName;


/**
 * This object contains factory methods for each 
 * Java content interface and Java element interface 
 * generated in the com.xml.JavaProxy.model package. 
 * <p>An ObjectFactory allows you to programatically 
 * construct new instances of the Java representation 
 * for XML content. The Java representation of XML 
 * content can consist of schema derived interfaces 
 * and classes representing the binding of schema 
 * type definitions, element declarations and model 
 * groups.  Factory methods for each of these are 
 * provided in this class.
 * 
 */
@XmlRegistry
public class ObjectFactory {

    private final static QName _Comment_QNAME = new QName("http://ftn.uns.ac.rs/xml2019/publication", "comment");
    private final static QName _Title_QNAME = new QName("http://ftn.uns.ac.rs/xml2019/publication", "title");
    private final static QName _Image_QNAME = new QName("http://ftn.uns.ac.rs/xml2019/publication", "image");

    /**
     * Create a new ObjectFactory that can be used to create new instances of schema derived classes for package: com.xml.JavaProxy.model
     * 
     */
    public ObjectFactory() {
    }

    /**
     * Create an instance of {@link Publication }
     * 
     */
    public Publication createPublication() {
        return new Publication();
    }

    /**
     * Create an instance of {@link Reference }
     * 
     */
    public Reference createReference() {
        return new Reference();
    }

    /**
     * Create an instance of {@link ImageContent }
     * 
     */
    public ImageContent createImageContent() {
        return new ImageContent();
    }

    /**
     * Create an instance of {@link Publication.Header }
     * 
     */
    public Publication.Header createPublicationHeader() {
        return new Publication.Header();
    }

    /**
     * Create an instance of {@link Publication.Authors }
     * 
     */
    public Publication.Authors createPublicationAuthors() {
        return new Publication.Authors();
    }

    /**
     * Create an instance of {@link Publication.Abstract }
     * 
     */
    public Publication.Abstract createPublicationAbstract() {
        return new Publication.Abstract();
    }

    /**
     * Create an instance of {@link Section }
     * 
     */
    public Section createSection() {
        return new Section();
    }

    /**
     * Create an instance of {@link Content }
     * 
     */
    public Content createContent() {
        return new Content();
    }

    /**
     * Create an instance of {@link Publication.Bibliography }
     * 
     */
    public Publication.Bibliography createPublicationBibliography() {
        return new Publication.Bibliography();
    }

    /**
     * Create an instance of {@link Author }
     * 
     */
    public Author createAuthor() {
        return new Author();
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://ftn.uns.ac.rs/xml2019/publication", name = "comment")
    public JAXBElement<String> createComment(String value) {
        return new JAXBElement<String>(_Comment_QNAME, String.class, null, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://ftn.uns.ac.rs/xml2019/publication", name = "title")
    public JAXBElement<String> createTitle(String value) {
        return new JAXBElement<String>(_Title_QNAME, String.class, null, value);
    }

    /**
     * Create an instance of {@link JAXBElement }{@code <}{@link String }{@code >}}
     * 
     */
    @XmlElementDecl(namespace = "http://ftn.uns.ac.rs/xml2019/publication", name = "image")
    public JAXBElement<String> createImage(String value) {
        return new JAXBElement<String>(_Image_QNAME, String.class, null, value);
    }

}
