<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method="html" encoding="UTF-8" indent="yes"/>
  <xsl:template match='/ArrayOfNavigationLink'>
    <ul>
      <xsl:apply-templates select="NavigationLink" />
    </ul>
  </xsl:template>

  <xsl:template match="NavigationLink">
    <li>
      <a href="{Url}">
        <xsl:value-of select="Text" />
      </a>
      <ul>
        <xsl:apply-templates select="NavigationLinks/NavigationLink" />
      </ul>
    </li>
  </xsl:template>

  <xsl:template match="NavigationLink[not(NavigationLinks)]">
    <li>
      <a href="{Url}">
        <xsl:value-of select="Text" />
      </a>
    </li>
  </xsl:template>
  
</xsl:stylesheet>
