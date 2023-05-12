using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

class Radar { }

class obiekty_latajace { }


namespace po_projekt_kontrola_lotu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private int _counter = 0;
        public MainWindow()
        {
            //poczatek main

            InitializeComponent();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += new EventHandler(dispatcherTimer_Tick);


            //koniec main
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            _counter++;
            TimerBox.Text = _counter.ToString();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            _timer.Start();
            Color customColor = Color.FromArgb(50, 0, 255, 0);
            this.TimerBox.Background = new SolidColorBrush(customColor);
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            Color customColor = Color.FromArgb(50, 255, 0, 0);
            this.TimerBox.Background = new SolidColorBrush(customColor);
        }

        private void Timer_Reset_Click(object sender, RoutedEventArgs e)
        {
            _counter = 0;
            TimerBox.Text = _counter.ToString();
        }
    }
}
