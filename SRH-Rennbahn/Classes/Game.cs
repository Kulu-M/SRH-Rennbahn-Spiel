using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRH_Rennbahn.Classes
{
    [Serializable]
    class Game
    {
        public int Bank { get; set; }

        public ObservableCollection<Player> Player { get; set; }
        public ObservableCollection<Racer> Racer { get; set; }
        public int MinBet { get; }
        public int MaxBet { get; }
        DateTime LastGame { get; set; }
    }
}
