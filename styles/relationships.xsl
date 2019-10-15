<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:template match="/">
                <xsl:apply-templates/>
    </xsl:template>

    <xsl:template match="knights">
        <knights>
            <xsl:apply-templates select="knight"/>
        </knights>
    </xsl:template>

    <xsl:template match="squires">
        <knights>
            <xsl:apply-templates select="knight"/>
            <xsl:apply-templates select="squire"/>
        </knights>
    </xsl:template>

    <xsl:template match="knight">
        <knight>
            <xsl:apply-templates select="society_chiv_number"/>
            <xsl:apply-templates select="name"/>
            <xsl:apply-templates select="squires"/>
        </knight>
    </xsl:template>

    <xsl:template match="squire">
        <squire>
            <xsl:apply-templates select="name"/>
        </squire>
    </xsl:template>

    <xsl:template match="society_chiv_number">
        <society_chiv_number>
            <xsl:value-of select="."/>
        </society_chiv_number>
    </xsl:template>

    <xsl:template match="name">
        <name>
            <xsl:value-of select="."/>
        </name>
    </xsl:template>

    <xsl:template match="squires">
        <squires>
            <xsl:apply-templates select="knight"/>
            <xsl:apply-templates select="squire"/>
        </squires>
    </xsl:template>

</xsl:stylesheet>
