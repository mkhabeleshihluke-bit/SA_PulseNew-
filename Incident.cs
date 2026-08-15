using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA_Pulse
{
    abstract class Incident // so i made this class abstract to make sure that no one can create an object of this class, but only create instances of the the child classes
    {
        private int incidentID;
        private string description;
        private int communityID;
        private DateTime date;
        private int impact;

        public abstract string GetDescription();

        public int IncidentID
        {
            get { return incidentID; }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Incident ID cannot be negative");
                }
                else
                {
                    incidentID = value;
                }
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                if (value == " ")// member 3 will handle the validation of the description, but I will add a basic validation here
                {
                    Console.WriteLine("Description cannot be empty");
                }
                else
                {
                    description = value;
                }
            }

        }

        public int CommunityID
        {
            get { return communityID; }
            set
            {
                if (value < 0) // for this validation member 2 will decide what collection they will use so that either member 2 or 3 can make sure that the community ID is valid and matches the community ID in the collection,
                {
                    Console.WriteLine("Community ID cannot be negative");
                }
                else
                {
                    communityID = value;
                }
            }
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                if (value > DateTime.Now)
                {
                    Console.WriteLine("Date cannot be in the future");
                }
                else
                {
                    date = value;
                }
            }

        }
        public int Impact
        {
            get { return impact; }
            set
            {
                if (value < 0 || value > 100)
                {
                    Console.WriteLine("Impact must be between 0 and 100");// again basic validation, member 3 will handle the validation of the impact
                }
                else
                {
                    impact = value;
                }
            }
        }
    }
}
