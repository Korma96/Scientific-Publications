
package com.xml.JavaProxy.model;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for anonymous complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType>
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;choice>
 *         &lt;element ref="{http://ftn.uns.ac.rs/xml2019/publication}imageContent"/>
 *         &lt;element name="text" type="{http://www.w3.org/2001/XMLSchema}string"/>
 *       &lt;/choice>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "imageContent",
    "text",
    "link"
})
@XmlRootElement(name = "content", namespace = "http://ftn.uns.ac.rs/xml2019/publication")
public class Content {

    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication")
    protected ImageContent imageContent;
    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication")
    protected String text;
    @XmlElement(namespace = "http://ftn.uns.ac.rs/xml2019/publication")
    protected Link link;

    /**
     * Gets the value of the imageContent property.
     * 
     * @return
     *     possible object is
     *     {@link ImageContent }
     *     
     */
    public ImageContent getImageContent() {
        return imageContent;
    }

    /**
     * Sets the value of the imageContent property.
     * 
     * @param value
     *     allowed object is
     *     {@link ImageContent }
     *     
     */
    public void setImageContent(ImageContent value) {
        this.imageContent = value;
    }

    /**
     * Gets the value of the text property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getText() {
        return text;
    }

    /**
     * Sets the value of the text property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setText(String value) {
        this.text = value;
    }

    public Link getLink() {
        return link;
    }

    public void setLink(Link link) {
        this.link = link;
    }
}
