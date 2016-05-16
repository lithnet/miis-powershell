using System;
using System.IO;
using System.Xml;
using System.Management.Automation;
using Lithnet.Miiserver.Client;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsCommon.Get, "SavedRunHistory")]
    public class GetSavedRunHistory : Cmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 1)]
        public string File { get; set; }

        protected override void ProcessRecord()
        {
            this.WriteObject(RunDetails.LoadRunDetails(this.File), true);
        }
    }
}
