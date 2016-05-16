using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;
using System.IO;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsCommon.Get, "Connectors")]
    public class GetConnectors : MATargetCmdlet
    {
        [Parameter(Mandatory = false, Position = 2)]
        public ConnectorState? Type { get; set; }

        protected override void ProcessRecord()
        {
            IEnumerable<CSObject> results;

            if (this.Type.HasValue)
            {
                results = this.MAInstance.GetConnectors(this.Type.Value);
            }
            else
            {
                results = this.MAInstance.GetConnectors();
            }

            foreach (var item in results)
            {
                this.WriteObject(item);
            }
        }
    }
}
