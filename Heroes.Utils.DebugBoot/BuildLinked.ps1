# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

Remove-Item "$env:RELOADEDIIMODS/sonicheroes.utils.debugboot/*" -Force -Recurse
dotnet publish "./Heroes.Utils.DebugBoot.csproj" -c Release -o "$env:RELOADEDIIMODS/sonicheroes.utils.debugboot" /p:OutputPath="./bin/Release" /p:ReloadedILLink="true"

# Restore Working Directory
Pop-Location