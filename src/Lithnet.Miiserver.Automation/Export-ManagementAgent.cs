using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;
using System.IO;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsData.Export, "ManagementAgent")]
    public class ExportManagementAgent : MATargetCmdlet
    {
        [Parameter(Mandatory = true, Position = 2, ValueFromRemainingArguments = true), ValidateNotNullOrEmpty]
        public string File { get; set; }

        protected override void ProcessRecord()
        {
            if (System.IO.File.Exists(this.File))
            {
                if (!this.ShouldContinue("The specified file already exists. Overwrite?", "File already exists"))
                {
                    return;
                }
            }

            this.MAInstance.ExportManagementAgent(this.File);
        }
    }
}
