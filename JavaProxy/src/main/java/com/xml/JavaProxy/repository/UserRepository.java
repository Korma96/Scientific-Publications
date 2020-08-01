package com.xml.JavaProxy.repository;

import com.xml.JavaProxy.helper.XUpdateTemplate;
import com.xml.JavaProxy.model.User;
import com.xml.JavaProxy.util.XmlUtility;
import org.springframework.stereotype.Repository;

import java.nio.charset.StandardCharsets;

@Repository
public class UserRepository extends BaseRepository {

    private String collectionId = "/db/test";
    private String documentId = "users.xml";
    private String findUserByUsernameXQuery = "xqueries/find_user_by_username.xqy";

    public String findByUsername(String username) throws Exception {

        String fileContent = readXQueryFile(findUserByUsernameXQuery);
        String xQuery = String.format(fileContent, username);
        return executeXQuery(collectionId, xQuery);
    }

    public void insert(User user) throws Exception {
        String xPathElement = "/users";
        String xmlFragment = XmlUtility.jaxbObjectToXML(user);
        String xUpdateExpression = String.format(XUpdateTemplate.APPEND, xPathElement, xmlFragment);
        executeXUpdate(collectionId, documentId, xPathElement, xUpdateExpression);
    }
}
