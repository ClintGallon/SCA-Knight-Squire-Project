<xsl:stylesheet version="1.0"
                xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

    <xsl:template match="/">
        <html>
            <head>
                <style>
                    * {
                    color: silver;
                    font-family: "Veranda", Sans-serif;
                    }
                    body {
                    background-color: dimgray;
                    }
                    h2 {
                    color: white;
                    }

                    ul, #myUL {
                    list-style-type: none;
                    }
                    #myUL {
                    margin: 0;
                    padding: 0;
                    }
                    .caret {
                    cursor: pointer;
                    -webkit-user-select: none; /* Safari 3.1+ */
                    -moz-user-select: none; /* Firefox 2+ */
                    -ms-user-select: none; /* IE 10+ */
                    user-select: none;
                    }
                    .caret::before {
                    content: "\25B6";
                    color: silver;
                    display: inline-block;
                    margin-right: 6px;
                    }
                    .caret-down::before {
                    -ms-transform: rotate(90deg); /* IE 9 */
                    -webkit-transform: rotate(90deg); /* Safari */'
                    transform: rotate(90deg);
                    }
                    .nested {
                    display: none;
                    }
                    .active {
                    display: block;
                    }
                </style>
            </head>
            <body>
                <h2>Knight Squire Project - 2.0</h2>
                <p>
                    name | date elevated | A.S. | society chivalry # | society knight # | kingdom elevated | kingdom chivalry #
                </p>
                <xsl:apply-templates/>
                <script>
                    <![CDATA[
            var toggler = document.getElementsByClassName("caret");
            var i;

            for (i = 0; i < toggler.length; i++) {
              toggler[i].addEventListener("click", function() {
                this.parentElement.querySelector(".nested").classList.toggle("active");
                this.classList.toggle("caret-down");
              });
            }
            for (i = 0; i < toggler.length; i++) {
              toggler[i].click();
            }

        ]]>
                </script>

            </body>
        </html>
    </xsl:template>

    <xsl:template match="knights">
        <ul id="myUL">
            <xsl:apply-templates select="knight"/>
        </ul>
    </xsl:template>

    <xsl:template match="knight">
        <li>
            <xsl:choose>
                <xsl:when test=".//squires/*">
                    <span style="color:DeepPink; text-shadow: 2px 2px black;" class="caret">
                        <xsl:apply-templates select="name"/>
                    </span>
                    | <xsl:apply-templates select="date_elevated"/> | <xsl:apply-templates select="anno_societatous"/> | <xsl:apply-templates select="society_precedence"/> | <xsl:apply-templates select="society_knight_number"/> | <xsl:apply-templates select="kingdom_of_elevation"/> | <xsl:apply-templates select="kingdom_precedence"/>
                </xsl:when>
                <xsl:when test=".//type/text()='Knight'">
                    <span style="color:white; text-shadow: 2px 2px black;">
                        <xsl:apply-templates select="name"/>
                    </span>
                    | <xsl:apply-templates select="date_elevated"/> | <xsl:apply-templates select="anno_societatous"/> | <xsl:apply-templates select="society_precedence"/> | <xsl:apply-templates select="society_knight_number"/> | <xsl:apply-templates select="kingdom_of_elevation"/> | <xsl:apply-templates select="kingdom_precedence"/>
                </xsl:when>
                <xsl:when test=".//type/text()='Master'">
                    <span style="color:LightSteelBlue; text-shadow: 2px 2px black;">
                        <xsl:apply-templates select="name"/>
                    </span>
                    | <xsl:apply-templates select="date_elevated"/> | <xsl:apply-templates select="anno_societatous"/> | <xsl:apply-templates select="society_precedence"/> | <xsl:apply-templates select="society_knight_number"/> | <xsl:apply-templates select="kingdom_of_elevation"/> | <xsl:apply-templates select="kingdom_precedence"/>
                </xsl:when>
                <xsl:when test=".//type/text()='squire'">
                    <span style="color:LightCoral; text-shadow: 2px 2px black;">
                        <xsl:apply-templates select="name"/>
                    </span>
                </xsl:when>
                <xsl:otherwise>
                    <span>
                        <xsl:apply-templates select="name"/>
                    </span>
                </xsl:otherwise>
            </xsl:choose>
            | <xsl:apply-templates select="type"/>
            <xsl:apply-templates select="squires"/>
        </li>
    </xsl:template>

    <xsl:template match="squires">
        <ul class="nested">
            <xsl:apply-templates select="knight"/>
        </ul>
    </xsl:template>

    <xsl:template match="name">
        <xsl:value-of select="."/>
    </xsl:template>

    <xsl:template match="date_elevated">
        <xsl:value-of select="."/>
    </xsl:template>

    <xsl:template match="society_precedence">
        SC#:
        <xsl:value-of select="."/>
    </xsl:template>

    <xsl:template match="society_knight_number">
        SK#:
        <xsl:value-of select="."/>
    </xsl:template>

    <xsl:template match="anno_societatous">
        A.S.
        <xsl:value-of select="."/>
    </xsl:template>

    <xsl:template match="kingdom_of_elevation">
        <xsl:value-of select="."/>
    </xsl:template>

    <xsl:template match="kingdom_precedence">
        KC#:
        <xsl:value-of select="."/>
    </xsl:template>

    <xsl:template match="type">
        TYPE:
        <xsl:value-of select="."/>
    </xsl:template>

    <xsl:template match="notes">
        NOTES:
        <xsl:value-of select="."/>
    </xsl:template>

</xsl:stylesheet>
