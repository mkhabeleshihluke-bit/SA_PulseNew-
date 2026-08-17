using System;

namespace SA_Pulse
{
    internal class PulseLevelChangedEventArgs : EventArgs
    {
        public string CommunityName { get; }
        public string OldLevel { get; }
        public string NewLevel { get; }
        public int PulseScore { get; }

        public PulseLevelChangedEventArgs(
            string communityName,
            string oldLevel,
            string newLevel,
            int pulseScore)
        {
            CommunityName = communityName;
            OldLevel = oldLevel;
            NewLevel = newLevel;
            PulseScore = pulseScore;
        }
    }
}