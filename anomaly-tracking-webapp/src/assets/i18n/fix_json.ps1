$Path = "src\assets\i18n\fr.json"

[string[]]$fileContents = Get-Content -Path $Path | Sort-Object  | Where-Object {$_} | Select -Unique

$fileContents = $fileContents | Where-Object {$_} | Foreach-Object { if (!$line){$_.trim()} } | Where-Object {$_}
$fileContents = $fileContents[0..($fileContents.length-3)]

Foreach($line in $fileContents) 
{ 
    $index = $fileContents.IndexOf($line)
    if ( $line -and $line.contains(":") )
    {
        $fileContents[$index] = "`t" + $fileContents[$index]
    }
    if ( $line -and !$line.endswith(",") -and $index -lt $fileContents.Count-1 )
    {
        $fileContents[$index] = $fileContents[$index] + ","
    }
    if ( $line -and $line.endswith(",") -and $index -eq $fileContents.Count-1 )
    {
        $fileContents[$index] = $fileContents[$index].Substring(0,$fileContents[$index].Length-1)
    }
    if ( $line -and $line.contains(":") -and ! $line.contains(" :") )
    {
        $fileContents[$index] = $fileContents[$index].replace(':',' :')
    }
    if ( $line -and $line.contains(":") -and ! $line.contains(": ") )
    {
        $fileContents[$index] = $fileContents[$index].replace(':',': ')
    }
}


$last = $fileContents[($fileContents.length-1)]
$penultimate = $fileContents[($fileContents.length-2)].SubString(0,$fileContents[($fileContents.length-2)].Length-1)

if( $last -eq $penultimate)
{
    $fileContents = $fileContents[0..($fileContents.length-2)]
    $temp = $fileContents[($fileContents.length-1)].SubString(0,$fileContents[($fileContents.length-1)].Length-1)
    $fileContents[($fileContents.length-1)] = $temp
}

$fileContents = $fileContents | Select -Unique

$newFileContent = @()
$newFileContent += "{"
$newFileContent += $fileContents
$newFileContent += "}"
$newFileContent | Set-Content $Path



