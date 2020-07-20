package com.xml.JavaProxy.repository;

import com.xml.JavaProxy.helper.XUpdateTemplate;
import org.springframework.stereotype.Repository;

@Repository
public class CoverLetterRepository extends BaseRepository {

    private String collectionId = "/db/test";
    private String documentId = "coverletters.xml";

    public void insert(String coverLetterStr) throws Exception {
        String xPathElement = "/coverLetters";
        String xUpdateExpression = String.format(XUpdateTemplate.APPEND, xPathElement, coverLetterStr);
        executeXUpdate(collectionId, documentId, xPathElement, xUpdateExpression);
    }

}
