using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using Lithnet.Miiserver.Client;

namespace Lithnet.Miiserver.Automation
{
    [Cmdlet(VerbsCommon.New, "MVQuery")]
    public class NewMVQuery : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 1)]
        public string Attribute { get; set; }

        [Parameter(Mandatory = false, Position = 2)]
        public MVSearchFilterOperator Operator { get; set; }

        [Parameter(Mandatory = false, Position = 3)]
        public object Value { get; set; }

        [Parameter(Mandatory = false, Position = 4, ValueFromPipeline = true)]
        public MVAttributeQuery PipelineObject { get; set; }

        private List<MVAttributeQuery> collectedObjects = new List<MVAttributeQuery>();

        protected override void ProcessRecord()
        {
            if (this.PipelineObject != null)
            {
                this.collectedObjects.Add(this.PipelineObject);
            }
        }

        protected override void EndProcessing()
        {
            MVAttributeQuery q = new MVAttributeQuery();
            q.Operator = this.Operator;
            q.Value = this.Value;

            if (this.Value == null)
            {
                if (!(this.Operator == MVSearchFilterOperator.IsNotPresent || this.Operator == MVSearchFilterOperator.IsPresent))
                {
                    throw new ArgumentNullException("Value", "Value must be specified unless operator is IsPresent or IsNotPresent");
                }
            }

            DsmlAttribute attribute;
            if (!MiisController.Schema.Attributes.TryGetValue(this.Attribute, out attribute))
            {
                throw new ItemNotFoundException(string.Format("Attribute {0} does not exist", attribute));
            }

            q.Attribute = attribute;

            if (this.collectedObjects.Count > 0)
            {
                this.WriteObject(this.collectedObjects, true);
            }

            this.WriteObject(q);
        }
    }
}