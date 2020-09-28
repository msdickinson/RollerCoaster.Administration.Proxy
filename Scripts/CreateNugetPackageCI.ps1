$Name = "RollerCoaster.Administration.Proxy"
$DateTime = [datetime]::UtcNow.ToString("yyyyMMdd-HHmmss")
$VersionSuffix = "-alpha" + $DateTime
dotnet pack $Name -c Release --version-suffix $VersionSuffix --output C:\Packages