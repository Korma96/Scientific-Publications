package com.xml.JavaProxy.api;

import com.xml.JavaProxy.model.User;
import com.xml.JavaProxy.repository.UserRepository;
import com.xml.JavaProxy.util.ResponseUtility;
import com.xml.JavaProxy.util.XmlUtility;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpSession;

@RestController
@RequestMapping(value = "/user")
public class UserController {

    private UserRepository userRepository;

    @Autowired
    public UserController(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    @RequestMapping(value = "/{username}", method = RequestMethod.GET, produces = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> findByUsername(@PathVariable("username") String username) throws Exception {
        String user = userRepository.findByUsername(username);
        return ResponseUtility.Ok(user);
    }

    @RequestMapping(method = RequestMethod.POST, consumes = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> insertUser(@RequestBody String userStr) throws Exception {
        User user = XmlUtility.convertXMLToObject(User.class, userStr);
        userRepository.insert(user);
        return ResponseUtility.Ok();
    }
}
