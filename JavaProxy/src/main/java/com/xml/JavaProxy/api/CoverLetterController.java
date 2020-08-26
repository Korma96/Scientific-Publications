package com.xml.JavaProxy.api;

import com.xml.JavaProxy.model.CoverLetter;
import com.xml.JavaProxy.repository.CoverLetterRepository;
import com.xml.JavaProxy.util.ResponseUtility;
import com.xml.JavaProxy.util.XmlUtility;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping(value = "/coverletter")
public class CoverLetterController {

    private CoverLetterRepository coverLetterRepository;

    @Autowired
    public CoverLetterController(CoverLetterRepository coverLetterRepository) {
        this.coverLetterRepository = coverLetterRepository;
    }

    @RequestMapping(method = RequestMethod.POST, produces = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> insert(@RequestBody String coverLetterStr) throws Exception {
        CoverLetter coverLetter = XmlUtility.convertXMLToObject(CoverLetter.class, coverLetterStr);
        coverLetterRepository.insert(coverLetter);
        return ResponseUtility.Ok();
    }

    @RequestMapping(value = "/{publicationId}", method = RequestMethod.GET, produces = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> findByPublicationId(@PathVariable("publicationId") String publicationId) throws Exception{
        String coverLetterStr = coverLetterRepository.findByPublicationId(publicationId);
        return ResponseUtility.Ok(coverLetterStr);
    }
}