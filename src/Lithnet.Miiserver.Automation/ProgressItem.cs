using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lithnet.Miiserver.Automation
{
    public class ProgressItem
    {
        public DateTime DateTime { get; set; }
        public int Count { get; set; }

        public ProgressItem(DateTime date, int count)
        {
            this.Count = count;
            this.DateTime = date;
        }
    }
}
