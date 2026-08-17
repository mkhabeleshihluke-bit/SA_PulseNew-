using System;

namespace SA_Pulse
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SA_Pulse Starting ===");

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
