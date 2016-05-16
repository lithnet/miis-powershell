using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;
using System.IO;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsData.Sync, "CSObject")]
    public class SyncCSObject : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true), ValidateNotNullOrEmpty]
        public CSObject CSObject { get; set; }

        [Parameter]
        public SwitchParameter Commit { get; set; }

        [Parameter]
        public SwitchParameter Delta { get; set; }

        protected override void ProcessRecord()
        {
            this.WriteObject(this.CSObject.Sync(this.Commit.IsPresent, this.Delta.IsPresent));
        }
    }
}
