using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;
using System.IO;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsCommon.Get, "PendingImports")]
    public class GetPendingImports : MATargetCmdlet
    {
        [Parameter]
        public SwitchParameter Delta { get; set; }

        [Parameter]
        public SwitchParameter Hologram { get; set; }

        protected override void ProcessRecord()
        {
            foreach (var item in this.MAInstance.GetPendingImports(true, true, true))
            {
                if (this.Delta.IsPresent)
                {
                    this.WriteObject(item.PendingImportDelta);
                }
                else if (this.Hologram.IsPresent)
                {
                    this.WriteObject(item.PendingImportHologram);
                }
                else
                {
                    this.WriteObject(item);
                }
            }
        }
    }
}
