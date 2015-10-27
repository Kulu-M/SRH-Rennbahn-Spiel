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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Speech.Synthesis;


namespace SRH_Rennbahn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Image winner;
        public double posX;
        public double posFin;
        public int mini, maxi;
        bool wechsel = false;
        public List <Image> winningracers = new List<Image>();
        public string[] winnerarray = new string[4];



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
            //Sprachnachricht
            //SpeechSynthesizer ss = new SpeechSynthesizer();
            //ss.Speak("Welcome! It is" + DateTime.Now);

            finishLine = (double)brd_finishline.GetValue(Canvas.LeftProperty);

            ////Aus einem Pool von Racern nur einige aussuchen, die starten
            //startingRacers.Add(App._gameDaten.racersList[1]);
            //startingRacers.Add(App._gameDaten.racersList[2]);
            //startingRacers.Add(App._gameDaten.racersList[3]);
            //startingRacers.Add(App._gameDaten.racersList[4]);
            //for (int i = 0; i < 2; i++)
            //{
            //    Image img = (Image)can_racetrack.FindName("image_" + i);
            //    var path = "Pics/" + startingRacers[i].name + "/0.png";
            //    BitmapImage bmi = new BitmapImage(new Uri(path, UriKind.Relative));
            //    img.Source = bmi;
            //}


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


        private void Timer_Tick(object sender, EventArgs e)
        {
            // Method to check for finishline
            checkFinishLine();

            
            {
                //Method to move pictures
                imageMover(image_0);
                imageMover(image_1);
                imageMover(image_2);
                imageMover(image_3);
                imageMover(image_4);
            }
                


            //CHANGE IMAGES
            if (wechsel)
            {
                imageChanger("Pics/Alien/0.png", image_0);
                imageChanger("Pics/Sonic/0.png", image_1);
                imageChanger("Pics/Ninja/0.png", image_3);
                imageChanger("Pics/Pikachu/0.png", image_2);
                imageChanger("Pics/Zombie/0.png", image_4);
            }
            else
            {
                if (!(winningracers.Contains(image_1))) imageChanger("Pics/Sonic/1.png", image_1);
                if (!(winningracers.Contains(image_0))) imageChanger("Pics/Alien/1.png", image_0);
                if (!(winningracers.Contains(image_3))) imageChanger("Pics/Ninja/1.png", image_3);
                if (!(winningracers.Contains(image_2))) imageChanger("Pics/Pikachu/1.png", image_2);
                if (!(winningracers.Contains(image_4))) imageChanger("Pics/Zombie/1.png", image_4);
            }
            wechsel = !wechsel;    
        }


        private void checkFinishLine()
        {
            //Check for end of race
            if (posX >= finishLine)
            {
                timer.Stop();
                b_reset.IsEnabled = true;

                
                int einsaetzecount = App._gameDaten.einsätze.Count;
                if (einsaetzecount > 0) {

                    for (int i = 0; i < einsaetzecount; i++)
                    {
                        try
                        {
                            tb_results.Opacity = 1;
                            winnerarray[i] = App._gameDaten.einsätze[i].player.name + " hat gespielt und " + App._gameDaten.einsätze[i].einsatz + " auf " + App._gameDaten.einsätze[i].racer.name + " gesetzt." + System.Environment.NewLine;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }

                    string output = string.Join("", winnerarray);
                    tb_results.Text = output;

                    winCalculation();
                }
            }
        }

        
        private void imageMover(Image racername)
        {
            //Eleganter: die letzte Stelle von racername, also die Nummer benutzten
            if (racername.Equals(image_0))
            {
                mini = App._gameDaten.racersList[0].minSpeed;
                maxi = App._gameDaten.racersList[0].maxSpeed;
            }
            else if (racername.Equals(image_1))
            {
                mini = App._gameDaten.racersList[1].minSpeed;
                maxi = App._gameDaten.racersList[1].maxSpeed;
            }
            else if (racername.Equals(image_2)) { 
                mini = App._gameDaten.racersList[2].minSpeed;
                maxi = App._gameDaten.racersList[2].maxSpeed;
            }
            else if (racername.Equals(image_3)) { 
                mini = App._gameDaten.racersList[3].minSpeed;
                maxi = App._gameDaten.racersList[3].maxSpeed;
            }
            else if (racername.Equals(image_4)) { 
                mini = App._gameDaten.racersList[4].minSpeed;
                maxi = App._gameDaten.racersList[4].maxSpeed;
            }

            //Generate Random numbers based on min and max skill values of racers 
            int rnd = RandomGenerator.rndGen(mini, maxi);
            var rac = racername;
            posX = (double)rac.GetValue(Canvas.LeftProperty) + 5 + rnd;
            posFin = (double)rac.GetValue(Canvas.LeftProperty);


            //Check auf: Racer im Ziel? ansonsten move racer und aktuellen racer zu liste hinzufügen
            if (posFin < finishLine)
            {
                rac.SetValue(Canvas.LeftProperty, posX);
            }
            else
            {
                winningracers.Add(racername);
            }  
        }

        private void imageChanger(string imgname, object racername)
        {
            BitmapImage bild = new BitmapImage(new Uri(imgname, UriKind.Relative));
            
            var rac = racername as Image;

            if (rac != null) rac.Source = bild;
        }

        private void b_startmethod(object sender, RoutedEventArgs e)
        {        
            timer.Start();
            b_reset.IsEnabled = false;
            b_start.IsEnabled = false;

            //Menü einklappen, wenn gestartet wird
            Storyboard sb = FindResource("wettbüro_raus") as Storyboard;
            sb?.Begin();

            //Wetten eintragen sobald gestartet wird
            int sli0 = (int)sli_slider_0.Value;
            int sli1 = (int) sli_slider_1.Value;
            int sli2 = (int)sli_slider_2.Value;
            int sli3 = (int)sli_slider_3.Value;
            Player p0 = (Player)cB_player0.SelectedItem;
            Player p1 = (Player)cB_player1.SelectedItem;
            Player p2 = (Player)cB_player2.SelectedItem;
            Player p3 = (Player)cB_player3.SelectedItem;
            Racer r0 = (Racer) cB_racer0.SelectedItem;
            Racer r1 = (Racer)cB_racer1.SelectedItem;
            Racer r2 = (Racer)cB_racer2.SelectedItem;
            Racer r3 = (Racer)cB_racer3.SelectedItem;


            App._gameDaten.einsätze = new List<Einsatz>()
            {
                new Einsatz {einsatz = sli0, gewinn = 0, player = p0, racer = r0},
                new Einsatz {einsatz = sli1, gewinn = 0, player = p1, racer = r1},
                new Einsatz {einsatz = sli2, gewinn = 0, player = p2, racer = r2},
                new Einsatz {einsatz = sli3, gewinn = 0, player = p3, racer = r3}
            };
        }

        private void b_resetmethod(object sender, RoutedEventArgs e)
        {
            b_start.IsEnabled = true;
            b_reset.IsEnabled = false;

            double posStart = 0;
            image_0.SetValue(Canvas.LeftProperty, posStart);
            image_1.SetValue(Canvas.LeftProperty, posStart);
            image_2.SetValue(Canvas.LeftProperty, posStart);
            image_3.SetValue(Canvas.LeftProperty, posStart);
            image_4.SetValue(Canvas.LeftProperty, posStart);

            tb_results.Opacity = 0;
        }

        private void cB_player0_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            any_cb_SelectionChanged(sender);            
        }

        private void cB_player1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            any_cb_SelectionChanged(sender);
        }

        private void cB_player2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            any_cb_SelectionChanged(sender);
        }

        private void cB_player3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            any_cb_SelectionChanged(sender);
        }

        public void any_cb_SelectionChanged(object sender)
        {
            //TESTWERTE
            App._gameDaten.playersList[0].wallet = 200;
            App._gameDaten.playersList[1].wallet = 2200;
            App._gameDaten.playersList[2].wallet = 30000;
            App._gameDaten.playersList[3].wallet = 0;

             //-----

            var cbx = sender as ComboBox;
            var kont = ((Player)(sender as ComboBox).SelectedItem).wallet;

            //Ist wallet oder maxbet größer?
            kont = Math.Min(kont, App._gameDaten.maxBet);

            //Bestimmten Slider finden
            string index = cbx.Name.Substring(cbx.Name.Length - 1);
            Slider sli = (Slider)grid.FindName("sli_slider_" + index);
            sli.Minimum = 1;
            sli.Maximum = kont;

            //Aus gefundenen Slider wallet entnehmen und in entpsrechende tbx eintragen
            TextBox tbx = (TextBox)grid.FindName("tB_wallet_" + index);
            int indexint;
            indexint = int.Parse(index);
            if (tbx != null) tbx.Text = App._gameDaten.playersList[indexint].wallet.ToString();

        }


        public void winCalculation()
        {
            try
            {
                if (winningracers[0].Equals(image_1))
                {
                    for (int j = 0; j <= App._gameDaten.einsätze.Count; j++)
                    {
                        if (App._gameDaten.einsätze[j].racer.name.Equals("Sonic"))
                        {
                            App._gameDaten.einsätze[j].player.wallet += App._gameDaten.einsätze[j].einsatz*2;
                            MessageBox.Show(App._gameDaten.einsätze[j].player.wallet.ToString());
                        }  
                    }
                    
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            
            
        }



    }
}
