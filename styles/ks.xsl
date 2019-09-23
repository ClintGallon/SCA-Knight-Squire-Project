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
          title | name | date elevated | A.S. | society chivalry # | society knight # | kingdom elevated | kingdom chivalry #
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
    <xsl:variable name="displayRed">|Duke|Duchess|</xsl:variable>
    <xsl:variable name="displayOrange">|Count|Countess|Earl|Jarl|Comte|Comtessa|</xsl:variable>
    <xsl:variable name="displayHotPink">|Viscount|Viscountess|</xsl:variable>
    <xsl:variable name="displayWhite">|Sir|Ritter|Riddari|Equis|</xsl:variable>
    <xsl:variable name="displayLime">|Master|Mistress|</xsl:variable>
    <li>
      <xsl:choose>
        <xsl:when test="contains($displayRed,title)">
          <xsl:choose>
            <xsl:when test=".//squires">
              <span style="color:red; text-shadow: 2px 2px black;" class="caret">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:when>
            <xsl:otherwise>
              <span style="color:red; text-shadow: 2px 2px black;">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:when>
        <xsl:when test="contains($displayOrange,title)">
          <xsl:choose>
            <xsl:when test=".//squires">
              <span style="color:orange; text-shadow: 2px 2px black;" class="caret">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:when>
            <xsl:otherwise>
              <span style="color:orange; text-shadow: 2px 2px black;">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:when>
        <xsl:when test="contains($displayHotPink,title)">
          <xsl:choose>
            <xsl:when test=".//squires">
              <span style="color:hotpink; text-shadow: 2px 2px black;" class="caret">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:when>
            <xsl:otherwise>
              <span style="color:hotpink; text-shadow: 2px 2px black;">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:when>
        <xsl:when test="contains($displayWhite,title)">
          <xsl:choose>
            <xsl:when test=".//squires">
              <span style="color:White; text-shadow: 2px 2px black;" class="caret">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:when>
            <xsl:otherwise>
              <span style="color:White; text-shadow: 2px 2px black;">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:when>
        <xsl:when test="contains($displayLime,title)">
          <xsl:choose>
            <xsl:when test=".//squires">
              <span style="color:lime; text-shadow: 2px 2px black;" class="caret">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:when>
            <xsl:otherwise>
              <span style="color:lime; text-shadow: 2px 2px black;">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:when>        
        <xsl:otherwise>
          <span style="color:DodgerBlue; text-shadow: 2px 2px black;">
            <xsl:apply-templates select="title"/>
          </span>
        </xsl:otherwise>
      </xsl:choose>
        <xsl:apply-templates select="name"/>
 |      <xsl:apply-templates select="date_elevated"/>
 |      <xsl:apply-templates select="anno_societatous"/>
 |      <xsl:apply-templates select="society_chiv_number"/>
 |      <xsl:apply-templates select="society_knight_number"/>
 |      <xsl:apply-templates select="kingdom_of_elevation"/>
 |      <xsl:apply-templates select="kingdom_chiv_number"/>
      <xsl:apply-templates select="squires"/>
    </li>
  </xsl:template>

  <xsl:template match="squires">
    <ul class="nested">
      <xsl:apply-templates select="knight"/>
      <xsl:apply-templates select="squire"/>
    </ul>
  </xsl:template>

  <xsl:template match="squire">
    <xsl:variable name="displayRed">|Duke|</xsl:variable>
    <xsl:variable name="displayOrange">|Count|Countess|Earl|Jarl|Comte|Comtessa|</xsl:variable>
    <xsl:variable name="displayHotPink">|Viscount|Viscountess|</xsl:variable>
    <xsl:variable name="displayWhite">|Sir|Ritter</xsl:variable>
    <xsl:variable name="displayLime">|Master|Mistress|</xsl:variable>
    <li>
      <xsl:choose>
        <xsl:when test="contains($displayRed,title)">
          <xsl:choose>
            <xsl:when test=".//squires">
              <span style="color:red; text-shadow: 2px 2px black;" class="caret">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:when>
            <xsl:otherwise>
              <span style="color:red; text-shadow: 2px 2px black;">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:when>
        <xsl:when test="contains($displayOrange,title)">
          <xsl:choose>
            <xsl:when test=".//squires">
              <span style="color:orange; text-shadow: 2px 2px black;" class="caret">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:when>
            <xsl:otherwise>
              <span style="color:orange; text-shadow: 2px 2px black;">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:when>
        <xsl:when test="contains($displayHotPink,title)">
          <xsl:choose>
            <xsl:when test=".//squires">
              <span style="color:hotpink; text-shadow: 2px 2px black;" class="caret">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:when>
            <xsl:otherwise>
              <span style="color:hotpink; text-shadow: 2px 2px black;">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:when>
        <xsl:when test="contains($displayWhite,title)">
          <xsl:choose>
            <xsl:when test=".//squires">
              <span style="color:White; text-shadow: 2px 2px black;" class="caret">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:when>
            <xsl:otherwise>
              <span style="color:White; text-shadow: 2px 2px black;">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:when>
        <xsl:when test="contains($displayLime,title)">
          <xsl:choose>
            <xsl:when test=".//squires">
              <span style="color:lime; text-shadow: 2px 2px black;" class="caret">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:when>
            <xsl:otherwise>
              <span style="color:lime; text-shadow: 2px 2px black;">
                <xsl:apply-templates select="title"/>
              </span>
            </xsl:otherwise>
          </xsl:choose>
        </xsl:when>        
        <xsl:otherwise>
          <span style="color:DodgerBlue; text-shadow: 2px 2px black;">
            <xsl:apply-templates select="title"/>
          </span>
        </xsl:otherwise>
      </xsl:choose>
      <xsl:apply-templates select="name"/>
 |      <xsl:apply-templates select="date_elevated"/>
 |      <xsl:apply-templates select="anno_societatous"/>
 |      <xsl:apply-templates select="society_chiv_number"/>
 |      <xsl:apply-templates select="society_knight_number"/>
 |      <xsl:apply-templates select="kingdom_of_elevation"/>
 |      <xsl:apply-templates select="kingdom_chiv_number"/>
    </li>
  </xsl:template>

  <xsl:template match="title">
    <xsl:value-of select="."/>
 - 
  </xsl:template>

  <xsl:template match="name">
    <xsl:value-of select="."/>
  </xsl:template>

  <xsl:template match="date_elevated">
    <xsl:value-of select="."/>
  </xsl:template>

  <xsl:template match="society_chiv_number">
    SC#: <xsl:value-of select="."/>
  </xsl:template>

  <xsl:template match="society_knight_number">
    SK#: <xsl:value-of select="."/>
  </xsl:template>

  <xsl:template match="anno_societatous">
    A.S. <xsl:value-of select="."/>
  </xsl:template>

  <xsl:template match="kingdom_of_elevation">
    <xsl:value-of select="."/>
  </xsl:template>

  <xsl:template match="kingdom_chiv_number">
    KC#: <xsl:value-of select="."/>
  </xsl:template>

</xsl:stylesheet>