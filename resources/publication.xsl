<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:p1="http://ftn.uns.ac.rs/xml2019/publication"
    version="2.0">
    
    <xsl:template match="/">
        <html>
            <head>
                <title><xsl:value-of select="p1:publication/p1:title"/></title>
            </head>
            <body
                style="font-family: Times New Roman; margin-left: 50px; margin-right: 50px;">
                <h1 style="text-align: center; font-weight: bold;">
                    <xsl:value-of select="p1:publication/p1:title" />
                </h1>
                
                <br />
                <p>
                    Publication date
                    <xsl:value-of
                        select="p1:publication/p1:header/p1:publicationDate"/>
                </p>
                <p>
                    Revision number
                    <xsl:value-of
                        select="p1:publication/p1:header/p1:revisionNumber" />
                </p>
                
                <p style="text-align: center; font-weight: normal;">
                    <xsl:for-each
                        select="p1:publication/p1:authors/p1:author">
                        <i>
                           <xsl:value-of select="p1:name"  />
                        </i>
                        <br />
                        <i>
                            <xsl:value-of select="p1:university" />
                        </i>
                        <br /><br />
                    </xsl:for-each>
                </p>
                <br /> <br /><br/>
                
                <p  style="text-align: center; font-size: 20px;"><b>Abstract</b></p>
                <p>
                    <b>Purpose&#xA0;&#xA0;</b>
                    <xsl:value-of
                        select="p1:publication/p1:abstract/p1:purpose" />
                    <br />
                    <b>Problem&#xA0;&#xA0;</b>
                    <xsl:value-of
                        select="p1:publication/p1:abstract/p1:problem" />
                    <br />
                    <b>method&#xA0;&#xA0;</b>
                    <xsl:value-of
                        select="p1:publication/p1:abstract/p1:method" />
                    <br />
                    <b>results&#xA0;&#xA0;</b>
                    <xsl:value-of
                        select="p1:publication/p1:abstract/p1:results" />
                    <br />
                    <b>conclusions&#xA0;&#xA0;</b>
                    <xsl:value-of
                        select="p1:publication/p1:abstract/p1:conclusions" />
                </p>
                <br /> <br /><br/>
                
                <b style="font-size: 20px;"> Keywords</b>
                <p style="font-weight:bold; font-style:italic;">
                    <xsl:value-of
                        select="p1:publication/p1:keywords" />
                    <br /><br/><br/>
                </p>
                
                <p style="text-align: center; font-weight: normal;">
                    <xsl:for-each select="p1:publication/p1:section">
                        <i>
                            <p  style="text-align: center; font-size: 20px;"><b><xsl:value-of select="p1:heading" /></b></p>
                            <br /><br />
                            <xsl:value-of select="p1:content/p1:text" />
                            <xsl:value-of select="p1:content/p1:imageContent/image" />
                            <xsl:value-of select="p1:content/p1:imageContent/about" />
                            <br /> <br />
                            <xsl:for-each select="p1:subsection">
                                
                                <p>
                                    <p  style="text-align: center; font-size: 14px;"><b><xsl:value-of select="p1:heading" /></b></p>
                                    <br /><br />
                                    <xsl:value-of select="p1:content/p1:text" />
                                    <xsl:if test="p1:content/p1:imageContent/p1:image">
                                        <br/>
                                        <xsl:value-of select="p1:content/p1:imageContent/p1:image" /><br/>
                                        <xsl:value-of select="p1:content/p1:imageContent/p1:about" />
                                    </xsl:if>
                                </p>
                                <br /><br />
                            </xsl:for-each>
                        </i>
                        <br /><br /><br />
                        
                    </xsl:for-each>
                </p>
                
                
                <p style="text-align: center; font-size: 20px;"> <b>Bibliography</b></p>
                <ul style="list-style: none; padding: 0; margin: 0;">
                    <xsl:for-each select="p1:publication/p1:bibliography/p1:reference">
                        <li>
                            <xsl:value-of select = "position()" />
                            <xsl:text >)  </xsl:text>
                            <xsl:value-of select="." />
                        </li>
                    </xsl:for-each>
                </ul>
                
            </body>
        </html>
    </xsl:template>
    
</xsl:stylesheet>