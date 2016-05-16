using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsCommon.Get, "MVSchema")]
    public class GetMVSchema : Cmdlet
    {
        [Parameter(Mandatory = false, Position = 1)]
        public string ObjectType { get; set; }

        protected override void ProcessRecord()
        {
            DsmlSchema schema = SyncServer.GetMVSchema();

            if (string.IsNullOrWhiteSpace(this.ObjectType))
            {
                this.WriteObject(schema);
            }
            else
            {
                if (schema.ObjectClasses.ContainsKey(this.ObjectType))
                {
                    this.WriteObject(schema.ObjectClasses[this.ObjectType]);
                }
            }
        }
    }
}