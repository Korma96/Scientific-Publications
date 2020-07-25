package com.xml.JavaProxy.repository;

import com.xml.JavaProxy.helper.XUpdateTemplate;
import com.xml.JavaProxy.model.CoverLetter;
import com.xml.JavaProxy.util.XmlUtility;
import org.springframework.stereotype.Repository;

@Repository
public class CoverLetterRepository extends BaseRepository {

    private String collectionId = "/db/test";
    private String documentId = "coverletters.xml";
    private String findCoverLetterByPublicationIdXQuery = "xqueries/find_coverletter_by_publicationid.xqy";

    public void insert(CoverLetter coverLetter) throws Exception {
        String xPathElement = "/coverLetters";
        String coverLetterStr = XmlUtility.jaxbObjectToXML(coverLetter);
        String xUpdateExpression = String.format(XUpdateTemplate.APPEND, xPathElement, coverLetterStr);
        executeXUpdate(collectionId, documentId, xPathElement, xUpdateExpression);
    }

    public String findByPublicationId(String publicationId) throws Exception {
        String fileContent = readXQueryFile(findCoverLetterByPublicationIdXQuery);
        String xQuery = String.format(fileContent, publicationId);
        return executeXQuery(collectionId, xQuery);
    }
}
