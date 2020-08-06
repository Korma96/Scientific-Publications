package com.xml.JavaProxy.api;


import com.xml.JavaProxy.model.Workflow;
import com.xml.JavaProxy.repository.WorkFlowRepository;
import com.xml.JavaProxy.util.ResponseUtility;
import com.xml.JavaProxy.util.XmlUtility;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping(value = "/workflow")
public class WorkFlowController {

    private WorkFlowRepository workFlowRepository;

    @Autowired
    public WorkFlowController(WorkFlowRepository workFlowRepository) {
        this.workFlowRepository = workFlowRepository;
    }

    @RequestMapping(method = RequestMethod.POST, consumes = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> insertWorkFlow(@RequestBody String workFlowStr) throws Exception {
        Workflow workflow = XmlUtility.convertXMLToObject(Workflow.class, workFlowStr);
        workFlowRepository.insert(workflow);
        return ResponseUtility.Ok();
    }

    @RequestMapping(value = "{publicationId}", method = RequestMethod.GET, produces = MediaType.APPLICATION_XML_VALUE)
    public ResponseEntity<String> findById(@PathVariable("publicationId") String publicationId) throws Exception{
        String workflow = workFlowRepository.findByPublicationId(publicationId);
        return ResponseUtility.Ok(workflow);
    }
}
