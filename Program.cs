using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA_Pulse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             Models/
            │
            ├── Community.cs
            ├── Indicator.cs
            ├── Incident.cs
            ├── WaterIncident.cs
            ├── PowerIncident.cs
            ├── TransportIncident.cs
            ├── WasteIncident.cs
            ├── EmergencyIncident.cs
            └── InfrastructureIncident.cs
             */
            Community community = new Community("Ubuntu-01", 1, 50000, "High", "Moderate", 45);

            Console.WriteLine(community.CommunityName);
            Console.WriteLine(community.CommunityID);
            Console.WriteLine(community.Population);
            Console.WriteLine(community.PopulationPressure);
            Console.WriteLine(community.RiskLevel);
            Console.WriteLine(community.PulseScore);
            Console.WriteLine(community.PulseStatus);

            Indicator water = new Indicator("Water", 35, 20);

            Console.WriteLine("\nIndicator:");
            Console.WriteLine(water.IndicatorName);
            Console.WriteLine(water.CurrentNumberOfIncidents);
            Console.WriteLine(water.PreviousNumberOfIncidents);
        }
        class Community
        {
            // fields that our communities will have
            private string communityName;
            private int communityID;
            private int population;
            private string populationPressure;
            private string riskLevel;
            private int pulseScore;


            public string CommunityName //property for community name with validation
            {
                get { return communityName; }
                set
                {
                    if (value == " ") //im going to use basic validation becuase that is Person 3's responsibility
                    {
                        Console.WriteLine("Community name cannot be empty");
                    }
                    else
                    {
                        communityName = value;
                    }
                }
            }
            public int CommunityID //property for community ID with validation
            {
                get { return communityID; }
                set
                {
                    if (value < 0)
                    {
                        Console.WriteLine("Community ID cannot be negative");
                    }
                    else
                    {
                        communityID = value;
                    }
                }
            }
            public int Population //property for population with validation
            {
                get { return population; }
                set
                {
                    if (value < 0)
                    {
                        Console.WriteLine("Population cannot be negative");
                    }
                    else
                    {
                        population = value;
                    }
                }
            }
            public string PopulationPressure //property for population pressure with validation
            {
                get { return populationPressure; }
                set
                {
                    if (value == " ")
                    {
                        Console.WriteLine("Population pressure cannot be empty");
                    }
                    else
                    {
                        populationPressure = value;
                    }
                }
            }
            public string RiskLevel //property for risk level with validation
            {
                get { return riskLevel; }
                set
                {
                    if (value == " ")
                    {
                        Console.WriteLine("Risk level cannot be empty");
                    }
                    else
                    {
                        riskLevel = value;
                    }
                }
            }
            public int PulseScore //property for pulse score with validation
            {
                get { return pulseScore; }
                set
                {
                    if (value < 0 || value > 100)
                    {
                        Console.WriteLine("Pulse score must be between 0 and 100");
                    }
                    else
                    {
                        pulseScore = value;
                    }
                }
            }
            public string PulseStatus
            {
                get
                {
                    if (PulseScore <= 30)
                        return "Normal";
                    else if (PulseScore <= 50)
                        return "Watch";
                    else if (PulseScore <= 70)
                        return "Elevated";
                    else if (PulseScore <= 85)
                        return "High";
                    else
                        return "Critical";
                }
            }
            public Community(string communityName, int communityID, int population,
                        string populationPressure, string riskLevel, int pulseScore)
            {
                CommunityName = communityName;
                CommunityID = communityID;
                Population = population;
                PopulationPressure = populationPressure;
                RiskLevel = riskLevel; //This is the starting condition of the community 
                PulseScore = pulseScore; //this is the current/ updated conditions as montioring and etc continue 
            }

        }
        class Indicator
        {
            private string indicatorName;
            private int currentNumberOfIncidents;
            private int previousNumberOfIncidents;
            private int change;
            public string IndicatorName
            {
                get { return indicatorName; }
                set
                {
                    if (value == " ")
                    {
                        Console.WriteLine("Indicator name cannot be empty");
                    }
                    else
                    {
                        indicatorName = value;
                    }
                }
            }
            public int CurrentNumberOfIncidents
            {
                get { return currentNumberOfIncidents; }
                set
                {
                    if (value < 0)
                    {
                        Console.WriteLine("Current number of incidents cannot be negative");
                    }
                    else
                    {
                        currentNumberOfIncidents = value;
                    }
                }
            }
            public int PreviousNumberOfIncidents
            {
                get { return previousNumberOfIncidents; }
                set
                {
                    if (value < 0)
                    {
                        Console.WriteLine("Previous number of incidents cannot be negative");
                    }
                    else
                    {
                        previousNumberOfIncidents = value;
                    }
                }
            }
            public int GetChange()
            {
                return CurrentNumberOfIncidents - PreviousNumberOfIncidents;
            }

            public Indicator(string indicatorName, int currentNumberOfIncidents, int previousNumberOfIncidents)
            {
                IndicatorName = indicatorName;
                CurrentNumberOfIncidents = currentNumberOfIncidents;
                PreviousNumberOfIncidents = previousNumberOfIncidents;
                change= GetChange();
            }
        }
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
        class WaterIncident : Incident
        {

            public WaterIncident(int incidentID, string description, int communityID, DateTime date, int impact)
            {
                IncidentID = incidentID;
                Description = description;
                CommunityID = communityID;
                Date = date;
                Impact = impact;
            }
            public override string GetDescription()
            {
                return "Water supply interruption";
            }

        }

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

        class TransportIncident : Incident
        {
            public TransportIncident(int incidentID, string description, int communityID, DateTime date, int impact)
            {
                IncidentID = incidentID;
                Description = description;
                CommunityID = communityID;
                Date = date;
                Impact = impact;
            }
            public override string GetDescription()
            {
                return "Transport disruption";
            }
        }

        class WasteIncident : Incident
        { 
              public WasteIncident(int incidentID, string description, int communityID, DateTime date, int impact)
                {
                 IncidentID = incidentID;
                 Description = description;
                 CommunityID = communityID;
                Date = date;
                Impact = impact;
            }
            public override string GetDescription()
            {
                return "Waste collection problem";
            }
        }

        class EmergencyIncident : Incident
        {
            public EmergencyIncident(int incidentID, string description, int communityID, DateTime date, int impact)
            {
                IncidentID = incidentID;
                Description = description;
                CommunityID = communityID;
                Date = date;
                Impact = impact;
            }
            public override string GetDescription()
            {
                return "Emergency incident";
            }
        }

        class InfrastructureIncident : Incident
        { 
            public InfrastructureIncident(int incidentID, string description, int communityID, DateTime date, int impact)
            {
                IncidentID = incidentID;
                Description = description;
                CommunityID = communityID;
                Date = date;
                Impact = impact;
            }
            public override string GetDescription()
            {
                return "Infrastructure problem";
            }
        }
        class RandomDataGenerator
        {
            private Random random = new Random();

            public int GenerateIncidentCount(int minimum, int maximum)
            {
                return random.Next(minimum, maximum + 1);
            }
        }
    }
}
