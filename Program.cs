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


            
            // EVENTS AND EXCEPTION HANDLING
            

            CrossSignalAnalyzer analyzer = new CrossSignalAnalyzer();

            AlertManager alertManager = new AlertManager();

            // Subscribe AlertManager to the two custom events
            analyzer.AnomalyDetected += alertManager.HandleAnomalyDetected;
            analyzer.PulseLevelChanged += alertManager.HandlePulseLevelChanged;

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


            // *****************************************************
            // PERSON 3: TESTING
            // *****************************************************

            try
            {
                Console.WriteLine();
                Console.WriteLine("========================================");
                Console.WriteLine("       SA PULSE - PERSON 3 TEST");
                Console.WriteLine("========================================");



                // Create additional indicators
                // *****************************************************

                Indicator electricity =
                    new Indicator("Electricity", 50, 25);

                Indicator transport =
                    new Indicator("Transport", 70, 40);


                Indicator[] indicators =
                {
        water,
        electricity,
        transport
    };



                // TEST 1 - CROSS-SIGNAL ANOMALY
                // *****************************************************

                Console.WriteLine();
                Console.WriteLine("TEST 1: Cross-Signal Detection");

                bool crossSignal =
                    analyzer.DetectCrossSignal(
                        community,
                        indicators);

                if (crossSignal)
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        "RESULT: Cross-signal anomaly detected.");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        "RESULT: No cross-signal anomaly detected.");
                }



                // TEST 2 - PULSE LEVEL CHANGE
                // *****************************************************

                Console.WriteLine();
                Console.WriteLine("TEST 2: Pulse Level Change");

                analyzer.CheckPulseLevelChange(
                    community,
                    75);



                // TEST 3 - INVALID PULSE SCORE
                // *****************************************************

                Console.WriteLine();
                Console.WriteLine("TEST 3: Invalid Pulse Score");

                analyzer.CheckPulseLevelChange(
                    community,
                    150);


            }
            catch (InvalidPulseScoreException ex)
            {
                Console.WriteLine();
                Console.WriteLine("CUSTOM EXCEPTION CAUGHT:");
                Console.WriteLine(ex.Message);
            }
            catch (CommunityNotFoundException ex)
            {
                Console.WriteLine();
                Console.WriteLine("COMMUNITY ERROR:");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("UNEXPECTED ERROR:");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("========================================");
                Console.WriteLine("       TESTING COMPLETED");
                Console.WriteLine("========================================");
            }
        }
    }
}
