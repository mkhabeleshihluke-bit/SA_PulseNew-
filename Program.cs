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

            Console.WriteLine("Indicator:");
            Console.WriteLine(water.IndicatorName);
            Console.WriteLine(water.CurrentNumberOfIncidents);
            Console.WriteLine(water.PreviousNumberOfIncidents);

            // --- MULTITHREADING & MONITORING ADDITION ---
            Console.WriteLine("=== Starting Multithreading System ===");

            // 1. Start background monitor
            SystemMonitor monitor = new SystemMonitor();
            monitor.Start();

            // 2. Start queue processor thread
            IncidentProcessor processor = new IncidentProcessor();
            processor.Start();

            // 3. Simple User Menu loop
            bool keepRunning = true;

            while (keepRunning)
            {
                Console.WriteLine("Menu: 1 = Add Water Incident | 2 = Exit");
                Console.Write("Choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    // Using your existing WaterIncident class from the project
                    WaterIncident incident = new WaterIncident();
                    processor.AddIncident(incident);
                }
                else if (choice == "2")
                {
                    keepRunning = false;
                }
            }

            // Stop background threads
            monitor.Stop();
            processor.Stop();

            Console.WriteLine("System shut down cleanly.");
        }
    }
}
