using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;
using System.IO;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsCommon.Join, "CSObject")]
    public class JoinCSObject : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = false, ParameterSetName ="Manual"), ValidateNotNullOrEmpty]
        public string MVObjectType { get; set; }

        [Parameter(Mandatory = true, Position = 2, ValueFromPipeline = false, ParameterSetName = "Manual"), ValidateNotNullOrEmpty]
        public Guid MVObjectID { get; set; }

        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = false, ParameterSetName = "Pipeline"), ValidateNotNullOrEmpty]
        public MVObject MVObject { get; set; }

        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, Position = 3, ValueFromPipeline = false, ParameterSetName = "Manual")]
        [Parameter(Mandatory = true, Position = 2, ValueFromPipeline = true, ParameterSetName = "Pipeline")]
        public CSObject CSObject { get; set; }

        protected override void ProcessRecord()
        {
            if (this.MVObject != null)
            {
                this.CSObject.Join(this.MVObject);
            }
            else
            {
                this.CSObject.Join(this.MVObjectType, this.MVObjectID);
            }
        }
    }
}
