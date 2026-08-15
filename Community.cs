using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA_Pulse
{
    internal class Community
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
}
