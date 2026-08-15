using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA_Pulse
{
    internal class Indicator
    {
        private string indicatorName;
        private int currentNumberOfIncidents;
        private int previousNumberOfIncidents;
        private int change;
        public string IndicatorName
        {
            get { return indicatorName; }
            set
            {
                if (value == " ")
                {
                    Console.WriteLine("Indicator name cannot be empty");
                }
                else
                {
                    indicatorName = value;
                }
            }
        }
        public int CurrentNumberOfIncidents
        {
            get { return currentNumberOfIncidents; }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Current number of incidents cannot be negative");
                }
                else
                {
                    currentNumberOfIncidents = value;
                }
            }
        }
        public int PreviousNumberOfIncidents
        {
            get { return previousNumberOfIncidents; }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Previous number of incidents cannot be negative");
                }
                else
                {
                    previousNumberOfIncidents = value;
                }
            }
        }
        public int GetChange()
        {
            return CurrentNumberOfIncidents - PreviousNumberOfIncidents;
        }

        public Indicator(string indicatorName, int currentNumberOfIncidents, int previousNumberOfIncidents)
        {
            IndicatorName = indicatorName;
            CurrentNumberOfIncidents = currentNumberOfIncidents;
            PreviousNumberOfIncidents = previousNumberOfIncidents;
            change = GetChange();
        }
    }
}

