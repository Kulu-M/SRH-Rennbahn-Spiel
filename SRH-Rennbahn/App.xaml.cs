using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace SRH_Rennbahn
{

    public partial class App : Application
    {
        public static Game _gameDaten;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _gameDaten = SaveLoad.readBinary<Game>("GameSaveFile.dat");
            if (_gameDaten == null)
            { 
                _gameDaten = new Game();
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            SaveLoad.writeBinary("GameSaveFile.dat", _gameDaten);
        }
    }
}
