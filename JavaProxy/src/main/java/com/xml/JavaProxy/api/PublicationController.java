package com.xml.JavaProxy.api;


import com.xml.JavaProxy.repository.PublicationRepository;
import com.xml.JavaProxy.util.ResponseUtility;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping(value = "/publication")
public class PublicationController {

    private PublicationRepository publicationRepository;

    @Autowired
    public PublicationController(PublicationRepository publicationRepository) {
        this.publicationRepository = publicationRepository;
    }

    @RequestMapping(value = "/{author}", method = RequestMethod.GET, produces = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> findByAuthor(@PathVariable("author") String author) throws Exception{
        String publications = publicationRepository.findByAuthor(author);
        return ResponseUtility.Ok(publications);
    }

    @RequestMapping(value = "/all", method = RequestMethod.GET, produces = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> findAll() throws Exception{
        String publications = publicationRepository.findAll();
        return ResponseUtility.Ok(publications);
    }
    @RequestMapping(value = "/insert", method = RequestMethod.POST, consumes = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> insertPublication(@RequestBody String publicationStr) throws Exception{
        publicationRepository.insert(publicationStr);
        return ResponseUtility.Ok();
    }
    @RequestMapping(value = "/withdraw/{id}", method = RequestMethod.PUT, consumes = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> withdrawPublication(@PathVariable("id") String publicationId) throws Exception{
        publicationRepository.withdraw(publicationId);
        return ResponseUtility.Ok();
    }



}
