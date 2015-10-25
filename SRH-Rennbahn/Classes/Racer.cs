using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SRH_Rennbahn
{
    [Serializable]
    public class Racer
    {
        public string name { get; set; }

        public int raceCount { get; set; }

        public int racesWon { get; set; }

        public int maxSpeed { get; set; }
        public int minSpeed { get; set; }
    }
}


        //public double skill { get; set; }
        //public string picPath { get; set; }
