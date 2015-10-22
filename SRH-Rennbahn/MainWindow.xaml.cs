using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace SRH_Rennbahn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        double finishLine;
        DispatcherTimer timer = new DispatcherTimer();
        public List<Racer> startingRacers = new List<Racer>();


        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.01);
            timer.Tick += Timer_Tick;
        }


        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            finishLine = (double)brd_finishline.GetValue(Canvas.LeftProperty);


            //Aus einem Pool von Racern nur einige aussuchen, die starten
            startingRacers.Add(App._gameDaten.racersList[1]);
            startingRacers.Add(App._gameDaten.racersList[2]);
            for (int i = 0; i < 2; i++)
            {
                Image img = (Image) can_racetrack.FindName("image_" + i);
                var path = "Pics/" + startingRacers[i].name + "/0.png";
                BitmapImage bmi = new BitmapImage(new Uri(path, UriKind.Relative));
                img.Source = bmi;
            }



            //geht nicht?
            //grid.DataContext = App._gameDaten;
            //---

            //KOMBO BOXEN FÜLLEN
            cB_player0.ItemsSource = App._gameDaten.playersList;
            cB_player1.ItemsSource = App._gameDaten.playersList;
            cB_player2.ItemsSource = App._gameDaten.playersList;
            cB_player3.ItemsSource = App._gameDaten.playersList;
            cB_racer0.ItemsSource = App._gameDaten.racersList;
            cB_racer1.ItemsSource = App._gameDaten.racersList;
            cB_racer2.ItemsSource = App._gameDaten.racersList;
            cB_racer3.ItemsSource = App._gameDaten.racersList;
            //---
        }


        bool wechsel = false;
        private void Timer_Tick(object sender, EventArgs e)
        {
            var posX = (double)image_1.GetValue(Canvas.LeftProperty) + 10;
            image_1.SetValue(Canvas.LeftProperty, posX);

            //END OF RACE
            if (posX >= finishLine)
            { 
                timer.Stop();
                b_reset.IsEnabled = true;
                BitmapImage bild = new BitmapImage(new Uri("Pics/alien3.png", UriKind.Relative));
                image_1.Source = bild;
            }

            //CHANGE IMAGES
            if (wechsel)
                imageChanger("Pics/sonic2.png", image_1.Source);
            else
                imageChanger("Pics/sonic1.png", image_1.Source);

            wechsel = !wechsel;
        }


        private void imageChanger(string imgname, object racername)
        {
            BitmapImage bild = new BitmapImage(new Uri(imgname, UriKind.Relative));

            image_0.Source = bild;
            image_1.Source = bild;
            image_2.Source = bild;
            image_3.Source = bild;
            image_4.Source = bild;
        }


        private void b_startmethod(object sender, RoutedEventArgs e)
        {        
            timer.Start();
            b_reset.IsEnabled = false;
            b_start.IsEnabled = false;
        }


        private void b_resetmethod(object sender, RoutedEventArgs e)
        {
            b_start.IsEnabled = true;
            b_reset.IsEnabled = false;

            double posStart = 0;
            image_1.SetValue(Canvas.LeftProperty, posStart);
        }

        private void cB_player1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbx = sender as ComboBox;
            var kont = ((Player)(sender as ComboBox).SelectedItem).wallet;

            kont = Math.Min(kont, App._gameDaten.maxBet);

            
            //Bestimmten Slider finden
            var index = cbx.Name.Substring(cbx.Name.Length - 1);
            Slider sli = (Slider) grid.FindName("sli_slider_" + index);
            sli.Minimum = 1;
            sli.Maximum = kont;
            //sli.ToolTip
            //TextBox tbx = (TextBox) grid.Findname("tB_wallet_" + index);
        }
    }
}
