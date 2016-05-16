# Lithnet FIM/MIM Synchronization Service PowerShell Module
(aka MIIS PowerShell)

The Lithnet FIM/MIM Synchronization Service PowerShell Module provides tools that allow interactions with the FIM/MIM Synchronization engine that goes beyond what is exposed via the supported WMI provider.

Before proceeeding, read the following disclaimer

***
> WARNING: USE THIS TOOL AT YOUR OWN RISK

> This tool is provided for testing and diagnostic purposes and is intended for use in development and test environments. Any problems that arise from the use of the tool are not supported by the developers or by Microsoft.

> The PowerShell module exposes functionality using a combination of 
 * Supported WMI interfaces
 * Wrapping existing PowerShell modules
 * Wrapping existing executables
 * Libraries that the synchronization client UI uses to interface with the sync engine itself. These libraries are undocumented APIs. 

> The module does NOT interface with the sync engine database in any way.

> It does not provide any mechanism to alter the internal configuration of the sync engine, unless an executable or documented API is available for that. 

> The developers make no warranties as to the suitability of these tools for use in your environment, nor will we be liable for any financial or other damages arising from the use of these tools.
***

If you are ok with all that, then happy PowerShell-ing!

[Read the getting started guide and list of cmdlets](https://github.com/lithnet/miis-powershell/wiki)

[Download the latest release](https://github.com/lithnet/miis-powershell/releases)
