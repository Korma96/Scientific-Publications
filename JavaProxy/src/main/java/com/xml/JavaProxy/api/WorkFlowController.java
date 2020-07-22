package com.xml.JavaProxy.api;

import com.xml.JavaProxy.model.User;
import com.xml.JavaProxy.repository.WorkFlowRepository;
import com.xml.JavaProxy.util.ResponseUtility;
import com.xml.JavaProxy.util.XmlUtility;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

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
        workFlowRepository.insert(workFlowStr);
        return ResponseUtility.Ok();
    }

}
