#!/usr/bin/pwsh

# -------- These variables are controlled by repo-pathes tool ---------
$repoRoot = Resolve-Path "$PSScriptRoot/../../.."
$scriptsRoot = "$repoRoot/scripts"
# ---------------------------------------------------------------------

$wrongSlnFiles = @()

foreach ($slnFile in &"$PSScriptRoot/config/sln-files.ps1"){
    $anyCpuConfigFound = $false;
    foreach ($configuration in &"$scriptsRoot/get-sln-configurations.ps1" $slnFile){
        if ($configuration.Split("|")[1] -eq "Any CPU"){
            $anyCpuConfigFound = $true;
            break;
        }
    }
    if ($anyCpuConfigFound){
        $wrongSlnFiles += $slnFile
    }
}

if ($wrongSlnFiles){
    Write-Host "Solution files with Any CPU configurations:"
    $wrongSlnFiles
    throw "Found solution files with Any CPU configuration."
}
