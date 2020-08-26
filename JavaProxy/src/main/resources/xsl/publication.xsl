<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:p1="http://ftn.uns.ac.rs/xml2019/publication"
    version="3.0">
    
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
                        <br/><br/>
                    </xsl:for-each>
                </p>
                <br/>
                
                <p  style="text-align: center; font-size: 20px;"><b>Abstract</b></p>
                <p>
                    <br/><br />
                    <b>Purpose&#xA0;&#xA0;</b>
                    <xsl:value-of
                        select="p1:publication/p1:abstract/p1:purpose" />
                    <br/><br />
                    <b>Problem&#xA0;&#xA0;</b>
                    <xsl:value-of
                        select="p1:publication/p1:abstract/p1:problem" />
                    <br /><br />
                    <b>Method&#xA0;&#xA0;</b>
                    <xsl:value-of
                        select="p1:publication/p1:abstract/p1:method" />
                    <br /><br />
                    <b>Results&#xA0;&#xA0;</b>
                    <xsl:value-of
                        select="p1:publication/p1:abstract/p1:results" />
                    <br /><br />
                    <b>Conclusions&#xA0;&#xA0;</b>
                    <xsl:value-of
                        select="p1:publication/p1:abstract/p1:conclusions" />
                </p>
                <br/><br/><br/>
                <b style="font-size: 20px;"> Keywords</b>
                <p style="font-weight:bold; font-style:italic;">
                    <xsl:value-of
                        select="p1:publication/p1:keywords" />
                </p>
                <p style="text-align: center; font-weight: normal;">
                    <xsl:for-each select="p1:publication/p1:section">
                        <i>
                                <a><xsl:attribute name="id">
                                    <xsl:value-of select="@p1:id"/>
                                </xsl:attribute>
                                </a>
                            <br/><br/>
                            <p  style="text-align: center; font-size: 20px;"><b><xsl:value-of select="p1:heading" /></b></p>
                            <br/><br/>
                            <xsl:for-each select="p1:content">
                                <xsl:if test="p1:text">
                                    <xsl:value-of select="p1:text" />
                                </xsl:if>
                                
                                <xsl:if test="p1:link">
                                    <xsl:variable name="ref"> <xsl:value-of select="p1:link/@p1:refId"/></xsl:variable>
                                    <xsl:variable name="samepage"> <xsl:value-of select="p1:link/@p1:samepage" /></xsl:variable>
                                    <a><xsl:attribute name="href">
                                        <xsl:if test = "$samepage = 'true'">
                                            <xsl:value-of select="concat('#', $ref)"/>
                                        </xsl:if>
                                        <xsl:if test = "$samepage = 'false'">
                                            <xsl:value-of select="concat('http://localhost:8081/api/publication/getByIdHtml/', $ref)"/>
                                        </xsl:if>
                                    </xsl:attribute>
                                        <xsl:value-of select="p1:link"/>
                                    </a>
                                </xsl:if>
                                <xsl:if test="p1:imageContent/p1:image">
                                    <br/>
                                    <p style="text-align: center; font-weight: normal;">
                                        <xsl:value-of select="p1:imageContent/p1:image" />
                                        <br/>
                                        <xsl:value-of select="p1:imageContent/p1:about" />
                                    </p>
                                    <br/>
                                </xsl:if>
                            </xsl:for-each>
                            <xsl:for-each select="p1:subsection">
                                <i>
                                    <br/><br/>
                                    <p  style="text-align: center; font-size: 14px;">
                                        <xsl:value-of select = "position()" />
                                        <xsl:text >. </xsl:text>
                                        <b><xsl:value-of select="p1:heading" /></b>
                                    </p>
                                    <br />
                                    <xsl:for-each select="p1:content"> 
                                        <xsl:if test="p1:text">
                                            <xsl:value-of select="p1:text" />
                                        </xsl:if>
                                        
                                        <xsl:if test="p1:link">
                                            <br/>
                                            <xsl:variable name="ref"> <xsl:value-of select="p1:link/@p1:refId"/></xsl:variable>
                                            <xsl:variable name="samepage"> <xsl:value-of select="p1:link/@p1:samepage" /></xsl:variable>
                                            <a><xsl:attribute name="href">
                                                <xsl:if test = "$samepage = 'true'">
                                                    <xsl:value-of select="concat('#', $ref)"/>
                                                </xsl:if>
                                                <xsl:if test = "$samepage = 'false'">
                                                    <xsl:value-of select="concat('http://localhost:8081/api/publication/getByIdHtml/', $ref)"/>
                                                </xsl:if>
                                            </xsl:attribute>
                                                <xsl:value-of select="p1:link"/>
                                            </a>
                                        </xsl:if>
                                        <xsl:if test="p1:imageContent/p1:image">
                                            <br/>
                                            <p style="text-align: center; font-weight: normal;">
                                                <xsl:value-of select="p1:imageContent/p1:image" />
                                                <br/>
                                                <xsl:value-of select="p1:imageContent/p1:about" />
                                            </p>
                                            <br/>
                                        </xsl:if>
                                    </xsl:for-each>
                                </i>
                            </xsl:for-each>
                        </i>
                    </xsl:for-each>
                </p>
                <p style="text-align: center; font-size: 20px;"> <b>Bibliography</b></p> 
                <br/>
                <ul style="list-style: none; padding: 0; margin: 0;">
                    <xsl:for-each select="p1:publication/p1:bibliography/p1:reference">
                        <li>
                            <xsl:value-of select = "position()" />
                            <xsl:text >)  </xsl:text>
                               <xsl:if test = "@p1:refId">
                                   <xsl:variable name="refId"> <xsl:value-of select="@p1:refId" /></xsl:variable>
                                   <xsl:variable name="samepage"> <xsl:value-of select="@p1:samepage" /></xsl:variable>
                                   <a><xsl:attribute name="href">
                                        <xsl:if test = "$samepage = 'true'">
                                            <xsl:value-of select="concat('#', $refId)"/>
                                        </xsl:if>
                                        <xsl:if test = "$samepage = 'false'">
                                            <xsl:value-of select="concat('http://localhost:8081/api/publication/getByIdHtml/', $refId)"/>
                                        </xsl:if>
                                        </xsl:attribute>
                                        <xsl:value-of select="."/>
                                   </a>
                                </xsl:if>
                        </li>
                    </xsl:for-each>
                </ul>
                <br /><br />
            </body>
        </html>
    </xsl:template>
    
</xsl:stylesheet>