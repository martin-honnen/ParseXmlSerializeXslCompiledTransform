<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:mf="http://example.com/mf"
  exclude-result-prefixes="mf">

  <xsl:output method="xml" cdata-section-elements="text"/>

  <xsl:template match="re">
    <text>
      <xsl:value-of select="mf:Serialize(mf:ParseXmlFragment(mf:ParseXmlFragment(bl)))"/>
    </text>
  </xsl:template>

</xsl:stylesheet>
