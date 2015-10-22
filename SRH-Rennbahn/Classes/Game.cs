using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SRH_Rennbahn
{
    [Serializable]
    public class Game
    {
        //BANK ACCOUNT 
        public int bankAccount { get; set; }

        //STARTMONEY
        public int startingMoney { get; set; }

        //MIN MAX VALUES OF BETS
        public int minBet { get; set; }
        public int maxBet { get; set; }

        //GAMECOUNT
        public int gamesCount { get; set; }

        //LAST GAME
        public DateTime lastGame { get; set; }

        //LIST
        public List<Player> playersList = new List<Player>();
        public List<Racer> racersList = new List<Racer>();

        //KONSTRUKTOR
        public Game()
        {
            startingMoney = 4000;
            maxBet = 400;

            playersList = new List<Player>();
            initPlayers();

            racersList = new List<Racer>();
            racersList = initRacers();

            lastGame = DateTime.Now;

            bankAccount = 0; //Bank verliert atm kein Geld daher egal
            gamesCount = 1;
        }

        //Liste Player befüllen (mit NPCs)
        private void initPlayers()
        {
            playersList = new List<Player>()
            {
                //Kurzer Weg zum befüllen
                new Player {name = "Bit", wallet = startingMoney, npc = true},
                new Player {name = "Byte", wallet = startingMoney, npc = true},
                new Player {name = "Hashtag", wallet = startingMoney, npc = true}
            };
        }

        //Liste Racer befüllen
        private List<Racer> initRacers()
        {
            //Langer Weg zum befüllen
            var r1 = new Racer { name = "Alien", maxSpeed = 9, minSpeed = 3};
            var r2 = new Racer { name = "Ninja", maxSpeed = 14, minSpeed = 6 };
            var r3 = new Racer { name = "Pikachu", maxSpeed = 11, minSpeed = 4 };
            var r4 = new Racer { name = "Sonic", maxSpeed = 30, minSpeed = 9 };
            var r5 = new Racer { name = "Zombie", maxSpeed = 5, minSpeed = 2 };

            //Erstellte Elemente der Liste hinzufügen
            racersList.Add(r1);
            racersList.Add(r2);
            racersList.Add(r3);
            racersList.Add(r4);
            racersList.Add(r5);

            return racersList;
        }



    }

    

}


        //public ObservableCollection<Player> Player { get; set; }
        //public ObservableCollection<Racer> Racer { get; set; }
