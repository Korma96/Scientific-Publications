package com.xml.JavaProxy.util;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.IOException;
import java.io.StringWriter;
import java.nio.charset.StandardCharsets;

import javax.xml.parsers.ParserConfigurationException;
import javax.xml.transform.OutputKeys;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;
import javax.xml.transform.stream.StreamSource;

import org.springframework.core.io.InputStreamResource;
import org.springframework.stereotype.Component;
import org.xml.sax.SAXException;

import com.itextpdf.text.Document;
import com.itextpdf.text.DocumentException;
import com.itextpdf.text.pdf.PdfWriter;
import com.itextpdf.tool.xml.XMLWorkerHelper;

public class PdfUtil {


    public static String transform(String xmlValue, String xslPath, String xsdPath)
            throws SAXException, ParserConfigurationException, IOException {
        final TransformerFactory factory = TransformerFactory.newInstance();
        final StringWriter writer = new StringWriter();
        final DOMSource source = new DOMSource(DomParser.buildDocument(xmlValue, xsdPath));
        final StreamResult result = new StreamResult(writer);

        try {
            final StreamSource transformSource = new StreamSource(new File(xslPath));
            final Transformer transformer = factory.newTransformer(transformSource);

            transformer.setOutputProperty("{http://xml.apache.org/xalan}indent-amount", "2");
            transformer.setOutputProperty(OutputKeys.INDENT, "yes");
            transformer.setOutputProperty(OutputKeys.METHOD, "xml");

            transformer.transform(source, result);

            return writer.toString();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
    }

    public static InputStreamResource toPdf(String xmlValue, String xslPath, String xsdPath)
            throws SAXException, ParserConfigurationException {
        final Document document = new Document();
        final ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
        final PdfWriter writer;

        try {
            writer = PdfWriter.getInstance(document, outputStream);
            document.open();
            XMLWorkerHelper.getInstance().parseXHtml(writer, document,
                    new ByteArrayInputStream(transform(xmlValue, xslPath, xsdPath).getBytes(StandardCharsets.UTF_8)));
        } catch (DocumentException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }

        document.close();
        return new InputStreamResource(new ByteArrayInputStream(outputStream.toByteArray()));
    }
}
