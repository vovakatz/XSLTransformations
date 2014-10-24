<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="urn:my-scripts">
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
        <xsl:value-of select="user:GetDate()"/>
      </a>
    </li>
  </xsl:template>
  <msxsl:script language="C#" implements-prefix="user">
    <![CDATA[
     public string GetDate()
     {
       return DateTime.Now.ToShortDateString();
     }
      ]]>
  </msxsl:script>
</xsl:stylesheet>
