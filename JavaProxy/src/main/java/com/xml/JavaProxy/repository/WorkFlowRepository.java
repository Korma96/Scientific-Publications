package com.xml.JavaProxy.repository;

import com.xml.JavaProxy.helper.XUpdateTemplate;
import org.springframework.stereotype.Repository;

@Repository
public class WorkFlowRepository extends BaseRepository {

    private String collectionId = "/db/test";
    private String documentId = "workflows.xml";


    public void insert(String workFlowStr) throws Exception {
        String xPathElement = "/workflows";
        String xUpdateExpression = String.format(XUpdateTemplate.APPEND, xPathElement, workFlowStr);
        executeXUpdate(collectionId, documentId, xPathElement, xUpdateExpression);
    }
}
