using System;
using System.Collections.Generic;
using System.Linq;

namespace SA_Pulse
{
    internal class Program
    {
        private static readonly List<Community> communities = new List<Community>();
        private static readonly List<Indicator> indicators = new List<Indicator>();
        private static readonly List<Incident> incidents = new List<Incident>();
        private static readonly List<string> alerts = new List<string>();
        private static readonly Random random = new Random();
        private static readonly SystemMonitor monitor = new SystemMonitor();
        private static bool monitoring;
        private static int nextIncidentId = 1;

        private static void Main(string[] args)
        {
            SeedData();
            bool running = true;
            while (running)
            {
                ShowMenu();
                int choice = ReadInt("Select an option: ", 0, 13);
                Console.WriteLine();
                try
                {
                    switch (choice)
                    {
                        case 1: ViewCommunities(); break;
                        case 2: AddCommunity(); break;
                        case 3: EditCommunity(); break;
                        case 4: DeleteCommunity(); break;
                        case 5: ViewCommunityDetails(); break;
                        case 6: ViewIndicators(); break;
                        case 7: ViewIncidents(); break;
                        case 8: SimulateIncident(); break;
                        case 9: AnalyseCommunity(); break;
                        case 10: ViewPulseScores(); break;
                        case 11: ViewAlerts(); break;
                        case 12: ToggleMonitoring(); break;
                        case 13: ViewStatistics(); break;
                        case 0: running = false; break;
                    }
                }
                catch (Exception ex) { Console.WriteLine("Error: " + ex.Message); }

                if (running)
                {
                    Console.WriteLine("\nPress Enter to return to the menu...");
                    Console.ReadLine();
                }
            }
            if (monitoring) monitor.Stop();
            Console.WriteLine("SA Pulse closed successfully.");
        }

        private static void ShowMenu()
        {
            if (!Console.IsOutputRedirected)
                Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("          SA PULSE MAIN MENU");
            Console.WriteLine("========================================");
            Console.WriteLine("1. View Communities");
            Console.WriteLine("2. Add Community");
            Console.WriteLine("3. Edit Community");
            Console.WriteLine("4. Delete Community");
            Console.WriteLine("5. View Community Details");
            Console.WriteLine("6. View Indicators");
            Console.WriteLine("7. View Incidents");
            Console.WriteLine("8. Simulate Incident");
            Console.WriteLine("9. Analyse Community");
            Console.WriteLine("10. View Pulse Scores");
            Console.WriteLine("11. View Alerts");
            Console.WriteLine("12. Start Monitoring" + (monitoring ? " (running)" : ""));
            Console.WriteLine("13. View Statistics");
            Console.WriteLine("0. Exit");
            Console.WriteLine("========================================\n");
        }

        private static void SeedData()
        {
            communities.Add(new Community("Ubuntu-01", 1, 50000, "7", "Moderate", 45));
            communities.Add(new Community("Mamelodi", 2, 120000, "8", "High", 68));
            communities.Add(new Community("Soweto", 3, 200000, "9", "Moderate", 54));
            indicators.Add(new Indicator("Water", 35, 20));
            indicators.Add(new Indicator("Electricity", 50, 40));
            indicators.Add(new Indicator("Transport", 24, 21));
            indicators.Add(new Indicator("Waste", 18, 15));
        }

        private static void ViewCommunities()
        {
            Heading("COMMUNITIES");
            if (!communities.Any()) { Console.WriteLine("No communities found."); return; }
            Console.WriteLine("{0,-5} {1,-22} {2,12} {3,8} {4,-10}", "ID", "Name", "Population", "Score", "Status");
            foreach (Community c in communities.OrderBy(c => c.CommunityID))
                Console.WriteLine("{0,-5} {1,-22} {2,12:N0} {3,8} {4,-10}", c.CommunityID, c.CommunityName, c.Population, c.PulseScore, c.PulseStatus);
        }

        private static void AddCommunity()
        {
            Heading("ADD COMMUNITY");
            string name = ReadText("Community name: ");
            int id = ReadInt("Community ID: ", 1, int.MaxValue);
            while (communities.Any(c => c.CommunityID == id))
            {
                Console.WriteLine("That community ID already exists.");
                id = ReadInt("Enter a different ID: ", 1, int.MaxValue);
            }
            int population = ReadInt("Population: ", 0, int.MaxValue);
            int pressure = ReadInt("Population pressure (1-10): ", 1, 10);
            string risk = ReadText("Risk level: ");
            int score = ReadInt("Pulse score (0-100): ", 0, 100);
            communities.Add(new Community(name, id, population, pressure.ToString(), risk, score));
            Console.WriteLine("Community added successfully.");
        }

        private static void EditCommunity()
        {
            Heading("EDIT COMMUNITY");
            Community c = SelectCommunity();
            if (c == null) return;
            c.CommunityName = ReadText("New community name: ");
            c.Population = ReadInt("New population: ", 0, int.MaxValue);
            c.PopulationPressure = ReadInt("New population pressure (1-10): ", 1, 10).ToString();
            c.RiskLevel = ReadText("New risk level: ");
            c.PulseScore = ReadInt("New pulse score (0-100): ", 0, 100);
            Console.WriteLine("Community updated successfully.");
        }

        private static void DeleteCommunity()
        {
            Heading("DELETE COMMUNITY");
            Community c = SelectCommunity();
            if (c == null) return;
            Console.Write("Delete {0}? (y/n): ", c.CommunityName);
            if (string.Equals(Console.ReadLine(), "y", StringComparison.OrdinalIgnoreCase))
            {
                communities.Remove(c);
                incidents.RemoveAll(i => i.CommunityID == c.CommunityID);
                Console.WriteLine("Community deleted successfully.");
            }
            else Console.WriteLine("Delete cancelled.");
        }

        private static void ViewCommunityDetails()
        {
            Heading("COMMUNITY DETAILS");
            Community c = SelectCommunity();
            if (c == null) return;
            Console.WriteLine("Name:                " + c.CommunityName);
            Console.WriteLine("ID:                  " + c.CommunityID);
            Console.WriteLine("Population:          " + c.Population.ToString("N0"));
            Console.WriteLine("Population pressure: " + c.PopulationPressure + "/10");
            Console.WriteLine("Risk level:          " + c.RiskLevel);
            Console.WriteLine("Pulse score:         " + c.PulseScore);
            Console.WriteLine("Pulse status:        " + c.PulseStatus);
            Console.WriteLine("Recorded incidents:  " + incidents.Count(i => i.CommunityID == c.CommunityID));
        }

        private static void ViewIndicators()
        {
            Heading("INDICATORS");
            Console.WriteLine("{0,-18} {1,10} {2,10} {3,10}", "Indicator", "Previous", "Current", "Change");
            foreach (Indicator i in indicators)
                Console.WriteLine("{0,-18} {1,10} {2,10} {3,10:+#;-#;0}", i.IndicatorName, i.PreviousNumberOfIncidents, i.CurrentNumberOfIncidents, i.GetChange());
        }

        private static void ViewIncidents()
        {
            Heading("INCIDENTS");
            if (!incidents.Any()) { Console.WriteLine("No incidents have been recorded."); return; }
            foreach (Incident i in incidents.OrderByDescending(i => i.Date))
            {
                Community c = communities.FirstOrDefault(c => c.CommunityID == i.CommunityID);
                Console.WriteLine("#{0} | {1:g} | {2} | {3} | Impact: {4}", i.IncidentID, i.Date, c == null ? "Unknown" : c.CommunityName, i.Description, i.Impact);
            }
        }

        private static void SimulateIncident()
        {
            Heading("SIMULATE INCIDENT");
            Community c = SelectCommunity();
            if (c == null) return;
            Console.WriteLine("1. Water  2. Power  3. Transport  4. Waste  5. Infrastructure  6. Emergency");
            int type = ReadInt("Incident type: ", 1, 6);
            int impact = random.Next(10, 41);
            int id = nextIncidentId++;
            DateTime now = DateTime.Now;
            string description;
            Incident incident;
            switch (type)
            {
                case 1: description = "Water supply interruption"; incident = new WaterIncident(id, description, c.CommunityID, now, impact); break;
                case 2: description = "Power outage"; incident = new PowerIncident(id, description, c.CommunityID, now, impact); break;
                case 3: description = "Transport disruption"; incident = new TransportIncident(id, description, c.CommunityID, now, impact); break;
                case 4: description = "Waste collection problem"; incident = new WasteIncident(id, description, c.CommunityID, now, impact); break;
                case 5: description = "Infrastructure problem"; incident = new InfrastructureIncident(id, description, c.CommunityID, now, impact); break;
                default: description = "Emergency incident"; incident = new EmergencyIncident(id, description, c.CommunityID, now, impact); break;
            }
            incidents.Add(incident);
            c.PulseScore = Math.Min(100, c.PulseScore + Math.Max(1, impact / 5));
            Indicator matching = indicators.FirstOrDefault(i => description.IndexOf(i.IndicatorName, StringComparison.OrdinalIgnoreCase) >= 0);
            if (matching != null) matching.CurrentNumberOfIncidents++;
            if (impact >= 25 || c.PulseScore > 70)
                alerts.Add(string.Format("[{0:g}] {1}: {2} (impact {3}, pulse {4})", now, c.CommunityName, description, impact, c.PulseScore));
            Console.WriteLine("Incident #{0} simulated. Impact: {1}. New pulse score: {2}.", id, impact, c.PulseScore);
        }

        private static void AnalyseCommunity()
        {
            Heading("COMMUNITY ANALYSIS");
            Community c = SelectCommunity();
            if (c == null) return;
            List<Incident> local = incidents.Where(i => i.CommunityID == c.CommunityID).ToList();
            double average = local.Select(i => (double)i.Impact).DefaultIfEmpty(0).Average();
            Console.WriteLine("Community:         " + c.CommunityName);
            Console.WriteLine("Current status:    " + c.PulseStatus);
            Console.WriteLine("Incident count:    " + local.Count);
            Console.WriteLine("Average impact:    " + average.ToString("F1"));
            Console.WriteLine("Rising indicators: " + indicators.Count(i => i.GetChange() > 0));
            Console.WriteLine("Assessment:        " + Assessment(c.PulseScore, local.Count));
        }

        private static void ViewPulseScores()
        {
            Heading("PULSE SCORES");
            foreach (Community c in communities.OrderByDescending(c => c.PulseScore))
            {
                int bars = c.PulseScore / 5;
                Console.WriteLine("{0,-22} {1,3} [{2}{3}] {4}", c.CommunityName, c.PulseScore, new string('#', bars), new string('.', 20 - bars), c.PulseStatus);
            }
        }

        private static void ViewAlerts()
        {
            Heading("ALERTS");
            if (!alerts.Any()) { Console.WriteLine("No alerts have been generated."); return; }
            foreach (string alert in alerts.AsEnumerable().Reverse()) Console.WriteLine(alert);
        }

        private static void ToggleMonitoring()
        {
            Heading("MONITORING");
            if (!monitoring) { monitor.Start(); monitoring = true; Console.WriteLine("Background monitoring started."); }
            else { monitor.Stop(); monitoring = false; Console.WriteLine("Background monitoring stopped."); }
        }

        private static void ViewStatistics()
        {
            Heading("STATISTICS");
            Console.WriteLine("Communities:          " + communities.Count);
            Console.WriteLine("Total population:     " + communities.Sum(c => (long)c.Population).ToString("N0"));
            Console.WriteLine("Average pulse score:  " + communities.Select(c => (double)c.PulseScore).DefaultIfEmpty(0).Average().ToString("F1"));
            Console.WriteLine("High/Critical areas:  " + communities.Count(c => c.PulseScore > 70));
            Console.WriteLine("Recorded incidents:   " + incidents.Count);
            Console.WriteLine("Active alerts:        " + alerts.Count);
            Console.WriteLine("Monitoring:           " + (monitoring ? "Running" : "Stopped"));
        }

        private static Community SelectCommunity()
        {
            if (!communities.Any()) { Console.WriteLine("No communities are available."); return null; }
            int id = ReadInt("Community ID: ", 1, int.MaxValue);
            Community c = communities.FirstOrDefault(x => x.CommunityID == id);
            if (c == null) Console.WriteLine("Community not found.");
            return c;
        }

        private static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                int value;
                if (int.TryParse(Console.ReadLine(), out value) && value >= min && value <= max) return value;
                Console.WriteLine("Enter a whole number between {0} and {1}.", min, max);
            }
        }

        private static string ReadText(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string value = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(value)) return value.Trim();
                Console.WriteLine("This value cannot be empty.");
            }
        }

        private static string Assessment(int score, int count)
        {
            if (score > 85) return "Immediate intervention required.";
            if (score > 70 || count >= 5) return "High pressure; monitor closely.";
            if (score > 50 || count >= 3) return "Elevated pressure.";
            return "Conditions are stable.";
        }

        private static void Heading(string title) { Console.WriteLine("===== " + title + " ====="); }
    }
}
