
# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD


./Publish.ps1 -ProjectPath "Heroes.Utils.DebugBoot/Heroes.Utils.DebugBoot.csproj" `
              -PackageName "Heroes.Utils.DebugBoot" `
              -PublishOutputDir "Publish/ToUpload" `
              -BuildR2R false

Pop-Location