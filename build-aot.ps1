# Build AspireMcpServer with AOT for multiple platforms

param(
    [Parameter()]
    [ValidateSet("win-x64", "linux-x64", "osx-x64", "osx-arm64", "all")]
    [string]$Runtime = "win-x64",
    
    [Parameter()]
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Release"
)

function Build-Runtime {
    param($TargetRuntime)
    
    Write-Host "Building for $TargetRuntime..." -ForegroundColor Green
    
    $publishArgs = @(
        "publish"
        "AspireMcpServer\AspireMcpServer.csproj"
        "-c", $Configuration
        "-r", $TargetRuntime
        "--self-contained"
        "/p:PublishAot=true"
        "/p:PublishSingleFile=true"
        "/p:PublishTrimmed=true"
    )
    
    & dotnet @publishArgs
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✓ Successfully built for $TargetRuntime" -ForegroundColor Green
        $outputPath = "AspireMcpServer\bin\$Configuration\net9.0\$TargetRuntime\publish"
        Write-Host "Output: $outputPath" -ForegroundColor Yellow
    } else {
        Write-Host "✗ Failed to build for $TargetRuntime" -ForegroundColor Red
    }
}

Write-Host "AspireMcpServer AOT Build Script" -ForegroundColor Cyan
Write-Host "Configuration: $Configuration" -ForegroundColor White

if ($Runtime -eq "all") {
    $runtimes = @("win-x64", "linux-x64", "osx-x64", "osx-arm64")
    foreach ($rt in $runtimes) {
        Build-Runtime $rt
        Write-Host ""
    }
} else {
    Build-Runtime $Runtime
}

Write-Host "Build process completed!" -ForegroundColor Green
