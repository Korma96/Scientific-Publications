package com.xml.JavaProxy.repository;

import com.xml.JavaProxy.util.ExistAuthUtility;
import org.exist.xmldb.EXistResource;
import org.springframework.core.io.ClassPathResource;
import org.springframework.stereotype.Repository;
import org.xmldb.api.DatabaseManager;
import org.xmldb.api.base.*;
import org.xmldb.api.modules.XMLResource;
import org.xmldb.api.modules.XQueryService;
import org.xmldb.api.modules.XUpdateQueryService;
import javax.xml.transform.OutputKeys;
import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Paths;

@Repository
public class BaseRepository {

    protected void executeXUpdate(String collectionId, String documentId, String xPathElement, String xUpdateStr) throws Exception{
        Collection collection = getCollection(collectionId);
        XUpdateQueryService xupdateService = (XUpdateQueryService) collection.getService("XUpdateQueryService", "1.0");
        xupdateService.setProperty("indent", "yes");

        System.out.println("[INFO] Updating " + xPathElement + " node.");
        long mods = xupdateService.updateResource(documentId, xUpdateStr);
        System.out.println("[INFO] " + mods + " modifications processed.");
    }

    protected String executeXQuery(String collectionId, String xQueryStr) throws Exception {
        Collection collection = getCollection(collectionId);
        XQueryService xQueryService = (XQueryService) collection.getService("XQueryService", "1.0");
        xQueryService.setProperty("indent", "yes");
        try {
            CompiledExpression compiledExpression = xQueryService.compile(xQueryStr);
            ResourceSet resourceSet = xQueryService.execute(compiledExpression);
            ResourceIterator i = resourceSet.getIterator();

            org.xmldb.api.base.Resource resource = null;
            //closeCollection(collection);
            if (i.hasMoreResources()) {
                try {
                    resource = i.nextResource();
                    XMLResource xmlResource = (XMLResource) resource;
                    return xmlResource.getContent().toString();
                } finally {
                    freeXmlResources(resource);

                }
            }
        }
        catch (Exception e)
        {
            System.out.println("cant compile");

        }
        return null;
    }

    protected String readXQueryFile(String xQueryPath) throws Exception {
        String classPath = new ClassPathResource(xQueryPath).getFile().getPath();
        return loadFileContent(classPath);
    }

    private XMLResource getResource(String collectionId, String documentId) throws Exception {
        ExistAuthUtility.ConnectionProperties conn = establishDbConnection();
        Collection col = null;
        XMLResource res = null;

        try {
            // get the collection
            System.out.println("[INFO] Retrieving the collection: " + collectionId);
            col = retrieveCollection(collectionId, conn);

            System.out.println("[INFO] Retrieving the document: " + documentId);
            res = (XMLResource) col.getResource(documentId);

            if (res == null) {
                System.out.println("[WARNING] Document '" + documentId + "' can not be found!");
            }

            return res;
        }
        catch(Exception e) {
            System.out.println("[ERROR] Retrieving the collection: " + collectionId);
            System.out.println("[ERROR] Retrieving the document: " + documentId);
            throw e;
        } finally {
            freeXmlResources(res);
            closeCollection(col);
        }
    }

    private Collection getCollection(String collectionId) throws Exception {
        ExistAuthUtility.ConnectionProperties conn = establishDbConnection();
        Collection collection = null;

        try {
            // get the collection
            System.out.println("[INFO] Retrieving the collection: " + collectionId);
            collection = retrieveCollection(collectionId, conn);
            return collection;
        }
        catch(Exception e) {
            System.out.println("[ERROR] Retrieving the collection: " + collectionId);
            throw e;
        } finally {
            //closeCollection(collection);
        }
    }

    private String loadFileContent(String path) throws IOException {
        byte[] allBytes = Files.readAllBytes(Paths.get(path));
        return new String(allBytes, StandardCharsets.UTF_8);
    }

    private Collection retrieveCollection(String collectionId, ExistAuthUtility.ConnectionProperties conn) throws Exception{
        Collection collection = DatabaseManager.getCollection(conn.uri + collectionId, conn.user, conn.password);
        collection.setProperty(OutputKeys.INDENT, "yes");
        return collection;
    }

    private ExistAuthUtility.ConnectionProperties establishDbConnection() throws Exception {
        ExistAuthUtility.ConnectionProperties conn = ExistAuthUtility.loadProperties();

        Class<?> cl = Class.forName(conn.driver);
        Database database = (Database) cl.newInstance();
        database.setProperty("create-database", "true");

        DatabaseManager.registerDatabase(database);
        return conn;
    }

    private void freeXmlResources(Object res) {
        if(res != null) {
            try {
                ((EXistResource)res).freeResources();
            } catch (XMLDBException xe) {
                xe.printStackTrace();
            }
        }
    }

    private void closeCollection(Collection collection) {
        if(collection != null) {
            try {
                collection.close();
            } catch (XMLDBException xe) {
                xe.printStackTrace();
            }
        }
    }
}
