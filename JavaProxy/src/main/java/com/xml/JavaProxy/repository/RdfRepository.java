package com.xml.JavaProxy.repository;

import com.xml.JavaProxy.util.*;
import org.apache.jena.query.QueryExecution;
import org.apache.jena.query.QueryExecutionFactory;
import org.apache.jena.query.QuerySolution;
import org.apache.jena.query.ResultSet;
import org.apache.jena.rdf.model.Model;
import org.apache.jena.rdf.model.ModelFactory;
import org.apache.jena.rdf.model.RDFNode;
import org.apache.jena.update.UpdateExecutionFactory;
import org.apache.jena.update.UpdateFactory;
import org.apache.jena.update.UpdateProcessor;
import org.apache.jena.update.UpdateRequest;
import org.springframework.stereotype.Repository;
import org.xml.sax.SAXException;

import javax.xml.transform.TransformerException;
import java.io.*;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

@Repository
public class RdfRepository {

    private final String sparqlFilePath = "src/main/resources/sparql/search_publications.rq";
    private final String graphName = "publications/metadata";
    private AuthenticationUtilities.ConnectionProperties conn;

    public void uploadMetadata(String rdfFilePath) throws IOException {
        conn = AuthenticationUtilities.loadProperties();
        // RDF triples which are to be loaded into the model

        // Creates a default model
        Model model = ModelFactory.createDefaultModel();
        model.read(rdfFilePath);

        ByteArrayOutputStream out = new ByteArrayOutputStream();

        model.write(out, SparqlUtil.NTRIPLES);

        System.out.println("[INFO] Rendering model as RDF/XML...");
        model.write(System.out, SparqlUtil.RDF_XML);

        // Delete all of the triples in all of the named graphs
        UpdateRequest request = UpdateFactory.create() ;
        request.add(SparqlUtil.dropAll());

        /*
         * Create UpdateProcessor, an instance of execution of an UpdateRequest.
         * UpdateProcessor sends update request to a remote SPARQL update service.
         */

        UpdateProcessor processor = UpdateExecutionFactory.createRemote(request, conn.updateEndpoint);
        processor.execute();

        // Creating the first named graph and updating it with RDF data
        String sparqlUpdate = SparqlUtil.insertData(conn.dataEndpoint + "/" + graphName, new String(out.toByteArray()));
        System.out.println(sparqlUpdate);

        // UpdateRequest represents a unit of execution
        UpdateRequest update = UpdateFactory.create(sparqlUpdate);

        processor = UpdateExecutionFactory.createRemote(update, conn.updateEndpoint);
        processor.execute();

        update = UpdateFactory.create(sparqlUpdate);

        processor = UpdateExecutionFactory.createRemote(update, conn.updateEndpoint);
        processor.execute();
    }

    public void extractRdf(String inputXmlFilePath, String outputRdfFilePath) throws IOException, SAXException, TransformerException {
        MetadataExtractor metadataExtractor = new MetadataExtractor();
        System.out.println("[INFO] Extracting metadata from RDFa attributes...");
        metadataExtractor.extractMetadata(
                new FileInputStream(new File(inputXmlFilePath)),
                new FileOutputStream(new File(outputRdfFilePath)));
    }

    public List<String> searchPublications(String searchQuery, String authorName) throws IOException {
        conn = AuthenticationUtilities.loadProperties();
        //authorName - Marko Radovic npr sluzi zbog uslova da ulogovani korisnik
        // moze da pregleda svoje radove u statusu koji nije accepted;

        List<String> resultPublicationIds = new ArrayList<String>();

        AuthenticationUtilities.ConnectionProperties conn = AuthenticationUtilities.loadProperties();

        String sparqlQuery = String.format(FileUtil.readFile(sparqlFilePath, StandardCharsets.UTF_8),
                conn.dataEndpoint + "/" + graphName, searchQuery, authorName);

        QueryExecution query = QueryExecutionFactory.sparqlService(conn.queryEndpoint, sparqlQuery);

        ResultSet results = query.execSelect();

        String varName;
        RDFNode varValue;

        while (results.hasNext()) {

            // A single answer from a SELECT query
            QuerySolution querySolution = results.next();
            Iterator<String> variableBindings = querySolution.varNames();

            // Retrieve variable bindings
            while (variableBindings.hasNext()) {

                varName = variableBindings.next();
                varValue = querySolution.get(varName);
                String[] tokens = varValue.toString().split("/");
                resultPublicationIds.add(tokens[tokens.length-1]);

            }
        }


        query = QueryExecutionFactory.sparqlService(conn.queryEndpoint, sparqlQuery);

        results = query.execSelect();

        query.close();


        return resultPublicationIds;


    }

}
