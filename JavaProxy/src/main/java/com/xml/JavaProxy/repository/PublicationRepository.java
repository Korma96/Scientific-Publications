package com.xml.JavaProxy.repository;

import com.xml.JavaProxy.helper.XUpdateTemplate;
import com.xml.JavaProxy.model.Publication;
import com.xml.JavaProxy.model.Section;
import com.xml.JavaProxy.util.XmlUtility;
import org.springframework.stereotype.Repository;


@Repository
public class PublicationRepository extends BaseRepository {

    private String collectionId = "/db/test";
    private String documentId = "publications.xml";
    private String findPublicationByAuthorXQuery = "xqueries/publication/find_publications_by_author.xqy";
    private String findAllPublicationsXQuery = "xqueries/publication/find_all_publications.xqy";
    private String withdrawPublicationXQuery = "xqueries/publication/find_and_withdraw_publication.xqy";
    private String findAllInProcedureByAuthorXQuery = "xqueries/publication/find_all_in_procedure_by_author.xqy";
    private String acceptPublicationXQuery = "xqueries/publication/accept_publication.xqy";
    private String findByStatusXQuery = "xqueries/publication/find_by_status.xqy";
    private String findByIdXQuery = "xqueries/publication/find_by_id.xqy";
    private String updateStatusXQuery = "xqueries/publication/update_status.xqy";
	private String textSearchPublished = "xqueries/text_search_published.xqy";
    private String textSearchMyPublications = "xqueries/text_search_my_publications.xqy";
    private String findPublicationByReviewerXQuery = "xqueries/publication/find_publications_by_reviewer.xqy";
    private String deleteByIdXQuery = "xqueries/publication/delete_by_id.xqy";
    //private String findPublicationIdsByReviewerXQuery = "xqueries/publication/find_publication_ids_by_reviewer.xqy";


    public String findByAuthor(String author) throws Exception {

        String fileContent = readXQueryFile(findPublicationByAuthorXQuery);
        String xQuery = String.format(fileContent, author);
        return executeXQuery(collectionId, xQuery);
    }

    public String findById(String id) throws Exception {
        String fileContent = readXQueryFile(findByIdXQuery);
        String xQuery = String.format(fileContent, id);
        return executeXQuery(collectionId, xQuery);
    }

    public String findByStatus(String status) throws Exception {

        String fileContent = readXQueryFile(findByStatusXQuery);
        String xQuery = String.format(fileContent, status);
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

    public String updateStatus(String publicationId, String status) throws Exception  {
        String fileContent = readXQueryFile(updateStatusXQuery);
        String xQuery = String.format(fileContent, publicationId, status);
        return executeXQuery(collectionId, xQuery);
    }

    public String withdraw(String publicationId) throws Exception {
        String fileContent = readXQueryFile(withdrawPublicationXQuery);
        String xQuery = String.format(fileContent, publicationId);
        return executeXQuery(collectionId, xQuery);
    }

    //editor
    public String acceptPublication(String publicationId, boolean accepted) throws Exception {
        String fileContent = readXQueryFile(acceptPublicationXQuery);
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

    public String findAllByReviewer(String reviewerUsername) throws Exception {
        String fileContent = readXQueryFile(findPublicationByReviewerXQuery);
        String xQuery = String.format(fileContent, reviewerUsername);
        return executeXQuery(collectionId, xQuery);
    }

    public String delete(String publicationId) throws Exception {
        String fileContent = readXQueryFile(deleteByIdXQuery);
        String xQuery = String.format(fileContent, publicationId);
        return executeXQuery(collectionId, xQuery);
    }


    public String addComment(String publicationId, String commentedSegmentId, String comment) throws Exception {
        String publicationStr =  findById(publicationId);
        /*
        String publicationXsdPath = "C:\\Users\\Vuk\\Desktop\\Faks\\7_semestar\\Xml i web servisi\\Projekat\\resources\\publication.xsd";
        File file = new File(publicationXsdPath);
        String constant = XMLConstants.W3C_XML_SCHEMA_NS_URI;
        SchemaFactory xsdFactory = SchemaFactory.newInstance(constant);
        Schema schema = xsdFactory.newSchema(file);
        */
        Publication publication =  XmlUtility.convertXMLToObject(Publication.class, publicationStr);
        if (publicationId.equals(commentedSegmentId)) {
            publication.setComment(comment);
        }
        else {
            for (Section section: publication.getSection()) {
                if (section.getId().equals(commentedSegmentId)) {
                    section.setComment(comment);
                    break;
                }
            }
        }
        delete(publicationId);
        insert(publication);
        return XmlUtility.jaxbObjectToXML(publication);

    }
}
