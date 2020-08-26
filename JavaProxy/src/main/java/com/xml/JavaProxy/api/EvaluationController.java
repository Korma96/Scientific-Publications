package com.xml.JavaProxy.api;

import com.xml.JavaProxy.model.Evaluation;
import com.xml.JavaProxy.repository.EvaluationRepository;
import com.xml.JavaProxy.util.ResponseUtility;
import com.xml.JavaProxy.util.XmlUtility;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;


@RestController
@RequestMapping(value = "/evaluation")
public class EvaluationController {

    @Autowired
    private EvaluationRepository evaluationRepository;

    @Autowired
    public EvaluationController(EvaluationRepository evaluationRepository) {
        this.evaluationRepository = evaluationRepository;
    }

    @RequestMapping(value = "/insert", method = RequestMethod.POST, produces = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> insert(@RequestBody String evaluationStr) throws Exception {
        Evaluation evaluation = XmlUtility.convertXMLToObject(Evaluation.class, evaluationStr);
        evaluationRepository.insert(evaluation);
        return ResponseUtility.Ok("success");
    }

    @RequestMapping(value = "by-publicationId/{publicationId}", method = RequestMethod.GET, produces = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> findByPublicationId(@PathVariable("publicationId") String publicationId) throws Exception{
        String evaluations = evaluationRepository.findByPublicationId(publicationId);
        return ResponseUtility.Ok(evaluations);
    }

    @RequestMapping(value = "by-publicationId-reviewer/{publicationId}/{reviewer}", method = RequestMethod.GET, produces = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> findByPublicationIdAndReviewer(@PathVariable("publicationId") String publicationId, @PathVariable("reviewer") String reviewer) throws Exception{
        String evaluation = evaluationRepository.findByPublicationIdAndReviewer(publicationId, reviewer);
        return ResponseUtility.Ok(evaluation);
    }
}
