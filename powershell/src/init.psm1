<#
.SYNOPSIS
Get Secrets Broker status summary.

.DESCRIPTION
Summary information includes Safeguard Appliance association information,
Secrets Broker configuration, and list of plugins.

.EXAMPLE
Get-SgDevOpsStatus
#>
function Get-SgDevOpsStatus
{
    [CmdletBinding()]
    Param(
    )

    if (-not $PSBoundParameters.ContainsKey("ErrorAction")) { $ErrorActionPreference = "Stop" }
    if (-not $PSBoundParameters.ContainsKey("Verbose")) { $VerbosePreference = $PSCmdlet.GetVariableValue("VerbosePreference") }

    Import-Module -Name "$PSScriptRoot\configuration.psm1" -Scope Local
    Import-Module -Name "$PSScriptRoot\plugins.psm1" -Scope Local

    Write-Host "---Appliance Connection---"
    Write-Host (Get-SgDevOpsApplianceStatus | Format-List | Out-String)

    Write-Host "---Configuration---"
    Write-Host (Get-SgDevOpsConfiguration | Format-List | Out-String)

    Write-Host "---Plugins---"
    Write-Host (Get-SgDevOpsPlugin | Format-Table | Out-String)
}


function Initialize-SgDevOps
{
    [CmdletBinding()]
    Param(
        [Parameter(Mandatory=$false, Position=0)]
        [string]$ServiceAddress,
        [Parameter(Mandatory=$false)]
        [int]$ServicePort,
        [Parameter(Mandatory=$false, Position=1)]
        [string]$Appliance,
        [Parameter(Mandatory=$false)]
        [switch]$Gui,
        [Parameter(Mandatory=$false)]
        [int]$ApplianceApiVersion = 3,
        [Parameter(Mandatory=$false)]
        [int]$ServiceApiVersion = 1
    )

    if (-not $PSBoundParameters.ContainsKey("ErrorAction")) { $ErrorActionPreference = "Stop" }
    if (-not $PSBoundParameters.ContainsKey("Verbose")) { $VerbosePreference = $PSCmdlet.GetVariableValue("VerbosePreference") }

    Write-Host -ForegroundColor Yellow "Associating Secrets Broker to trust SPP appliance for authentication ..."
    Initialize-SgDevOpsAppliance -ServiceAddress $ServiceAddress -ServicePort $ServicePort -ServiceApiVersion $ServiceApiVersion `
                                 -Appliance $Appliance -ApplianceApiVersion $ApplianceApiVersion -Gui:$Gui -Insecure -Force:$Force

    Write-Host -ForegroundColor Yellow "Connecting to Secrets Broker using SPP user ..."
    Connect-SgDevOps -ServiceAddress $ServiceAddress -ServicePort $ServicePort -Gui:$Gui -Insecure:$Insecure

    # TODO: test whether insecure flag is necessary
    #       if so, walk the user through fixing it

    Write-Host -ForegroundColor Yellow "Configuring Secrets Broker instance account in SPP ..."

    # TODO: Configure SgDevOps user
}
