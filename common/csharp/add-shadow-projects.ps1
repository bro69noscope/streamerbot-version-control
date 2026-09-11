<#
.SYNOPSIS
  Creates a <Name>.shadow.csproj next to every SubActions/**/<Name>.cs that lacks one
  and adds it to csharp.sln under a solution folder mirroring the directory path.

  The csproj is a single line: everything (target framework, CPH shim, Streamer.bot
  reference, the <Name>.cs compile item) comes from
  Directory.Build.props, keyed on the project name.

.EXAMPLE
  .\add-shadow-projects.ps1
  .\add-shadow-projects.ps1 -WhatIf
#>
[CmdletBinding(SupportsShouldProcess)]
param()

$ErrorActionPreference = 'Stop'
$root = $PSScriptRoot
$sln = Join-Path $root 'csharp.sln'
$subActions = Join-Path $root 'SubActions'

$existing = @(dotnet sln $sln list | Select-Object -Skip 2 | ForEach-Object { $_.Trim() })

$csFiles = Get-ChildItem $subActions -Recurse -Filter *.cs |
    Where-Object { $_.FullName -notmatch '[\\/](obj|bin|wip)[\\/]' }

$created = 0
$added = 0
foreach ($cs in $csFiles) {
    $csproj = Join-Path $cs.DirectoryName ($cs.BaseName + '.shadow.csproj')
    $relCsproj = [IO.Path]::GetRelativePath($root, $csproj)

    if (-not (Test-Path $csproj)) {
        if ($PSCmdlet.ShouldProcess($relCsproj, 'create')) {
            Set-Content -Path $csproj -Value '<Project Sdk="Microsoft.NET.Sdk" />' -Encoding utf8NoBOM
            Write-Host "created  $relCsproj"
            $created++
        }
    }

    if ($existing -notcontains $relCsproj) {
        $folder = ([IO.Path]::GetRelativePath($root, $cs.DirectoryName)) -replace '\\', '/'
        if ($PSCmdlet.ShouldProcess($relCsproj, "add to sln under $folder")) {
            dotnet sln $sln add $csproj --solution-folder $folder | Out-Null
            Write-Host "sln add  $relCsproj  ->  $folder"
            $added++
        }
    }
}

Write-Host "done: $created csproj created, $added added to sln"
