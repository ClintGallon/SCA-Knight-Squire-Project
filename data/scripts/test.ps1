
$whiteBeltXMLPathAndFilename = "..\original\SCA_ChivList-Latest.xml"
[Xml]$whiteBeltXML = Get-Content $whiteBeltXMLPathAndFilename

Write-Host "whitebelt csv file loaded ..." -BackgroundColor Green -ForegroundColor Black


foreach knight in $whiteBeltXML

Select-Xml -Xml $whiteBeltXML -XPath "/*/knight/*" | Where-Object -Property "society_precedence" -EQ "10"
# $xmlRecord = Select-Xml -Xml $whiteBeltXML -XPath "//knights/knight/society_precedence" | Where-Object {$_.society_precedence -eq "10"}

# Write-Host "Write-Knight xmlRecord: " $xmlRecord -BackgroundColor Yellow -ForegroundColor Black


Write-Host "Complete ..."