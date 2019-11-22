package com.xml.JavaProxy.api;

import com.xml.JavaProxy.repository.UserRepository;
import com.xml.JavaProxy.util.ResponseUtility;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping(value = "/base")
public class BaseController {

    private UserRepository userRepository;

    @Autowired
    public BaseController(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    @RequestMapping(value = "/{username}", method = RequestMethod.GET, produces = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> getOrderedReviews(@PathVariable("username") String username) throws Exception{
        String user = userRepository.findByUsername(username);
        return ResponseUtility.Ok(user);
    }

}
