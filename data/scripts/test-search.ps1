
$whiteBeltCSVPathAndFilename = "..\original\SCA_ChivList-Latest.csv"

$knightName="Eiolf Eriksson" 

Write-Host "Seaching for - " $knightName

$csvRecord = $whiteBeltCSV | Where-Object {$_.NAME -eq $knightName}

Write-Host "csvRecord - " $csvRecord