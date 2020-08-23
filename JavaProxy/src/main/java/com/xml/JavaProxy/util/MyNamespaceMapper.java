package com.xml.JavaProxy.util;

import com.sun.xml.internal.bind.marshaller.NamespacePrefixMapper;

import java.util.HashMap;
import java.util.Map;

public class MyNamespaceMapper extends NamespacePrefixMapper {

    private Map<String, String> namespaceMap = new HashMap<>();

    /**
     * Create mappings.
     */
    public MyNamespaceMapper() {
        namespaceMap.put("http://www.w3.org/2001/XMLSchema", "xs");
        namespaceMap.put("http://www.w3.org/2007/XMLSchema-versioning", "vc");
        namespaceMap.put("http://ftn.uns.ac.rs/xml2019/publication", "p1");
    }
    @Override
    public String getPreferredPrefix(String namespaceUri, String suggestion, boolean requirePrefix) {
        return namespaceMap.getOrDefault(namespaceUri, suggestion);
    }
}