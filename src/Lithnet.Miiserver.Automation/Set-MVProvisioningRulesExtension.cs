using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsCommon.Set, "MVProvisioningRulesExtension")]
    public class SetMVProvisioningRulesExtension : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 1)]
        public bool Enabled { get; set; }

        protected override void ProcessRecord()
        {
            SyncServer.SetProvisionRulesExtension(this.Enabled);
        }
    }
}
