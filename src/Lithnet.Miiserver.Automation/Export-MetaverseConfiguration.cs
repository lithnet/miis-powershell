using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;
using System.IO;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsData.Export, "MetaverseConfiguration")]
    public class ExportMetaverseConfiguration : Cmdlet
    {
        [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 1), ValidateNotNullOrEmpty]
        public string Path { get; set; }

        protected override void ProcessRecord()
        {
            if (!Directory.Exists(this.Path))
            {
                Directory.CreateDirectory(this.Path);
            }

            if (Directory.GetFiles(this.Path).Any())
            {
                throw new ArgumentException("The specified directory must be empty");
            }

            SyncServer.ExportMetaverseConfiguration(this.Path);
        }
    }
}
