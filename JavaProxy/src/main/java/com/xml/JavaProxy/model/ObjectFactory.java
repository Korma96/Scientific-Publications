
package com.xml.JavaProxy.model;

import javax.xml.bind.annotation.XmlRegistry;


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


    /**
     * Create a new ObjectFactory that can be used to create new instances of schema derived classes for package: com.xml.JavaProxy.model
     * 
     */
    public ObjectFactory() {
    }

    /**
     * Create an instance of {@link Workflow }
     * 
     */
    public Workflow createWorkflow() {
        return new Workflow();
    }

    /**
     * Create an instance of {@link Workflow.Reviewers }
     * 
     */
    public Workflow.Reviewers createWorkflowReviewers() {
        return new Workflow.Reviewers();
    }

}
