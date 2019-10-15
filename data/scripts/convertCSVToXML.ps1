# Set The Formatting
$xmlsettings = New-Object System.Xml.XmlWriterSettings
$xmlsettings.Indent = $true
$xmlsettings.IndentChars = "    "


# Set the File Name Create The Document
$XmlWriter = [System.XML.XmlWriter]::Create("..\original\SCA_ChivList-Latest.xml", $xmlsettings)

# Write the XML Decleration and set the XSL
$xmlWriter.WriteStartDocument()
$xmlWriter.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='display.xsl'")

# Start the Root Element
$xmlWriter.WriteStartElement("knights")


$file = Import-Csv "..\original\SCA_ChivList-Latest.csv"
foreach ($line in $file)
{
	if (($line.NAME).length -gt 0) {
		$xmlWriter.WriteStartElement("knight") # <-- Start <Object>

		#  "PRECEDENCE","KNIGHT","MASTER","NAME","TYPE","DATE ELEVATED","A.S.","KINGDOM","KINGDOM PRECEDENCE","RESIGNED OR REMOVED","PASSED AWAY","NOTES"

			$xmlWriter.WriteElementString("society_precedence",$line.PRECEDENCE)
			$xmlWriter.WriteElementString("society_knight_number",$line.KNIGHT)
			$xmlWriter.WriteElementString("society_master_number",$line.MASTER)
			$xmlWriter.WriteElementString("name",$line.NAME)
			$xmlWriter.WriteElementString("type",$line.TYPE)
			$xmlWriter.WriteElementString("date_elevated",$line.'DATE ELEVATED')
			$xmlWriter.WriteElementString("anno_societatous",$line.'A.S.')
			$xmlWriter.WriteElementString("kingdom_of_elevation",$line.'KINGDOM')
			$xmlWriter.WriteElementString("kingdom_precedence",$line.'KINGDOM PRECEDENCE')
			$xmlWriter.WriteElementString("resigned_or_removed",$line.'RESIGNED OR REMOVED')
			$xmlWriter.WriteElementString("passed_away",$line.'PASSED AWAY')
			$xmlWriter.WriteElementString("notes",$line.'notes')

			$xmlWriter.WriteElementString("squires", $null)

	    $xmlWriter.WriteEndElement() # <-- End <Object>
	} 
}

$xmlWriter.WriteEndElement() # <-- End <Root> 

# End, Finalize and close the XML Document
$xmlWriter.WriteEndDocument()
$xmlWriter.Flush()
$xmlWriter.Close()

Write-Host "Complete ..."