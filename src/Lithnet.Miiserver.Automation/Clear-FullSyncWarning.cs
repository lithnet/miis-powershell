using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsCommon.Clear, "FullSyncWarning", DefaultParameterSetName = "none")]
    public class ClearFullSyncWarning : Cmdlet
    {
        private ManagementAgent ma;

        [Parameter(Mandatory = false, Position = 1, ValueFromPipeline = false, ParameterSetName = "MAName"), ValidateNotNullOrEmpty]
        public string MAName { get; set; }

        [Parameter(Mandatory = false, Position = 1, ValueFromPipeline = true, ParameterSetName = "MAObject"), ValidateNotNullOrEmpty]
        public ManagementAgent MA { get; set; }

        protected ManagementAgent MAInstance
        {
            get
            {
                if (this.MA != null)
                {
                    return this.MA;
                }
                else if (this.MAName != null)
                {
                    if (this.ma == null)
                    {
                        this.ma = MiisController.GetManagementAgent(this.MAName, false);
                    }

                    return this.ma;
                }
                else
                {
                    return null;
                }
            }
        }
        protected override void ProcessRecord()
        {
            ManagementAgent instance = this.MAInstance;

            if (instance == null)
            {
                foreach (ManagementAgent ma in MiisController.GetManagementAgents())
                {
                    ma.SuppressFullSyncWarning();
                }
            }
            else
            {
                instance.SuppressFullSyncWarning();
            }
        }
    }
}
