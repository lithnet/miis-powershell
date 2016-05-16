using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsLifecycle.Stop, "ManagementAgent")]
    public class StopManagementAgent : MATargetCmdlet
    {
        [Parameter(Mandatory = false, Position = 3)]
        public SwitchParameter Async { get; set; }

        protected override void ProcessRecord()
        {
            if (this.Async)
            {
                this.MAInstance.StopAsync();
            }
            else
            {
                this.MAInstance.Stop();
            }
        }
    }
}
