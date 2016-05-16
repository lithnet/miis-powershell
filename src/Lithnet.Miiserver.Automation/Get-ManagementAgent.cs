using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsCommon.Get, "ManagementAgent")]
    public class GetManagementAgent : Cmdlet
    {
        [Parameter(ValueFromPipeline = true, Mandatory = false, Position = 1)]
        public string Name { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Reload { get; set; }

        protected override void ProcessRecord()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                foreach (ManagementAgent ma in MiisController.GetManagementAgents())
                {
                    this.WriteObject(ma);
                }
            }
            else
            {
                this.WriteObject(MiisController.GetManagementAgent(this.Name, this.Reload.IsPresent));
            }
        }
    }
}
