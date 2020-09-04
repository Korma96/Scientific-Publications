package com.xml.JavaProxy.repository;

import com.xml.JavaProxy.util.AuthenticationUtilities;
import com.xml.JavaProxy.util.FileUtil;
import com.xml.JavaProxy.util.MetadataExtractor;
import com.xml.JavaProxy.util.SparqlUtil;
import org.springframework.stereotype.Repository;

import java.io.*;
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

@Repository
public class RdfRepository {

    private String sparqlFilePath = "resources/sparql/search_publications.rq";
    private String rdfFilePath = "data/rdf/publication_metadata.rdf";


    public void uploadMetadata() {

        /*// RDF triples which are to be loaded into the model

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
        /*
        UpdateProcessor processor = UpdateExecutionFactory.createRemote(request, conn.updateEndpoint);
        processor.execute();

        // Creating the first named graph and updating it with RDF data
        String sparqlUpdate = SparqlUtil.insertData(conn.dataEndpoint + "/publications/metadata", new String(out.toByteArray()));
        System.out.println(sparqlUpdate);

        // UpdateRequest represents a unit of execution
        UpdateRequest update = UpdateFactory.create(sparqlUpdate);

        processor = UpdateExecutionFactory.createRemote(update, conn.updateEndpoint);
        processor.execute();

        update = UpdateFactory.create(sparqlUpdate);

        processor = UpdateExecutionFactory.createRemote(update, conn.updateEndpoint);
        processor.execute();*/
    }

    public List<String> searchPublications(String searchQuery, String authorName) throws IOException {

        //authorName - Marko Radovic npr sluzi zbog uslova da ulogovani korisnik
        // moze da pregleda svoje radove u statusu koji nije accepted;

        List<String> resultPublicationIds = new ArrayList<String>();
/*
        AuthenticationUtilities.ConnectionProperties conn = AuthenticationUtilities.loadProperties();

        String sparqlQuery = String.format(FileUtil.readFile(sparqlFilePath, StandardCharsets.UTF_8),
                conn.dataEndpoint + "/publications/metadata", searchQuery, authorName);

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

*/
        return resultPublicationIds;


    }

}
