using System;

namespace SA_Pulse
{
    internal class CrossSignalAnalyzer
    {
       
        // DELEGATES
        

        public delegate void AnomalyDetectedHandler(
            object sender,
            AnomalyDetectedEventArgs e);

        public delegate void PulseLevelChangedHandler(
            object sender,
            PulseLevelChangedEventArgs e);


        
        // EVENTS
       

        public event AnomalyDetectedHandler AnomalyDetected;

        public event PulseLevelChangedHandler PulseLevelChanged;


        
        // SETTINGS
        

        // An indicator must increase by at least 30%
        // to be considered a significant change.
        private const double AnomalyThreshold = 30.0;

        // At least 2 indicators must show a significant
        // increase for a cross-signal anomaly.
        private const int RequiredSignals = 2;


        
        // CROSS-SIGNAL DETECTION
        

        public bool DetectCrossSignal(
            Community community,
            Indicator[] indicators)
        {
            ValidateCommunity(community);

            if (indicators == null || indicators.Length == 0)
            {
                throw new ArgumentException(
                    "At least one indicator is required.");
            }

            int significantSignals = 0;

            foreach (Indicator indicator in indicators)
            {
                if (indicator == null)
                {
                    continue;
                }

                double percentageChange =
                    CalculatePercentageChange(
                        indicator.PreviousNumberOfIncidents,
                        indicator.CurrentNumberOfIncidents);

                if (percentageChange >= AnomalyThreshold)
                {
                    significantSignals++;

                    OnAnomalyDetected(
                        community,
                        indicator);
                }
            }

            return significantSignals >= RequiredSignals;
        }


        
        // CALCULATE PERCENTAGE CHANGE
        

        private double CalculatePercentageChange(
            int previousValue,
            int currentValue)
        {
            if (previousValue == 0)
            {
                return currentValue > 0 ? 100 : 0;
            }

            return ((double)(currentValue - previousValue)
                    / previousValue) * 100;
        }


        
        // RAISE ANOMALY EVENT
        

        protected virtual void OnAnomalyDetected(
            Community community,
            Indicator indicator)
        {
            if (AnomalyDetected != null)
            {
                AnomalyDetected(
                    this,
                    new AnomalyDetectedEventArgs(
                        community.CommunityName,
                        indicator.IndicatorName,
                        indicator.PreviousNumberOfIncidents,
                        indicator.CurrentNumberOfIncidents));
            }
        }


        
        // CHECK PULSE LEVEL CHANGE
        

        public void CheckPulseLevelChange(
            Community community,
            int newPulseScore)
        {
            ValidateCommunity(community);

            if (newPulseScore < 0 || newPulseScore > 100)
            {
                throw new InvalidPulseScoreException(
                    "Pulse score must be between 0 and 100.");
            }

            string oldLevel = community.PulseStatus;

            // Update the existing Community object.
            community.PulseScore = newPulseScore;

            string newLevel = community.PulseStatus;

            // Only raise the event if the actual level changed.
            if (!oldLevel.Equals(
                newLevel,
                StringComparison.OrdinalIgnoreCase))
            {
                OnPulseLevelChanged(
                    community,
                    oldLevel,
                    newLevel,
                    newPulseScore);
            }
        }


        
        // RAISE PULSE LEVEL EVENT
        

        protected virtual void OnPulseLevelChanged(
            Community community,
            string oldLevel,
            string newLevel,
            int pulseScore)
        {
            if (PulseLevelChanged != null)
            {
                PulseLevelChanged(
                    this,
                    new PulseLevelChangedEventArgs(
                        community.CommunityName,
                        oldLevel,
                        newLevel,
                        pulseScore));
            }
        }


        
        // COMMUNITY VALIDATION
        

        private void ValidateCommunity(
            Community community)
        {
            if (community == null)
            {
                throw new CommunityNotFoundException(
                    "The selected community could not be found.");
            }
        }
    }
}