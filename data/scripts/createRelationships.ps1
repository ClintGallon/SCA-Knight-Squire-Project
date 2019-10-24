function TransformXML{
    param ($xml, $xsl, $output)

    if (-not $xml -or -not $xsl -or -not $output)
    {
        Write-Host "& .\xslt.ps1 [-xml] xml-input [-xsl] xsl-input [-output] transform-output"
        return 0;
    }

    Try
    {
        $xslt_settings = New-Object System.Xml.Xsl.XsltSettings;
        $XmlUrlResolver = New-Object System.Xml.XmlUrlResolver;
		$xslt_settings.EnableScript = 1;
		
        $xslt = New-Object System.Xml.Xsl.XslCompiledTransform;
        $xslt.Load($xsl,$xslt_settings,$XmlUrlResolver);
        $xslt.Transform($xml, $output);
    }

    Catch
    {
        $ErrorMessage = $_.Exception.Message
        $FailedItem = $_.Exception.ItemName
        Write-Host  'Error'$ErrorMessage':'$FailedItem':' $_.Exception;
        return 0
    }
    return 1

}

$xmlFilename = "C:\projects\SCA-Knight-Squire-Project\data\ks_relationships.xml"
$xslFilename = "C:\projects\SCA-Knight-Squire-Project\styles\relationships.xsl"
$outputFilename = "C:\projects\SCA-Knight-Squire-Project\data\ks_relationships-squirenames.xml"

TransformXML -xml $xmlFilename -xsl $xslFilename -output $outputFilename
