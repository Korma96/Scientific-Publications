package com.xml.JavaProxy.helper;

import org.exist.xupdate.XUpdateProcessor;

public class XUpdateTemplate {

    // to do: add target namespace

    public static final String APPEND = "<xu:modifications version=\"1.0\" xmlns:xu=\"" + XUpdateProcessor.XUPDATE_NS
            + "\">" + "<xu:append select=\"%1$s\" child=\"last()\">%2$s</xu:append>" + "</xu:modifications>";
}
