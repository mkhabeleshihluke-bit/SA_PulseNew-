using System;

namespace SA_Pulse
{
    internal class CommunityNotFoundException : Exception
    {
        public CommunityNotFoundException()
            : base("Community could not be found.")
        {
        }

        public CommunityNotFoundException(string message)
            : base(message)
        {
        }

        public CommunityNotFoundException(
            string message,
            Exception innerException)
            : base(message, innerException)
        {
        }
    }
}