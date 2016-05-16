using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsData.Save, "RunHistory", DefaultParameterSetName = "ByDate")]
    public class SaveRunHistory : Cmdlet
    {
        [Parameter(Mandatory = false, ParameterSetName = "ByDate", Position = 1)]
        public DateTime? BeforeDate { get; set; }

        [Parameter(Mandatory = true)]
        public string File { get; set; }

        protected override void ProcessRecord()
        {
            if (this.BeforeDate.HasValue)
            {
                SyncServer.SaveRunHistory(this.BeforeDate.Value.ToUniversalTime(), this.File);
            }
            else
            {
                SyncServer.SaveRunHistory(this.File);
            }
        }
    }
}
