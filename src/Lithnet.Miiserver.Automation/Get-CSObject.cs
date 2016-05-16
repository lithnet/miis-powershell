using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsCommon.Get, "CSObject")]
    public class GetCSObject : MATargetCmdlet
    {
        [Parameter(ValueFromPipeline = false, ParameterSetName = "Guid", Mandatory = true, Position = 1)]
        public Guid ID { get; set; }

        [Parameter(ValueFromPipeline = false, ParameterSetName = "DN", Mandatory = true, Position = 1)]
        public string DN { get; set; }

        [Parameter(ValueFromPipeline = false, ParameterSetName = "RDN", Mandatory = true, Position = 1)]
        public string Rdn { get; set; }

        [Parameter(ValueFromPipeline = false, ParameterSetName = "DN", Mandatory = false, Position = 2)]
        public SwitchParameter IncludeSubTree { get; set; }
        
        protected override void ProcessRecord()
        {
            if (this.ID != Guid.Empty)
            {
                this.WriteObject(this.MAInstance.GetCSObject(this.ID));
                return;
            }
            else if (!string.IsNullOrWhiteSpace(this.DN))
            {
                if (this.IncludeSubTree.IsPresent)
                {
                    this.WriteObject(this.MAInstance.GetCSObjects(this.DN, true), true);
                }
                else
                {
                    this.WriteObject(this.MAInstance.GetCSObject(this.DN));
                }
                return;
            }
            else if (!string.IsNullOrWhiteSpace(this.Rdn))
            {
                this.WriteObject(this.MAInstance.GetCSObjects(this.Rdn), true);
                return;
            }
        }
    }
}