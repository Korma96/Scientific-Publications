package com.xml.JavaProxy.api;


import com.xml.JavaProxy.api.dto.StatusDto;
import com.xml.JavaProxy.model.FilePathDto;
import com.xml.JavaProxy.model.Publication;
import com.xml.JavaProxy.repository.PublicationRepository;
import com.xml.JavaProxy.repository.RdfRepository;
import com.xml.JavaProxy.util.PdfUtil;
import com.xml.JavaProxy.util.ResponseUtility;
import com.xml.JavaProxy.util.XmlUtility;
import org.apache.commons.io.IOUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.io.ByteArrayResource;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;


@RestController
@RequestMapping(value = "/publication")
public class PublicationController {

    @Autowired
    private PublicationRepository publicationRepository;
    @Autowired
    private RdfRepository rdfRepository;

    private PdfUtil pdfUtil;
    
    private final String publicationXsdPath = "src\\main\\resources\\xsd\\publication.xsd";
    private final String publicationXslPath = "src\\main\\resources\\xsl\\publication.xsl";

    @Autowired
    public PublicationController(PublicationRepository publicationRepository) {
        this.publicationRepository = publicationRepository;
    }

    @RequestMapping(value = "{author}", method = RequestMethod.GET, produces = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> findByAuthor(@PathVariable("author") String author) throws Exception{
        String publications = publicationRepository.findByAuthor(author);
        return ResponseUtility.Ok(publications);
    }

    @RequestMapping(value = "/status/{status}", method = RequestMethod.GET, produces = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> findByStatus(@PathVariable("status") String status) throws Exception{
        String publications = publicationRepository.findByStatus(status);
        return ResponseUtility.Ok(publications);
    }

    @RequestMapping(value = "/id/{id}", method = RequestMethod.GET, produces = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> findById(@PathVariable("id") String id) throws Exception{
        String publication = publicationRepository.findById(id);
        return ResponseUtility.Ok(publication);
    }

    @RequestMapping(value = "{publicationId}", method = RequestMethod.PUT, produces = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> updateStatus(@PathVariable("publicationId") String publicationId, @RequestBody String statusStr) throws Exception{
        StatusDto status = XmlUtility.convertXMLToObject(StatusDto.class, statusStr);
        publicationRepository.updateStatus(publicationId, status.getStatus());
        return ResponseUtility.Ok();
    }
    // TODO: do we need this endpoint?
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
    //find all publications by status for author
    @RequestMapping(value = "/status/{status}/{author}", method = RequestMethod.GET, produces = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> findAllInProcedure(@PathVariable("status") String status, @PathVariable("author") String author) throws Exception {
        String publications = publicationRepository.findByStatusAndAuthor(status, author);
        return ResponseUtility.Ok(publications);
    }
    // TODO: "status/{status}/{publicationId}" can be used instead
    //editor
    @RequestMapping(value = "/accept/{id}/{accepted}", method = RequestMethod.PUT)
    public ResponseEntity<String> acceptPublication(@PathVariable("id") String publicationId, @PathVariable("accepted") boolean accepted) throws Exception{
        publicationRepository.acceptPublication(publicationId, accepted);
        return ResponseUtility.Ok();
    }

    @RequestMapping(value = "/text-search/{searchQuery}", method = RequestMethod.GET)
    public ResponseEntity<String> textSearchPublished(@PathVariable("searchQuery") String searchQuery) throws Exception{
        String publications = publicationRepository.textSearchPublished(searchQuery);
        return ResponseUtility.Ok(publications);
    }

    @RequestMapping(value = "/my-publications-text-search/{username}/{searchQuery}", method = RequestMethod.GET)
    public ResponseEntity<String> textSearchMyPublications(@PathVariable("username") String username, @PathVariable("searchQuery") String searchQuery) throws Exception{
        String publications = publicationRepository.textSearchMyPublications(searchQuery, username);
        return ResponseUtility.Ok(publications);
    }

    @RequestMapping(value = "/reviewer-assigned-publications/{reviewer}", method = RequestMethod.GET)
    public ResponseEntity<String> findAllByReviewer(@PathVariable("reviewer") String reviewerUsername) throws Exception{
        String publications = publicationRepository.findAllByReviewer(reviewerUsername);
        return ResponseUtility.Ok(publications);
    }

    @GetMapping(value = "/getByIdPDF/{id}", produces = MediaType.APPLICATION_PDF_VALUE)
    public ResponseEntity<ByteArrayResource> getPDF(@PathVariable String id) throws Exception {
        String publication = publicationRepository.findById(id);
        return new ResponseEntity<>(
                new ByteArrayResource(IOUtils.toByteArray(PdfUtil
                        .toPdf(publication, publicationXslPath, publicationXsdPath).getInputStream())),
                HttpStatus.OK);
    }

    @GetMapping(value = "/getByIdHtml/{id}", produces = MediaType.TEXT_HTML_VALUE)
    public ResponseEntity<String> getHtml(@PathVariable String id) throws Exception {
        String publication = publicationRepository.findById(id);
        return new ResponseEntity<String>(PdfUtil
                        .transform(publication, publicationXslPath, publicationXsdPath),
                HttpStatus.OK);
    }

    @RequestMapping(value = "add-comment/{publicationId}/{commentedSegmentId}", method = RequestMethod.PUT, consumes = MediaType.APPLICATION_XML_VALUE, produces = MediaType.APPLICATION_XML_VALUE )
    public ResponseEntity<String> addComment(@PathVariable("publicationId") String publicationId, @PathVariable("commentedSegmentId") String commentedSegmentId, @RequestBody String comment) throws Exception {
        String publication = publicationRepository.addComment(publicationId, commentedSegmentId, comment);
        return ResponseUtility.Ok(publication);
    }

    @RequestMapping(value = "/referencing/{publicationId}", method = RequestMethod.GET, produces = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> findReferencingPublications(@PathVariable("publicationId") String publicationId) throws Exception {
        String publications = publicationRepository.findReferencingPublications(publicationId);
        return ResponseUtility.Ok(publications);
    }

    @RequestMapping(value = "/advanced-search/{searchQuery}/{loggedName}", method = RequestMethod.GET)
    public ResponseEntity<String> advancedSearch(@PathVariable("searchQuery") String searchQuery, @PathVariable("loggedName") String loggedName) throws Exception {
        List<String> publicationIds = rdfRepository.searchPublications(searchQuery, loggedName);
        String publications = "<publications>\n";
        for (String id : publicationIds) {
            String publication = publicationRepository.findById(id);
            if (publication != null) {
                publications += publication;
            }
        }
        publications += "\n</publications>";
        return ResponseUtility.Ok(publications);
    }

    @PostMapping("/upload/rdf")
    public ResponseEntity<String> uploadRdfFileToDataset(@RequestBody FilePathDto filePathDto) throws Exception {
        rdfRepository.uploadMetadata(filePathDto.getInputPath());
        return ResponseUtility.Ok();
    }

    @PostMapping("/xml-to-rdf/grddl")
    public ResponseEntity<String> generateRdfFromXmlUsingGrddl(@RequestBody FilePathDto filePathDto) throws Exception {
        rdfRepository.extractRdf(filePathDto.getInputPath(), filePathDto.getOutputPath());
        return ResponseUtility.Ok();
    }
}
