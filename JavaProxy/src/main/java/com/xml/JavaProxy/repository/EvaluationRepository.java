package com.xml.JavaProxy.repository;

import com.xml.JavaProxy.helper.XUpdateTemplate;
import com.xml.JavaProxy.model.Evaluation;
import com.xml.JavaProxy.util.XmlUtility;
import org.springframework.stereotype.Repository;

@Repository
public class EvaluationRepository extends BaseRepository {

    private String collectionId = "/db/test";
    private String documentId = "evaluations.xml";
    private String findByPublicationId = "xqueries/evaluation/find_by_publicationId.xqy";


    public void insert(Evaluation evaluation) throws Exception {
        String xPathElement = "/evaluations";
        String evaluationStr = XmlUtility.jaxbObjectToXML(evaluation);
        String xUpdateExpression = String.format(XUpdateTemplate.APPEND, xPathElement, evaluationStr);
        executeXUpdate(collectionId, documentId, xPathElement, xUpdateExpression);
    }

    public String findByPublicationId(String publicationId) throws Exception {
        String fileContent = readXQueryFile(findByPublicationId);
        String xQuery = String.format(fileContent, publicationId);
        return executeXQuery(collectionId, xQuery);
    }


}
