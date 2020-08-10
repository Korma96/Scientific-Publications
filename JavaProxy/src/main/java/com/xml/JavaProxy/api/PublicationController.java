package com.xml.JavaProxy.api;


import com.xml.JavaProxy.model.CoverLetter;
import com.xml.JavaProxy.model.Publication;
import com.xml.JavaProxy.repository.PublicationRepository;
import com.xml.JavaProxy.util.ResponseUtility;
import com.xml.JavaProxy.util.XmlUtility;
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
    @Autowired
    private UserController userController;

    @RequestMapping(value = "/my-publications/{author}", method = RequestMethod.GET, produces = MediaType.APPLICATION_XML_VALUE)
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
        Publication publication = XmlUtility.convertXMLToObject(Publication.class, publicationStr);
        publicationRepository.insert(publication);
        return ResponseUtility.Ok();
    }

    @RequestMapping(value = "/withdraw/{id}", method = RequestMethod.PUT, consumes = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> withdrawPublication(@PathVariable("id") String publicationId) throws Exception{
        publicationRepository.withdraw(publicationId);
        return ResponseUtility.Ok();
    }

    //find all publications in procedure for author
    @RequestMapping(value = "/in-procedure/{author}", method = RequestMethod.GET, produces = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> findAllInProcedure(@PathVariable("author") String author) throws Exception {
        String publications = publicationRepository.findAllInProcedureByAuthor(author);
        return ResponseUtility.Ok(publications);
    }
    //editor
    @RequestMapping(value = "/accept/{id}/{accepted}", method = RequestMethod.PUT, consumes = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> acceptPublication(@PathVariable("id") String publicationId, @PathVariable("accepted") boolean accepted) throws Exception{
        publicationRepository.acceptPublication(publicationId, accepted);
        return ResponseUtility.Ok();
    }

    @RequestMapping(value = "/text-search/{searchQuery}", method = RequestMethod.GET, consumes = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> textSearchPublished(@PathVariable("searchQuery") String searchQuery) throws Exception{
        String publications = publicationRepository.textSearchPublished(searchQuery);
        return ResponseUtility.Ok(publications);
    }
    @RequestMapping(value = "/my-publications-text-search/{username}/{searchQuery}", method = RequestMethod.GET, consumes = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> textSearchMyPublications(@PathVariable("searchQuery") String searchQuery, @PathVariable("username") String loggedUser) throws Exception{
        String publications = publicationRepository.textSearchMyPublications(searchQuery, loggedUser );
        return ResponseUtility.Ok(publications);
    }

}
