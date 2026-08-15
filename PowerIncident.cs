using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA_Pulse
{
    class PowerIncident : Incident
    {
        public PowerIncident(int incidentID, string description, int communityID, DateTime date, int impact)
        {
            IncidentID = incidentID;
            Description = description;
            CommunityID = communityID;
            Date = date;
            Impact = impact;
        }
        public override string GetDescription()
        {
            return "Power outage";
        }
    }
}
