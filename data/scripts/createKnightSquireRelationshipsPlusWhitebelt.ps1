Function Write-Knight{

    param (
        [Parameter(Mandatory)]
        [System.Xml.XmlElement]$knightNode
    )

    #$xmlWriter.WriteStartElement("knight")

    $knightChildNodes = $knightNode.ChildNodes
    Write-Host "Write-Knight knightChildNodes.count: " $knightChildNodes.count -BackgroundColor White -ForegroundColor Black
    foreach ($knightChildNode in $knightChildNodes){
            Write-Host "Write-Knight - knightChildNodes.localname: " $knightChildNode.localname -BackgroundColor White -ForegroundColor Black
        if ($knightChildNode.localname -eq "society_precedence"){
            Write-Host "Write-Knight looking for society_precedence.InnerText: " $knightChildNode.InnerText -BackgroundColor White -ForegroundColor Black
 
            $knightSPrecedence = $knightChildNode.InnerText
            $csvRecord = $whiteBeltCSV | Where-Object {$_.PRECEDENCE -eq $knightSPrecedence}

            if ($null -eq $csvRecord){
                $foundData = $false
            }
            else {

                Write-Host "Write-Knight FOUND" -BackgroundColor Gray -ForegroundColor DarkBlue
                Write-Host "Write-Knight -----------------------------------------------------------------------------------------------------------------------------" -BackgroundColor White -ForegroundColor Gray
                Write-Host "Write-Knight society_precedence " $csvRecord."PRECEDENCE" -BackgroundColor Gray -ForegroundColor DarkBlue
                #if($csvRecord."PRECEDENCE") {$xmlWriter.WriteElementString("society_precedence", $csvRecord."PRECEDENCE")}
                
                Write-Host "Write-Knight society_knight_number " $csvRecord."KNIGHT" -BackgroundColor Gray -ForegroundColor DarkBlue
                #if($csvRecord."KNIGHT") {$xmlWriter.WriteElementString("society_knight_number", $csvRecord."KNIGHT")}
                
                Write-Host "Write-Knight society_master_number " $csvRecord."MASTER" -BackgroundColor Gray -ForegroundColor DarkBlue
                #if($csvRecord."MASTER") {$xmlWriter.WriteElementString("society_master_number", $csvRecord."MASTER")}              
               
                Write-Host "Write-Knight date_elevated " $csvRecord."DATE ELEVATED" -BackgroundColor Gray -ForegroundColor DarkBlue
                #if($csvRecord."DATE ELEVATED") {$xmlWriter.WriteElementString("date_elevated", $csvRecord."DATE ELEVATED")}
                
                Write-Host "Write-Knight anno_societatous " $csvRecord."A.S." -BackgroundColor Gray -ForegroundColor DarkBlue
                #if($csvRecord."A.S.") {$xmlWriter.WriteElementString("anno_societatous",  $csvRecord."A.S.")}
                
                Write-Host "Write-Knight kingdom " $csvRecord."KINGDOM" -BackgroundColor Gray -ForegroundColor DarkBlue
                #if($csvRecord."KINGDOM") {$XmlWriter.WriteElementString("kingdom_of_elevation", $csvRecord."KINGDOM")}
                
                Write-Host "Write-Knight kingdom_precedence " $csvRecord."KINGDOM" -BackgroundColor Gray -ForegroundColor DarkBlue
                #if($csvRecord."KINGDOM PRECENCE") {$XmlWriter.WriteElementString("kingdom_precedence", $csvRecord."KINGDOM PRECENCE")}
                
                Write-Host "Write-Knight resigned_or_removed " $csvRecord."RESIGNED OR REMOVED" -BackgroundColor Gray -ForegroundColor DarkBlue
                #if($csvRecord."RESIGNED OR REMOVED") {$XmlWriter.WriteElementString("resigned_or_removed", $csvRecord."RESIGNED OR REMOVED")}
                
                Write-Host "Write-Knight passed_away " $csvRecord."PASSED AWAY" -BackgroundColor Gray -ForegroundColor DarkBlue
                #if($csvRecord."PASSED AWAY") {$XmlWriter.WriteElementString("passed_away", $csvRecord."PASSED AWAY")}
                
                Write-Host "Write-Knight notes " $csvRecord."NOTES" -BackgroundColor Gray -ForegroundColor DarkBlue
                #if($csvRecord."NOTES") {$XmlWriter.WriteElementString("notes", $csvRecord."NOTES")}
                $foundData = $true
            }

 
        }
        if ($knightChildNode.localname -eq "society_precedence") {
            if ($foundData -eq $false) {
                if ($knightChildNode.InnerText) {
                    Write-Host "Write-Knight society_precedence " $knightChildNode.OuterXml -BackgroundColor White -ForegroundColor Black
                    #$xmlWriter.WriteElementString("society_precedence", $knightChildNode.InnerText)
                }
            }
        }
        if ($knightChildNode.localname -eq "name"){
            if ($knightChildNode.InnerText) {
                Write-Host "Write-Knight name " $knightChildNode.OuterXml -BackgroundColor White -ForegroundColor Black
                #$xmlWriter.WriteElementString("name", $knightChildNode.InnerText)
            }
        }
        if ($knightChildNode.localname -eq "type"){
            if ($knightChildNode.InnerText) {
                Write-Host "Write-Knight type " $knightChildNode.OuterXml -BackgroundColor White -ForegroundColor Black
                #$xmlWriter.WriteElementString("type", $knightChildNode.InnerText)
            }
        }        
        if ($knightChildNode.localname -eq "squires"){
            # $squires = $knightChildNode.ChildNodes
            Write-Host "Sending Write-Squires knightChildNode: " $knightChildNode.OuterXml -ForegroundColor White -BackgroundColor Red
            $writeSquirescounter = $writeSquirescounter + 1
            if ($writeSquirescounter -eq 1) {
                Start-Job -ScriptBlock $scriptBlockWriteSquires -ArgumentList @($knightNode) 
            } else {
                & $scriptBlockWriteKnight -knightNode $knightNode 
            }
        } 
    }
    #$xmlWriter.WriteEndElement() #knight
}

Function Write-Squires()
{
    param (
        [Parameter(Mandatory)]
        [System.Xml.XmlElement]$squiresNode
    )

   Write-Host "Inside Write-Squires" -ForegroundColor White -BackgroundColor Red
   Write-Host "Write-Squires - squiresNode.OuterXml: " $squiresNode.OuterXml -ForegroundColor White -BackgroundColor Red
   #$xmlWriter.WriteStartElement("squires")
   $nodes = $squiresNode.ChildNodes
   Write-Host "Write-Squires - nodes.count: " $nodes.count -ForegroundColor White -BackgroundColor Red
   foreach ($node in $nodes) {
       Write-Host "Write-Squires - node.localname: " $node.localname -ForegroundColor White -BackgroundColor Red
       if ($node.localname -eq "knight") {
           Write-Host "Sending Write-Knight node.OuterXml: " $node.OuterXml -ForegroundColor White -BackgroundColor Red
           $writeKnightcounter = $writeKnightcounter + 1
           Write-Host "Knight.OuterXml: " $knightNode.OuterXml -ForegroundColor White -BackgroundColor Red
           if ($writeKnightcounter -eq 1) {
            $jobs = Start-Job -ScriptBlock $scriptBlockWriteKnight -ArgumentList @($knightNode) 
           }
           else {
            $jobs = & $scriptBlockWriteKnight -ArgumentList @($knightNode) 
           }
       }
   }
   foreach ($job in $jobs.ChildJobs)
   {
       Receive-Job -Job $job -Wait
       Write-Host "Received Job: " $job + " ID: " + $job.ID
   }
      #$xmlWriter.WriteEndElement() # squires
} #end Write-Squires


$scriptBlockWriteKnight = {
    param ($knightNode)
    Write-Knight -knightNode $knightNode
}

$scriptBlockWriteSquires = {
    param ($squiresNode)
    Write-Knight -squiresNode $squiresNode
}


$relationshipsPathAndFilename = "..\ks_relationships-squirenames.xml"
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
$xmlsettings.IndentChars = "  "

# Set the File Name Create The Document
$XmlWriter = [System.XML.XmlWriter]::Create($outputPathAndFilename, $xmlsettings)
Write-Host "XmlWriter opened path: " $outputPathAndFilename  -BackgroundColor DarkGreen -ForegroundColor White

# write the header
$xmlWriter.WriteStartDocument()

# set XSL statements
$xmlWriter.WriteProcessingInstruction("xml-stylesheet", "type='text/xsl' href='style.xsl'")

# create root element "knight" and add some attributes to it
$xmlWriter.WriteStartElement('knights')

$knightNodes = $xmlRelationships.SelectNodes("//knights/knight")
Write-Host "knightNodes.count: " $knightNodes.count -BackgroundColor Black -ForegroundColor DarkYellow
$writeKnightcounter = 0
$writeSquirescounter = 0
foreach ($knightNode in $knightNodes) {
    Write-Host "node.localname: " $knightNode.localname
    if ($knightNode.localname -eq "knight"){
        $writeKnightcounter = $writeKnightcounter + 1
        Write-Host "Knight.OuterXml: " $knightNode.OuterXml -BackgroundColor Black -ForegroundColor DarkYellow
        Write-Host "knightNode.GetType: " $knightNode.GetType() -BackgroundColor Black -ForegroundColor DarkYellow
        if ($writeKnightcounter -eq 1) {
         $jobs = Start-Job -ScriptBlock $scriptBlockWriteKnight -ArgumentList @($knightNode) 
        } else {
          $jobs = & $scriptBlockWriteKnight -knightNode $knightNode 
        }
        Write-Host "Jobs ---------------------------------------- " -BackgroundColor Black -ForegroundColor DarkYellow
        Write-Host Get-Job -IncludeChildJobs
    }
}
foreach ($job in $jobs.ChildJobs)
{
    Receive-Job -Job $job -Wait
    Write-Host "Received Job: " $job + " ID: " + $job.ID -BackgroundColor Black -ForegroundColor DarkYellow
}


Write-Host "Closing knights node ... " -BackgroundColor Black -ForegroundColor DarkYellow
# close the "knights" node:
$xmlWriter.WriteEndElement()

# finalize the document:
Write-Host "Writing the end of the document node ... " -BackgroundColor Black -ForegroundColor DarkYellow
$xmlWriter.WriteEndDocument()
Write-Host "Flushing ... " -BackgroundColor Black -ForegroundColor DarkYellow
$xmlWriter.Flush()
Write-Host "Closing ... " -BackgroundColor Black -ForegroundColor DarkYellow
$xmlWriter.Close()
