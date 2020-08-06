package com.xml.JavaProxy.repository;

import com.xml.JavaProxy.helper.XUpdateTemplate;
import com.xml.JavaProxy.model.Workflow;
import com.xml.JavaProxy.util.XmlUtility;
import org.springframework.stereotype.Repository;

@Repository
public class WorkFlowRepository extends BaseRepository {

    private String collectionId = "/db/test";
    private String documentId = "workflows.xml";
    private String findByIdXQuery = "xqueries/workflow/find_by_id.xqy";

    public void insert(Workflow workFlow) throws Exception {
        String xPathElement = "/workflows";
        String workFlowStr = XmlUtility.jaxbObjectToXML(workFlow);
        String xUpdateExpression = String.format(XUpdateTemplate.APPEND, xPathElement, workFlowStr);
        executeXUpdate(collectionId, documentId, xPathElement, xUpdateExpression);
    }

    public String findByPublicationId(String id) throws Exception {
        String fileContent = readXQueryFile(findByIdXQuery);
        String xQuery = String.format(fileContent, id);
        return executeXQuery(collectionId, xQuery);
    }
}
