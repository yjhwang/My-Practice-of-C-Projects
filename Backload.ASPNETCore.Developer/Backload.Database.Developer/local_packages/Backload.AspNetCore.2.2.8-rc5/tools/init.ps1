param($installPath, $toolsPath, $package, $project)

#### INITIALIZE
$projectFullName = $project.FullName
$fileInfo = new-object -typename System.IO.FileInfo -ArgumentList $projectFullName
$projectDirectory = $fileInfo.DirectoryName + "\"
Write-Host "Project root path: $projectDirectory"

$backloadDirectory = $projectDirectory + "Backload\"
if(!(Test-Path -Path $backloadDirectory)){
	$contentSrcDir = $installPath + "\content\*.*"
	Write-Host "Copying assets from $contentSrcDir to $projectDirectory"

	xcopy /E /D /Y /I "$contentSrcDir" "$projectDirectory"
}



