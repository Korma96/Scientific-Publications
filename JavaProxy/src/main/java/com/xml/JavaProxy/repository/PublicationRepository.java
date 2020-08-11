package com.xml.JavaProxy.repository;

import com.xml.JavaProxy.helper.XUpdateTemplate;
import com.xml.JavaProxy.model.Publication;
import com.xml.JavaProxy.util.XmlUtility;
import org.springframework.stereotype.Repository;

@Repository
public class PublicationRepository extends BaseRepository {

    private String collectionId = "/db/test";
    private String documentId = "publications.xml";
    private String findPublicationByAuthorXQuery = "xqueries/find_publications_by_author.xqy";
    private String findAllPublicationsXQuery = "xqueries/find_all_publications.xqy";
    private String withdrawPublicationXQuery = "xqueries/find_and_withdraw_publication.xqy";
    private String findAllInProcedureByAuthorXQuery = "xqueries/find_all_in_procedure_by_author.xqy";
    private String acceptPublication = "xqueries/accept_publication.xqy";
    private String textSearchPublished = "xqueries/text_search_published.xqy";
    private String textSearchMyPublications = "xqueries/text_search_my_publications.xqy";

    public String findByAuthor(String author) throws Exception {

        String fileContent = readXQueryFile(findPublicationByAuthorXQuery);
        String xQuery = String.format(fileContent, author);
        return executeXQuery(collectionId, xQuery);
    }

    public String findAll() throws Exception {

        String fileContent = readXQueryFile(findAllPublicationsXQuery);
        String xQuery = String.format(fileContent);
        return executeXQuery(collectionId, xQuery);
    }


    public String findAllInProcedureByAuthor(String author) throws Exception {

        String fileContent = readXQueryFile(findAllInProcedureByAuthorXQuery);
        String xQuery = String.format(fileContent, author);
        return executeXQuery(collectionId, xQuery);
    }

    public void insert(Publication publication) throws Exception {
        String xPathElement = "/publications";
        String publicationStr = XmlUtility.jaxbObjectToXML(publication);
        String xUpdateExpression = String.format(XUpdateTemplate.APPEND, xPathElement, publicationStr);
        executeXUpdate(collectionId, documentId, xPathElement, xUpdateExpression);
    }

    public String withdraw(String publicationId) throws Exception {
        String fileContent = readXQueryFile(withdrawPublicationXQuery);
        String xQuery = String.format(fileContent, publicationId);
        return executeXQuery(collectionId, xQuery);
    }


    //editor
    public String acceptPublication(String publicationId, boolean accepted) throws Exception {
        String fileContent = readXQueryFile(acceptPublication);
        String xQuery = String.format(fileContent, publicationId, accepted);
        return executeXQuery(collectionId, xQuery);
    }


    public String textSearchPublished(String searchQuery) throws Exception {
        String fileContent = readXQueryFile(textSearchPublished);
        String xQuery = String.format(fileContent, searchQuery);
        return executeXQuery(collectionId, xQuery);
    }

    public String textSearchMyPublications(String searchQuery, String username) throws Exception {
        String fileContent = readXQueryFile(textSearchMyPublications);
        String xQuery = String.format(fileContent, username, searchQuery);
        return executeXQuery(collectionId, xQuery);
    }

}
