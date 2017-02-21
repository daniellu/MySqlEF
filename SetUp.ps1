function RenameProject($oldProjectName, $newProjectName)
{
	Write-Host "Old Project name: " $oldProjectName
	Write-Host "New Project name:" $newProjectName
	
	$oldProjectFileComplateName = "Source/" + $oldProjectName + "/" + $oldProjectName + ".csproj"
	$newProjectFileComplateName = $newProjectName + ".csproj"
	
	Write-Host $oldProjectFileComplateName
	Write-Host $newProjectFileComplateName
	
	$oldProjectFolderComplateName = "Source/" + $oldProjectName
	$newProjectFolderComplateName = $newProjectName
	
	Rename-Item $oldProjectFileComplateName $newProjectFileComplateName
	Rename-Item $oldProjectFolderComplateName $newProjectFolderComplateName
}

# Set-ExecutionPolicy Unrestricted

Write-Host "Initialing the project..."
$SolutionName = Read-Host -Prompt 'Please input the solution name'
Write-Host "The solution name is:" $SolutionName

Rename-Item "ProjectTemplate.sln" "$SolutionName.sln"
RenameProject -oldProjectName "ProjectTemplate.Data" -newProjectName "$SolutionName.Data"
RenameProject -oldProjectName "ProjectTemplate.Domain" -newProjectName "$SolutionName.Domain"
RenameProject -oldProjectName "ProjectTemplate.Executable" -newProjectName "$SolutionName.Executable"
RenameProject -oldProjectName "ProjectTemplate.Web" -newProjectName "$SolutionName.Web"



$files = Get-ChildItem -Path "Source\*" -Include "*.cs","*.csproj","*.cshtml" -Recurse -Force
foreach ($file in $files)
{
  Write-Host $file
 (Get-Content $file) -replace "ProjectTemplate", $SolutionName | Set-Content $file
}

Write-Host "Solution initialize finishes. Ready to rock!!!!"

