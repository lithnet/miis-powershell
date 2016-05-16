using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;
using System.IO;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsCommon.Set, "DisconnectorState")]
    public class SetDisconnectorState : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline=true), ValidateNotNullOrEmpty]
        public CSObject CSObject { get; set; }

        [Parameter(Mandatory = true, Position = 2)]
        public DisconnectorState Type { get; set; }

        protected override void ProcessRecord()
        {
            if (this.CSObject.IsConnector)
            {
                throw new ArgumentException("The specfied CSObject is not a disconnector");
            }

            if ((ConnectorState)this.Type == this.CSObject.ConnectorState)
            {
                return;
            }

            this.CSObject.SetConnectorState((ConnectorState)this.Type);
        }
    }
}
