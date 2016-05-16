using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;
using System.Threading;
using System.Threading.Tasks;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsLifecycle.Start, "ManagementAgent")]
    public class StartManagementAgent : MAWaitingCmdlet
    {
        [Parameter(Mandatory = true, Position = 2)]
        public string RunProfileName { get; set; }

        [Parameter(Mandatory = false, Position = 3)]
        public SwitchParameter Async { get; set; }

        [Parameter(Mandatory = false, Position = 4)]
        public SwitchParameter ResumeLastRun { get; set; }

        [Parameter(Mandatory = false, Position = 5)]
        public SwitchParameter NoProgress { get; set; }

        protected override void ProcessRecord()
        {
            RunDetails last = this.MAInstance.GetLastRun();
            int lastExecutionNumber = last?.RunNumber ?? -1;

            while (!this.MAInstance.IsIdle())
            {
                if (!this.NoProgress.IsPresent)
                {
                    this.UpdateProgress(true, lastExecutionNumber);
                }

                Thread.Sleep(5000);
            }

            if (this.Async)
            {
                this.MAInstance.ExecuteRunProfileAsync(this.RunProfileName, this.ResumeLastRun);
            }
            else
            {
                Task<string> t = this.MAInstance.ExecuteRunProfileAsync(this.RunProfileName, this.ResumeLastRun);
                int currentExecutionNumber = -1;

                do
                {
                    if (t.IsCompleted)
                    {
                        break;
                    }

                    RunDetails d = this.MAInstance.GetLastRun();
                    if (d != null)
                    {
                        currentExecutionNumber = d.RunNumber;
                    }

                    Thread.Sleep(500);
                }
                while (currentExecutionNumber == -1 || currentExecutionNumber == lastExecutionNumber);

                while (!t.IsCompleted)
                {
                    if (!this.NoProgress.IsPresent)
                    {
                        this.UpdateProgress(false, currentExecutionNumber);
                    }

                    Thread.Sleep(2000);
                }

                ProgressRecord r = new ProgressRecord(0, this.MAInstance.Name, $"Finished: {this.RunProfileName}")
                {
                    RecordType = ProgressRecordType.Completed,
                    PercentComplete = 100
                };
                this.WriteProgress(r);

                if (t.IsFaulted)
                {
                    throw t.Exception?.InnerExceptions.First() ?? new MAExecutionException();
                }

                if (t.Result != "success")
                {
                    this.WriteWarning($"Management agent returned {t.Result}");
                }
            }
        }
    }
}