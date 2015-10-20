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

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;
        }


        bool wechsel = false;
        private void Timer_Tick(object sender, EventArgs e)
        {
            var posX = (double)imageSonic.GetValue(Canvas.LeftProperty) + 10;
            imageSonic.SetValue(Canvas.LeftProperty, posX);

            if (posX >= finishLine)
                timer.Stop();
            if (wechsel)
                imageChanger("Pics/sonic2.png");
            else
                imageChanger("Pics/sonic1.png");

            wechsel = !wechsel;
        }


        private void imageChanger(string imgname)
        {
            BitmapImage bi = new BitmapImage(new Uri(imgname, UriKind.Relative));
            imageSonic.Source = bi;
            imageAlien.Source = bi;
            imageNinja.Source = bi;
            imageZombie.Source = bi;
            imagePika.Source = bi;
        }


        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            finishLine = (double) brd_finishline.GetValue(Canvas.LeftProperty);
        }


        private void ImageSnake_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            timer.Start();
        }
    }
}
