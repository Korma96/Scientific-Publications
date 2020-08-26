package com.xml.JavaProxy.model;

import javax.xml.bind.annotation.*;

@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
        "value"
})
@XmlRootElement(name = "link", namespace = "http://ftn.uns.ac.rs/xml2019/publication")
public class Link {

    @XmlValue
    protected String value;
    @XmlAttribute(name = "refId", namespace = "http://ftn.uns.ac.rs/xml2019/publication")
    @XmlSchemaType(name = "anySimpleType")
    protected String refId;

    public String getValue() {
        return value;
    }

    public void setValue(String value) {
        this.value = value;
    }

    public String getRefId() {
        return refId;
    }

    public void setRefId(String refId) {
        this.refId = refId;
    }
}