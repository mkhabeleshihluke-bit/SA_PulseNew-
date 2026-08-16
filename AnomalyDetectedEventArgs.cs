using System;

namespace SA_Pulse
{
    internal class AnomalyDetectedEventArgs : EventArgs
    {
        public string CommunityName { get; }
        public string IndicatorName { get; }
        public int PreviousValue { get; }
        public int CurrentValue { get; }
        public double PercentageChange { get; }

        public AnomalyDetectedEventArgs(
            string communityName,
            string indicatorName,
            int previousValue,
            int currentValue)
        {
            CommunityName = communityName;
            IndicatorName = indicatorName;
            PreviousValue = previousValue;
            CurrentValue = currentValue;

            if (previousValue == 0)
            {
                PercentageChange = currentValue > 0 ? 100 : 0;
            }
            else
            {
                PercentageChange =
                    ((double)(currentValue - previousValue)
                    / previousValue) * 100;
            }
        }
    }
}
