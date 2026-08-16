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
    }
}
