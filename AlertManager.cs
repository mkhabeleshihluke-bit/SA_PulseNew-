using System;

namespace SA_Pulse
{
    internal class AlertManager
    {
        // Handles the AnomalyDetected event
        public void HandleAnomalyDetected(
            object sender,
            AnomalyDetectedEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("          ANOMALY DETECTED");
            Console.WriteLine("========================================");

            Console.WriteLine($"Community:       {e.CommunityName}");
            Console.WriteLine($"Indicator:       {e.IndicatorName}");
            Console.WriteLine($"Previous Value:  {e.PreviousValue}");
            Console.WriteLine($"Current Value:   {e.CurrentValue}");
            Console.WriteLine($"Percentage:      {e.PercentageChange:F1}%");

            Console.WriteLine("========================================");
        }


        // Handles the PulseLevelChanged event
        public void HandlePulseLevelChanged(
            object sender,
            PulseLevelChangedEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("         PULSE LEVEL CHANGED");
            Console.WriteLine("========================================");

            Console.WriteLine($"Community:   {e.CommunityName}");
            Console.WriteLine($"Previous:    {e.OldLevel}");
            Console.WriteLine($"New Level:   {e.NewLevel}");
            Console.WriteLine($"Pulse Score: {e.PulseScore}");

            Console.WriteLine("========================================");
        }
    }
}