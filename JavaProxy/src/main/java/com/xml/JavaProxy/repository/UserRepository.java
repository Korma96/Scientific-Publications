package com.xml.JavaProxy.repository;

import org.springframework.stereotype.Repository;

@Repository
public class UserRepository extends BaseRepository {

    private String collectionId = "/db/test";
    private String documentId = "users.xml";
    private String findUserByUsernameXQuery = "xqueries/find_user_by_username.xqy";

    public String findByUsername(String username) throws Exception {
        String fileContent = readXQueryFile(findUserByUsernameXQuery);
        String xQuery = String.format(fileContent, username);
        return ExecuteXQuery(collectionId, xQuery);
    }
}
