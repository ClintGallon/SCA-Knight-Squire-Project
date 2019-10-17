Function Write-Knight($knightNode){

    $xmlWriter.WriteStartElement("knight")

    $knightChildNodes = $knightNode.ChildNodes
    Write-Host "Write-Knight knightChildNodes.count: " $knightChildNodes.count -BackgroundColor White -ForegroundColor Black
    foreach ($knightChildNode in $knightChildNodes){
            Write-Host "Write-Knight - knightChildNodes.localname: " $knightChildNode.localname -BackgroundColor White -ForegroundColor Black
        if ($knightChildNode.localname -eq "society_chiv_number"){
            Write-Host "Write-Knight looking for society_chiv_number.InnerText: " $knightChildNode.InnerText -BackgroundColor White -ForegroundColor Black
            [bool]$foundData = $false
            foreach ($csvRecord in $whiteBeltCSV){
                if ($csvRecord."PRECEDENCE" -eq $knightChildNode.InnerText){
                    Write-Host "Write-Knight FOUND" -BackgroundColor White -ForegroundColor Blue
                    #Write-Host "Write-Knight -----------------------------------------------------------------------------------------------------------------------------" -BackgroundColor White -ForegroundColor Blue
                    #Write-Host "Write-Knight society_precedence " $csvRecord."PRECEDENCE" -BackgroundColor Black -ForegroundColor White
                    if($csvRecord."PRECEDENCE") {$xmlWriter.WriteElementString("society_precedence", $csvRecord."PRECEDENCE")}
                    
                    #Write-Host "Write-Knight society_knight_number " $csvRecord."KNIGHT" 
                    if($csvRecord."KNIGHT") {$xmlWriter.WriteElementString("society_knight_number", $csvRecord."KNIGHT")}
                    
                    #Write-Host "Write-Knight society_master_number " $csvRecord."MASTER"
                    if($csvRecord."MASTER") {$xmlWriter.WriteElementString("society_master_number", $csvRecord."MASTER")}
                    
                    #Write-Host "Write-Knight type " $csvRecord."TYPE"
                    if($csvRecord."TYPE") {$xmlWriter.WriteElementString("type", $csvRecord."TYPE")}
                    
                    #Write-Host "Write-Knight date_elevated " $csvRecord."DATE ELEVATED" 
                    if($csvRecord."DATE ELEVATED") {$xmlWriter.WriteElementString("date_elevated", $csvRecord."DATE ELEVATED")}
                    
                    #Write-Host "Write-Knight anno_societatous " $csvRecord."A.S."
                    if($csvRecord."A.S.") {$xmlWriter.WriteElementString("anno_societatous",  $csvRecord."A.S.")}
                    
                    #Write-Host "Write-Knight kingdom " $csvRecord."KINGDOM" 
                    if($csvRecord."KINGDOM") {$XmlWriter.WriteElementString("kingdom_of_elevation", $csvRecord."KINGDOM")}
                    
                    #Write-Host "Write-Knight kingdom_precedence " $csvRecord."KINGDOM" 
                    if($csvRecord."KINGDOM PRECENCE") {$XmlWriter.WriteElementString("kingdom_precedence", $csvRecord."KINGDOM PRECENCE")}
                    
                    #Write-Host "Write-Knight resigned_or_removed " $csvRecord."RESIGNED OR REMOVED" 
                    if($csvRecord."RESIGNED OR REMOVED") {$XmlWriter.WriteElementString("resigned_or_removed", $csvRecord."RESIGNED OR REMOVED")}
                    
                    #Write-Host "Write-Knight passed_away " $csvRecord."PASSED AWAY" 
                    if($csvRecord."PASSED AWAY") {$XmlWriter.WriteElementString("passed_away", $csvRecord."PASSED AWAY")}
                    
                    #Write-Host "Write-Knight notes " $csvRecord."NOTES" 
                    if($csvRecord."NOTES") {$XmlWriter.WriteElementString("notes", $csvRecord."NOTES")}
                    $foundData = $true
                    break
                } 
            }
        }
        if ($knightChildNode.localname -eq "society_chiv_number") {
            if ($foundData -eq $false) {
                if ($knightChildNode.InnerText) {$xmlWriter.WriteElementString("society_precedence", $knightChildNode.InnerText)}
            }
        }
        if ($knightChildNode.localname -eq "name"){
            if ($knightChildNode.InnerText) {$xmlWriter.WriteElementString("name", $knightChildNode.InnerText)}
        }
        if ($knightChildNode.localname -eq "squires"){
            # $squires = $knightChildNode.ChildNodes
            Write-Host "Sending Write-Squires knightChildNode: " $knightChildNode.OuterXml -ForegroundColor White -BackgroundColor Red
            Write-Squires($knightChildNode.OuterXml)
        } 
    }
    $xmlWriter.WriteEnddElement() #knight
}

Function Write-Squires($squiresNode)
{
   Write-Host "Inside Write-Squires" -ForegroundColor White -BackgroundColor Red
   Write-Host "Write-Squires - squiresNode.OuterXml: " $squiresNode.OuterXml -ForegroundColor White -BackgroundColor Red
   $xmlWriter.WriteStartElement("squires")
   $nodes = $squiresNode.ChildNodes
   Write-Host "Write-Squires - nodes.count: " $nodes.count -ForegroundColor White -BackgroundColor Red
   foreach ($node in $nodes) {
       Write-Host "Write-Squires - node.localname: " $node.localname -ForegroundColor White -BackgroundColor Red
       if ($node.localname -eq "knight") {
           Write-Host "Sending Write-Knight node.OuterXml: " $node.OuterXml -ForegroundColor White -BackgroundColor Red
           Write-Knight($node.OuterXml)
       }
       if ($node.localname -eq "squire") {
            Write-Host "Sending Write-Squire node.OuterXml: " $node.OuterXml -ForegroundColor White -BackgroundColor Red
            Write-Squire($node.OuterXml)
       }
   }
   $xmlWriter.WriteEndElement() # squires
} #end Write-Squires

Function Write-Squire($squireNode)
{
    $xmlWriter.WriteStartElement("squire")
    Write-Host "Inside Write-Squire" -ForegroundColor Yellow -BackgroundColor Blue
    Write-Host "Write-Squire - squireNode: " $squireNode.OuterXml -ForegroundColor Yellow -BackgroundColor Blue
    $nodes = $squireNode.ChildNodes
    Write-Host "Write-Squire - nodes.count: " $nodes.count -ForegroundColor Yellow -BackgroundColor Blue
    foreach ($node in $nodes){
        Write-Host "Write-Squire - node.localname: " $node.localname -ForegroundColor Yellow -BackgroundColor Blue
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

$outputPathAndFilename = "c:\temp\" + $currentRunDateTime.ToString("yyyyMMdd-HHmmss") + "_ks_output.xml" 

$xmlsettings = New-Object System.Xml.XmlWriterSettings
$xmlsettings.Indent = $true
$xmlsettings.IndentChars = "    "

# Set the File Name Create The Document
$XmlWriter = [System.XML.XmlWriter]::Create($outputPathAndFilename, $xmlsettings)
Write-Host "XmlWriter opened path: " $outputPathAndFilename  -BackgroundColor DarkGreen -ForegroundColor White

# write the header
$xmlWriter.WriteStartDocument()

# set XSL statements
$xmlWriter.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='style.xsl'")

# create root element "knight" and add some attributes to it
$xmlWriter.WriteStartElement('knights')

$knightNodes = $xmlRelationships.SelectNodes("//knights/*")
Write-Host "knightNodes.count: " $knightNodes.count
$counter = 0
foreach ($knightNode in $knightNodes) {
    Write-Host "node.localname: " $knightNode.localname
    if ($knightNode.localname -eq "knight"){
        $counter = $counter + 1
        Write-Host "Knight.OuterXml: " $knightNode.OuterXml
        Write-Knight($knightNode)
        if ($counter -eq 43) {
            break
        }
    }
}

Write-Host "Closing knights node ... " 
# close the "knights" node:
$xmlWriter.WriteEndElement()

# finalize the document:
Write-Host "Writing the end of the document node ... "
$xmlWriter.WriteEndDocument()
Write-Host "Flushing ... "
$xmlWriter.Flush()
Write-Host "Closing ... "
$xmlWriter.Close()









