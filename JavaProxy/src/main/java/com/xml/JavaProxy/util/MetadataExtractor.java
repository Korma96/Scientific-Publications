package com.xml.JavaProxy.util;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;

import javax.xml.transform.OutputKeys;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.stream.StreamResult;
import javax.xml.transform.stream.StreamSource;

import org.xml.sax.SAXException;

import com.sun.org.apache.xalan.internal.xsltc.trax.TransformerFactoryImpl;

public class MetadataExtractor {
	
	private TransformerFactory transformerFactory;

	private static final String XSLT_FILE = "src\\main\\resources\\xsl\\grddl.xsl";
	
	public MetadataExtractor() throws SAXException, IOException {
		
		// Setup the XSLT transformer factory
		transformerFactory = new TransformerFactoryImpl();
	}

	/**
	 * Generates RDF/XML based on RDFa metadata from an XML containing 
	 * input stream by applying GRDDL XSL transformation.
	 *  
	 * @param in XML containing input stream
	 * @param out RDF/XML output stream
	 */
	public void extractMetadata(InputStream in, OutputStream out) throws FileNotFoundException, TransformerException {
		
		// Create transformation source
		StreamSource transformSource = new StreamSource(new File(XSLT_FILE));
		
		// Initialize GRDDL transformer object
		Transformer grddlTransformer = transformerFactory.newTransformer(transformSource);
		
		// Set the indentation properties
		grddlTransformer.setOutputProperty("{http://xml.apache.org/xalan}indent-amount", "2");
		grddlTransformer.setOutputProperty(OutputKeys.INDENT, "yes");
		
		// Initialize transformation subject
		StreamSource source = new StreamSource(in);

		// Initialize result stream
		StreamResult result = new StreamResult(out);
		
		// Trigger the transformation
		grddlTransformer.transform(source, result);
		
	}


	/*public void test() throws Exception {

		System.out.println("[INFO] " + MetadataExtractor.class.getSimpleName());
		
		String filePath = "src\\main\\resources\\xsl\\publications_metadata.rdf";

		InputStream in = new FileInputStream(new File("C:\\Users\\Vuk\\Desktop\\Faks\\7_semestar\\Xml i web servisi\\Projekat\\resources\\db\\publications.xml"));

		OutputStream out = new FileOutputStream(filePath);
		
		extractMetadata(in, out);
		
		System.out.println("[INFO] File \"" + filePath + "\" generated successfully.");
		
		System.out.println("[INFO] End.");

	}

	public static void main(String[] args) throws Exception {
		new MetadataExtractor().test();
	}*/

}
