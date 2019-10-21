<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:template match="/">
                <xsl:apply-templates/>
    </xsl:template>

    <xsl:template match="knights">
        <knights>
            <xsl:apply-templates select="knight"/>
        </knights>
    </xsl:template>

    <xsl:template match="knight">
        <knight>
            <xsl:apply-templates select="society_precedence"/>
            <xsl:apply-templates select="name"/>
            <xsl:apply-templates select="type"/>
			<xsl:apply-templates select="squire-names"/>
            <xsl:apply-templates select="squires"/>
        </knight>
    </xsl:template>

    <xsl:template match="society_precedence">
        <society_precedence><xsl:value-of select="."/></society_precedence>
    </xsl:template>

    <xsl:template match="name">
        <name><xsl:value-of select="."/></name>
    </xsl:template>
	
    <xsl:template match="type">
        <type><xsl:value-of select="."/></type>
    </xsl:template>

	<xsl:template match="squire-names">
		<squire-names>
            <xsl:for-each select="../squires/knight">
			    <xsl:value-of select="name"/>|
            </xsl:for-each>
		</squire-names>
	</xsl:template>

    <xsl:template match="squires">
        <squires>
            <xsl:apply-templates select="knight"/>
        </squires>
    </xsl:template>

</xsl:stylesheet>
