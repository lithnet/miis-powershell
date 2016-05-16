using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsCommon.Get, "LastRunDetails")]
    public class GetLastRunDetails : MATargetCmdlet
    {
        [Parameter(Position = 2)]
        public int Count { get; set; }

        protected override void ProcessRecord()
        {
           
            if (this.Count > 0)
            {
                this.WriteObject(this.MAInstance.GetRunHistory(this.Count), true);
            }
            else
            {
                this.WriteObject(this.MAInstance.GetLastRun(), true);
            }
        }
    }
}
