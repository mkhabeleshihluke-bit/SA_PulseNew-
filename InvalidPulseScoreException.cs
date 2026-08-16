using System;

namespace SA_Pulse
{
    internal class InvalidPulseScoreException : Exception
    {
        public InvalidPulseScoreException()
            : base("Invalid pulse score.")
        {
        }

        public InvalidPulseScoreException(string message)
            : base(message)
        {
        }

        public InvalidPulseScoreException(
            string message,
            Exception innerException)
            : base(message, innerException)
        {
        }
    }
}