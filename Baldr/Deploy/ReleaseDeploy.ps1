<#
.SYNOPSIS
    Deploys dotnet core application

    PublishDirectory needs to be an absolute path otherwise some files may not publish.
#>

param(
    [string]$ProjectDirectory,
    [string]$PublishDirectory,
    [string]$Configuration
)

cd $ProjectDirectory
dotnet restore
dotnet publish $ProjectDirectory -o $PublishDirectory -c $Configuration