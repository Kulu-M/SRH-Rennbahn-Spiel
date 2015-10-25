using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRH_Rennbahn
{
    public class RandomGenerator
    {
        private static Random rnd = new Random();
        public static int rndGen (int min, int max)
        {

            //Generate random number
            int i = rnd.Next(min, max);
            return i;
        }
    }
}
