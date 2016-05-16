using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsLifecycle.Wait, "ManagementAgent")]
    public class WaitManagementAgent : MAWaitingCmdlet
    {
        protected override void ProcessRecord()
        {
            RunDetails last = this.MAInstance.GetLastRun();

            while (!this.MAInstance.IsIdle())
            {
                this.UpdateProgress(true, last.RunNumber);
                System.Threading.Thread.Sleep(5000);
            }

            ProgressRecord r = new ProgressRecord(0, this.MAInstance.Name, string.Format("Finished: {0}", last.RunProfileName));
            r.RecordType = ProgressRecordType.Completed;
            r.PercentComplete = 100;
            this.WriteProgress(r);
        }
    }
}