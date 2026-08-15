using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA_Pulse
{
    class RandomDataGenerator
    {
        private Random random = new Random();

        public int GenerateIncidentCount(int minimum, int maximum)
        {
            return random.Next(minimum, maximum + 1);
        }
    }
}
