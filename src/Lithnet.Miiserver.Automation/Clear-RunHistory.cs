using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsCommon.Clear, "RunHistory", DefaultParameterSetName = "ByDate")]
    public class ClearRunHistory : Cmdlet
    {
        [Parameter(Mandatory = false, ParameterSetName ="ByDate", Position = 1)]
        public DateTime? BeforeDate { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = "ByDays", Position = 1)]
        public int? DaysToKeep { get; set; }

        [Parameter]
        public string File { get; set; }

        protected override void ProcessRecord()
        {
            if (this.BeforeDate.HasValue)
            {
                if (this.File == null)
                {
                    SyncServer.ClearRunHistory(this.BeforeDate.Value.ToUniversalTime());
                }
                else
                {
                    SyncServer.ClearRunHistory(this.BeforeDate.Value.ToUniversalTime(), this.File);
                }
            }
            if (this.DaysToKeep.HasValue)
            {
                if (this.File == null)
                {
                    SyncServer.ClearRunHistory(DateTime.UtcNow.AddDays(-this.DaysToKeep.Value));
                }
                else
                {
                    SyncServer.ClearRunHistory(DateTime.UtcNow.AddDays(-this.DaysToKeep.Value), this.File);

                }
            }
            else
            {
                if (this.File == null)
                {
                    SyncServer.ClearRunHistory();
                }
                else
                {
                    SyncServer.ClearRunHistory(this.File);
                }
            }
        }
    }
}
