Function Get-FromCSVRecord($csvRecord, $lookingfor){
	$csvCols = $csvRecord -split ";"
	Write-Host "csvCols.count" $csvCols.Count
	Write-Host "lookingfor: " $lookingfor
	$strReturn = switch ($lookingfor)
	{
		"PRECEDENCE" {$csvCols[0]}
		"KNIGHT" {$csvCols[1]} 
		"MASTER" {$csvCols[2]}
		"NAME"{$csvCols[3]}
		"TYPE" {$csvCols[4]}
		"DATE ELEVATED" {$csvCols[5]}
		"A.S." {$csvCols[6]}
		"KINGDOM" {$csvCols[7]}
		"KINGDOM PRECEDENCE" {$csvCols[8]}
		"RESIGNED OR REMOVED" {$csvCols[9]}
		"PASSED AWAY" {$csvCols[10]}
		"NOTES" {$csvCols[11]}
	}

	return $strReturn
}

$relationshipsPathAndFilename = "..\ks_relationships.xml"
$whiteBeltCSVPathAndFilename = "..\original\SCA_ChivList-Latest.csv"

[Xml]$xmlRelationships = Get-Content -Path $relationshipsPathAndFilename
Write-Host "relationships xml file loaded ..." -BackgroundColor Green -ForegroundColor Black

$whiteBeltCSV = Import-Csv -Path $whiteBeltCSVPathAndFilename
Write-Host "chivlist csv file loaded ..." -BackgroundColor Green -ForegroundColor Black

Write-Host "Knights-Start"

$relKnightNodes = $xmlRelationships.SelectNodes("//knights/knight")


foreach ($relKnightNode in $relKnightNodes) {

    Write-Host "node.localname: " $relKnightNode.localname
	Write-Host "relKnightNodes.count: " $relKnightNodes.Count
	
    if ($relKnightNode.localname -eq "knight"){

		Write-Host "Found a knight"
		Write-Host "relKnightNode.OuterXml: " $relKnightNode.OuterXml
		
		forEach ($relKnightSubNode in $relKnightNode.ChildNodes){
			if ($relKnightSubNode.localname -eq "society_precedence") {
				$knightSPrecedence = $relKnightSubNode.InnerText
				Write-Host "knightSPrecedence: " $knightSPrecedence
				$csvRecord = $whiteBeltCSV | Where-Object {$_.PRECEDENCE -eq $knightSPrecedence}
				Write-Host "csvRecord: " $csvRecord
				Write-Host "PRECEDENCE - " -$csvRecord.PRECEDENCE
			
				$elskn = $xmlRelationships.CreateElement("society_knight_number")
				$elskn.InnerText = $csvRecord.KNIGHT
				$relKnightNode.AppendChild($elskn)

				$elsmn = $xmlRelationships.CreateElement("society_master_number")
				$elsmn.InnerText = $csvRecord.MASTER
				$relKnightNode.AppendChild($elsmn)

				$elde = $xmlRelationships.CreateElement("date_elevated")
				$elde.InnerText = $csvRecord.'DATE ELEVATED'
				$relKnightNode.AppendChild($elde)	

				$elas = $xmlRelationships.CreateElement("anno_societatous")
				$elas.InnerText = $csvRecord.'A.S.'
				$relKnightNode.AppendChild($elas)	

				$elkingdom = $xmlRelationships.CreateElement("kingdom_of_elevation")
				$elkingdom.InnerText = $csvRecord.KINGDOM
				$relKnightNode.AppendChild($elkingdom)	

				$elkp = $xmlRelationships.CreateElement("kingdom_precedence")
				$elkp.InnerText = $csvRecord.'KINGDOM PRECEDENCE'
				$relKnightNode.AppendChild($elkp)					

				$elrr = $xmlRelationships.CreateElement("resigned_or_removed")
				$elrr.InnerText = $csvRecord.'RESIGNED OR REMOVED'
				$relKnightNode.AppendChild($elrr)	

				$elpa = $xmlRelationships.CreateElement("passed_away")
				$elpa.InnerText = $csvRecord.'PASSED AWAY'
				$relKnightNode.AppendChild($elpa)	

				$elnotes = $xmlRelationships.CreateElement("notes")
				$elnotes.InnerText = $csvRecord.'PASSED AWAY'
				$relKnightNode.AppendChild($elnotes)					

				Write-Host "relKnightNode.OuterXml: " $relKnightNode.OuterXml;

				break
			}
		}
    }
}

Write-Host "Knights-End"