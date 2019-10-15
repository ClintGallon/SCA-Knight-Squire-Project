Function Write-Knight($knightNode){

    $xmlWriter.WriteStartElement("knight")

    $knightChildNodes = $knightNode.ChildNodes
    # Write-Host "Write-Knight knightChildNodes.count: " $knightChildNodes.count
    foreach ($knightChildNode in $knightChildNodes){
            # Write-Host "Write-Knight - knightChildNodes.localname: " $knightChildNode.localname
        if ($knightChildNode.localname -eq "society_chiv_number"){
            # Write-Host "Write-Knight looking for society_chiv_number.InnerText: " $knightChildNode.InnerText
            foreach ($csvRecord in $whiteBeltCSV){
                if ($csvRecord."PRECEDENCE" -eq $knightChildNode.InnerText){
                    Write-Host "Write-Knight FOUND" -BackgroundColor White -ForegroundColor Blue
                    Write-Host "Write-Knight -----------------------------------------------------------------------------------------------------------------------------" -BackgroundColor White -ForegroundColor Blue
                    $xmlWriter.WriteElementString("society_precedence", $csvRecord."PRECEDENCE") 
                    Write-Host "Write-Knight society_precedence " $csvRecord."PRECEDENCE" -BackgroundColor Black -ForegroundColor White
                    $xmlWriter.WriteElementString("society_knight_number ", $csvRecord."KNIGHT")
                    Write-Host "Write-Knight society_knight_number " $csvRecord."KNIGHT" 
                    $xmlWriter.WriteElementString("society_master_number", $csvRecord."MASTER")
                    Write-Host "Write-Knight society_master_number " $csvRecord."MASTER"
                    $xmlWriter.WriteElementString("type", $csvRecord."TYPE")
                    Write-Host "Write-Knight type " $csvRecord."TYPE"
                    $xmlWriter.WriteElementString("date_elevated", $csvRecord."DATE ELEVATED")
                    Write-Host "Write-Knight date_elevated " $csvRecord."DATE ELEVATED" 
                    $xmlWriter.WriteElementString("anno_societatous",  $csvRecord."A.S.")
                    Write-Host "Write-Knight anno_societatous " $csvRecord."A.S."
                    $XmlWriter.WriteElementString("kingdom_of_elevation", $csvRecord."KINGDOM")
                    Write-Host "Write-Knight kingdom " $csvRecord."KINGDOM" 
                    $XmlWriter.WriteElementString("kingdom_precedence", $csvRecord."KINGDOM PRECENCE")
                    Write-Host "Write-Knight kingdom_precedence " $csvRecord."KINGDOM" 
                    $XmlWriter.WriteElementString("resigned_or_removed", $csvRecord."RESIGNED OR REMOVED")
                    Write-Host "Write-Knight resigned_or_removed " $csvRecord."RESIGNED OR REMOVED" 
                    $XmlWriter.WriteElementString("passed_away", $csvRecord."PASSED AWAY")
                    Write-Host "Write-Knight passed_away " $csvRecord."PASSED AWAY" 
                    $XmlWriter.WriteElementString("notes", $csvRecord."NOTES")
                    Write-Host "Write-Knight notes " $csvRecord."NOTES" 
                    break
                } else {
                    # Write-Host "Write-Knight NOT FOUND" -BackgroundColor White -ForegroundColor Blue
                    $xmlWriter.WriteElementString("society_chiv_number", $knightChildNode.InnerText)
                }
            }
        }
        if ($knightChildNode.localname -eq "name"){
            $xmlWriter.WriteElementString("name", $knightChildNode.InnerText)
        }
        if ($knightChildNode.localname -eq "squires"){
            $squires = $knightChildNode.ChildNodes
            Write-Squires($squires)
        } 
    }
    $xmlWriter.WriteEndElement() #knight
    $xmlWriter.Flush()
}

Function Write-Squires($squiresNode)
{
   Write-Host "Inside Write-Squires" -ForegroundColor White -BackgroundColor Red
   # Write-Host "Write-Squires - squiresNode.OuterXml: " $squiresNode.OuterXml
   $xmlWriter.WriteStartElement("squires")
   $nodes = $squiresNode.ChildNodes
   Write-Host "Write-Squires - nodes.count: " $nodes.count
   foreach ($node in $nodes) {
       Write-Host "Write-Squires - node.localname: " $node.localname
       if ($node.localname -eq "knight") {
           Write-Knight($node)
       }
       if ($node.localname -eq "squire") {
           Write-Squire($node)
       }
   }
   $xmlWriter.WriteEndElement()
} #end Write-Squires

Function Write-Squire($squireNode)
{
    $xmlWriter.WriteStartElement("squire")
    Write-Host "Inside Write-Squire" -ForegroundColor White -BackgroundColor Red
    Write-Host "Write-Squire - squireNode: " $squireNode.OuterXml
    $nodes = $squireNode.ChildNodes
    Write-Host "Write-Squire - nodes.count: " $nodes.count
    foreach ($node in $nodes){
        Write-Host "Write-Squire - node.localname: " $node.localname
        if ($node.localname -eq "name"){
            $xmlWriter.WriteElemantString("name", $node.InnerText)
        }
    }
    $xmlWriter.WriteEndElement() #squire
} #end Write-Squire

$relationshipsPathAndFilename = "..\ks_relationships.xml"
$whiteBeltCSVPathAndFilename = "..\original\SCA_ChivList-Latest.csv"

[Xml]$xmlRelationships = Get-Content -Path $relationshipsPathAndFilename
Write-Host "relationships xml file loaded ..." -BackgroundColor Green -ForegroundColor Black

$whiteBeltCSV = Import-Csv -Path $whiteBeltCSVPathAndFilename
Write-Host "chivlist csv file loaded ..." -BackgroundColor Green -ForegroundColor Black

# this is where the document will be saved:
$currentRunDateTime = Get-Date

$Path = "..\" + $currentRunDateTime.ToString("yyyy-MM-dd-HH-mm-ss") + "_ks_output.xml" 

# get an XMLTextWriter to create the XML
$XmlWriter = New-Object System.XMl.XmlTextWriter($Path,$Null)
Write-Host "XmlWriter opened path: " + $Path  -BackgroundColor Green -ForegroundColor Black


# choose a pretty formatting:
$xmlWriter.Formatting = 'Indented'
$xmlWriter.Indentation = 1
$XmlWriter.IndentChar = "`t"

# write the header
$xmlWriter.WriteStartDocument()

# set XSL statements
$xmlWriter.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='style.xsl'")

# create root element "knight" and add some attributes to it
$xmlWriter.WriteStartElement('knights')


$knightNodes = $xmlRelationships.SelectNodes("//knights/*")
Write-Host "knightNodes.count: " $knightNodes.count
foreach ($knightNode in $knightNodes) {
    # Write-Host "node.localname: " $knightNode.localname
    if ($knightNode.localname -eq "knight"){
        # Write-Host "Knight.OuterXml: " $knightNode.OuterXml
        Write-Knight($knightNode)
    }
}

# close the "knights" node:
$xmlWriter.WriteEndElement()

# finalize the document:
$xmlWriter.WriteEndDocument()
$xmlWriter.Flush()
$xmlWriter.Close()


