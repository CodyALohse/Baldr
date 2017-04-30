<#
.SYNOPSIS
    Deploys dotnet core application

    PublishDirectory needs to be an absolute path otherwise some files may not publish.
#>

param(
    [string]$ProjectDirectory,
    [string]$PublishDirectory,
    [string]$Configuration,
    [string]$ServiceName
)


Function Publish-App{
    Param(
        [string]$ProjectDirectory,
        [string]$PublishDirectory,
        [string]$Configuration
    )

    Write-Host "Switching to project directory" + $ProjectDirectory -ForegroundColor Green
    cd $ProjectDirectory

    Write-Host "Starting Dotnet restore" -ForegroundColor Green
    dotnet restore

    Write-Host "Starting Dotnet publish" -ForegroundColor Green
    dotnet publish $ProjectDirectory -o $PublishDirectory -c $Configuration

    Write-Host "Dotnet publish finished" -ForegroundColor Green
}


Get-Service -DisplayName $ServiceName | Sort-Object Status, Displayname |
ForEach-Object {
    $Displayname = $_.DisplayName
    If($_.Status -eq "Running")
    {
        Write-Host "Service is running - Stopping Service" -ForegroundColor Green
        Stop-Service -DisplayName $Displayname
    }

    Publish-App -ProjectDirectory $ProjectDirectory -PublishDirectory $PublishDirectory -Configuration $Configuration

    Write-Host "Starting service"
    Start-Service -DisplayName $Displayname
}
