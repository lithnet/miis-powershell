using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lithnet.Miiserver.Client;

namespace Lithnet.Miiserver.Automation
{
    internal static class MiisController
    {
        private static Dictionary<string, ManagementAgent> maCache = new Dictionary<string, ManagementAgent>(StringComparer.OrdinalIgnoreCase);

        private static DsmlSchema schema;

        internal static IEnumerable<ManagementAgent> GetManagementAgents()
        {
            maCache.Clear();

            foreach (ManagementAgent ma in ManagementAgent.GetManagementAgents())
            {
                maCache.Add(ma.Name, ma);
            }

            return maCache.Values;
        }

        internal static ManagementAgent GetManagementAgent(string name, bool reload)
        {
            if (reload || !maCache.ContainsKey(name))
            {
                ManagementAgent ma =  ManagementAgent.GetManagementAgent(name);

                if (!maCache.ContainsKey(name))
                {
                    maCache.Add(ma.Name, ma);
                }
                else
                {
                    maCache[ma.Name] = ma;
                }
            }

            return maCache[name];
        }

        internal static DsmlSchema Schema
        {
            get
            {
                if (MiisController.schema == null)
                {
                    MiisController.schema = SyncServer.GetMVSchema();
                }

                return MiisController.schema;
            }
        }

        internal static void ClearSchemaCache()
        {
            MiisController.schema = null;
        }
    }
}
