using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsCommon.Clear, "FullSyncWarning")]
    public class ClearFullSyncWarning : MATargetCmdlet
    {
        protected override void ProcessRecord()
        {
            this.MAInstance.SuppressFullSyncWarning();
        }
    }
}
