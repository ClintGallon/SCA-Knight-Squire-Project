$relationshipsPathAndFilename = "..\ks_relationships-squirenames.xml"
$whiteBeltXmlPathAndFilename = "..\SCA_ChivList-Latest.xml"

[Xml]$xmlRelationships = Get-Content $relationshipsPathAndFilename
Write-Host "relationships xml file loaded ..." -BackgroundColor Green -ForegroundColor Black

[Xml]$xmlWhiteBelt = Get-Content $whiteBeltXmlPathAndFilename
Write-Host "whitebelt xml file loaded ..." -BackgroundColor Green -ForegroundColor Black

# this is where the document will be saved:
$currentRunDateTime = Get-Date
$outputPathAndFilename = "c:\temp\" + $currentRunDateTime.ToString("yyyyMMdd-HHmmss") + "_ks_output.xml" 

$wbNodes = $xmlWhiteBelt.DocumentElement.ChildNodes
            

foreach ($wbNode in $wbNodes){
	if ($wbNode.LocalName -eq "knight"){
		$knightChildNodes = $wbNode.ChildNodes
		foreach ($knightChildNode in $knightChildNodes){
			if ($knightChildNode.LocalName -eq "society_precedence"){
				$societyPrecedence = $knightChildNode.InnerText
				Write-Host "society precedence: " $societyPrecedence -BackgroundColor White -ForegroundColor Blue
			}
			if ($knightChildNode.LocalName -eq "name"){
				$searchName = $knightChildNode.InnerText
				Write-Host "Searching for squires of: " $searchName -BackgroundColor White -ForegroundColor Black			
			
				$foundNodes = $xmlRelationships.Document.ChildNodes.ChildNodes | Where-Object {$_name -eq $searchName}

				if ($foundNodes -eq $null)
				{
					Write-Host "NOT Found: " $searchName
				}
				else {
					Write-Host "Found him: " $searchName
				}
			}
		}
	}
}