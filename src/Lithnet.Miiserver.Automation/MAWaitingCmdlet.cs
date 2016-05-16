using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;

namespace Lithnet.Miiserver.Automation
{
    public class MAWaitingCmdlet : MATargetCmdlet
    {
        private MAStatistics stats;

        private RunConfiguration runProfile;

        private FixedSizedQueue<ProgressItem> countHistory = new FixedSizedQueue<ProgressItem>(30);

        private int lastStepNumber;

        protected void UpdateProgress(bool pending, int runNumber)
        {
            if (runNumber < 0)
            {
                return;
            }

            RunDetails s = this.MAInstance.GetRunDetail(runNumber);

            if (s == null)
            {
                return;
            }

            if (this.runProfile == null)
            {
                this.runProfile = this.MAInstance.RunProfiles[s.RunProfileName];
                this.stats = this.MAInstance.Statistics;
            }

            StepDetails d = s.StepDetails.FirstOrDefault();

            if (d == null)
            {
                return;
            }

            string description;

            if (pending)
            {
                description = $"Waiting for {this.MAInstance.Name} to finish {s.RunProfileName}";
            }
            else
            {
                description = this.MAInstance.Name;
            }

            if (this.lastStepNumber == 0 || this.lastStepNumber != d.StepNumber)
            {
                this.lastStepNumber = d.StepNumber;
                this.countHistory = new FixedSizedQueue<ProgressItem>(30);
            }

            ProgressRecord r = new ProgressRecord(0, description, string.Format(
                $"Performing {this.runProfile.Name} step {d.StepNumber}/{this.runProfile.RunSteps.Count}: {d.StepDefinition.StepTypeDescription}"))
            {
                RecordType = ProgressRecordType.Processing
            };

            int processed;
            double total;
            int remaining = 0;
            if (this.GetCounts(d, out processed, out total))
            {
                int percentComplete = (int)((processed / total) * 100);
                r.PercentComplete = percentComplete > 100 ? 0 : percentComplete;
                remaining = (int)total - processed;
            }

            if (processed > 0)
            {
                double objpersec = 0;

                int changedCount;
                TimeSpan? timespan;

                this.GetCountDiff(processed, out changedCount, out timespan);

                if (changedCount > 0 && timespan.HasValue)
                {
                    objpersec = changedCount / timespan.Value.TotalSeconds;
                }

                if (remaining > 0 && objpersec > 0)
                {
                    int remainingSeconds = (int)(remaining / objpersec);
                    r.SecondsRemaining = remainingSeconds > 0 ? remainingSeconds : 0;
                }

                if (objpersec > 0 && !double.IsInfinity(objpersec))
                {
                    r.StatusDescription += $" ({objpersec:N2} obj/sec)";
                }
            }
            else
            {
                r.StatusDescription += " (waiting for MA to start)";
            }

            this.WriteProgress(r);
        }

        private bool GetCounts(StepDetails d, out int processed, out double total)
        {
            processed = 0;
            total = 0;

            switch (d.StepDefinition.Type)
            {
                case RunStepType.Export:
                    total = this.stats.PendingExportTotal;
                    processed = d.ExportCounters.ExportAdd + d.ExportCounters.ExportDelete + d.ExportCounters.ExportDeleteAdd + d.ExportCounters.ExportDeleteAdd + d.ExportCounters.ExportFailure + d.ExportCounters.ExportRename + d.ExportCounters.ExportUpdate;
                    break;

                case RunStepType.DeltaImport:
                case RunStepType.DeltaImportDeltaSynchronization:
                    total = 0;
                    processed = d.StagingCounters.StageAdd + d.StagingCounters.StageDelete + d.StagingCounters.StageDeleteAdd + d.StagingCounters.StageFailure + d.StagingCounters.StageNoChange + d.StagingCounters.StageRename + d.StagingCounters.StageUpdate;
                    return false;

                case RunStepType.FullSynchronization:
                    total = this.stats.Total;
                    processed = d.InboundFlowCounters.ConnectorFlow + d.InboundFlowCounters.ConnectorNoFlow + d.InboundFlowCounters.DisconnectedRemains + d.InboundFlowCounters.DisconnectorFiltered;
                    break;

                case RunStepType.DeltaSynchronization:
                    total = this.stats.PendingImportTotal;
                    processed = d.InboundFlowCounters.ConnectorFlow + d.InboundFlowCounters.ConnectorNoFlow + d.InboundFlowCounters.DisconnectedRemains + d.InboundFlowCounters.DisconnectorFiltered;
                    break;

                case RunStepType.FullImport:
                case RunStepType.FullImportFullSynchronization:
                case RunStepType.FullImportDeltaSynchronization:
                    total = this.stats.Total;
                    processed = d.StagingCounters.StageAdd + d.StagingCounters.StageDelete + d.StagingCounters.StageDeleteAdd + d.StagingCounters.StageFailure + d.StagingCounters.StageNoChange + d.StagingCounters.StageRename + d.StagingCounters.StageUpdate;
                    break;

                default:
                    return false;
            }

            return true;
        }

        private void GetCountDiff(int currentcount, out int intervalCount, out TimeSpan? timespan)
        {
            intervalCount = 0;
            timespan = null;

            if (currentcount == 0)
            {
                return;
            }
            
            ProgressItem current = new ProgressItem(DateTime.Now, currentcount);
            this.countHistory.Enqueue(current);

            ProgressItem first = this.countHistory.First();

            timespan = current.DateTime.Subtract(first.DateTime);
            intervalCount = current.Count - first.Count;
        }
    }
}
