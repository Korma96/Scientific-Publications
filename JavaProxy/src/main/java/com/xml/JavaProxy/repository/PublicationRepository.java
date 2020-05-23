package com.xml.JavaProxy.repository;

import com.xml.JavaProxy.helper.XUpdateTemplate;
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

    public void insert(String publicationStr) throws Exception {
        String xPathElement = "/publications";
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



}
