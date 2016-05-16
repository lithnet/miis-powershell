using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsCommon.Get, "MAStatistics")]
    public class GetMAStatistics : MATargetCmdlet
    {
        protected override void ProcessRecord()
        {
            this.WriteObject(this.MAInstance.Statistics);
        }
    }
}
