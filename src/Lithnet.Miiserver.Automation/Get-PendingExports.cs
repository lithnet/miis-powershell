using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;
using System.IO;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsCommon.Get, "PendingExports")]
    public class GetPendingExports : MATargetCmdlet
    {
        [Parameter]
        public SwitchParameter Delta { get; set; }

        [Parameter]
        public SwitchParameter Hologram { get; set; }

        protected override void ProcessRecord()
        {
            foreach (var item in this.MAInstance.GetPendingExports(true, true, true))
            {
                if (this.Delta.IsPresent)
                {
                    this.WriteObject(item.UnappliedExportDelta);
                }
                else if (this.Hologram.IsPresent)
                {
                    this.WriteObject(item.UnappliedExportHologram);
                }
                else
                {
                    this.WriteObject(item);
                }
            }
        }
    }
}
