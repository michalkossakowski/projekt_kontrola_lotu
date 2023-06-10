using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO; //potrzebne jest do plików tekstowych
using Path = System.IO.Path; //do wczytywania plików z folderu
using Microsoft.Win32;//do wczytywania plików z folderu

namespace po_projekt_kontrola_lotu
{
    public partial class MainWindow : Window
    {
        //////////////////////////////////////////////////////////// Mainwindow
        
        // timer zmienne globalne
        private DispatcherTimer _timer;
        private double _counter = 0;
        
        public MainWindow()
        {
            // wyłączenie rozszerzania okna
            this.ResizeMode = ResizeMode.NoResize;

            //timer
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += new EventHandler(Timer_Tick);
        }

        // *** Obsługa Timer ***

        //timer start
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _timer.Start();
            this.TimerBox.Background = new SolidColorBrush(Color.FromArgb(50, 0, 255, 0));
        }

        //timer stop
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            this.TimerBox.Background = new SolidColorBrush(Color.FromArgb(50, 255, 0, 0));
        }

        //timer reset
        private void ResetTimer()
        {
            _counter = 0;
            TimerBox.Text = _counter.ToString();
            _timer.Stop();
            this.TimerBox.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        // * * * Reset * * *

        //przycisk reset
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            resetAll();
        }
        //reset wszysktiego

        private void resetAll()
        {
            //reset timera mapy i legendy
            ResetFun();
            Reset.Visibility = Visibility.Hidden;
            //reset wczytywania pliku
            wczytaj.Content = "Wczytaj Plik";
            wczytaj.IsEnabled = true;
            //reset obiektow
            ResetFlyObj();
            // ukrywanie interfejsu
            HideGeneruj();
            HideTimer();
            HideZmienTrase();
            legendatext.Visibility = Visibility.Hidden;
            legendabox.Visibility = Visibility.Hidden;
        }

        // reset timera, mapy i legendy
        private void ResetFun()
        {
            //resetowanie timera
            ResetTimer();
            //reset Mapy i legendy
            Mapa.Children.Clear();
            LegendaContainer.Children.Clear();
        }

        //resetowanie statków
        private void ResetFlyObj()
        {
            FlyMapa.Children.Clear();
            ListaStatkow.Clear();
            ResetTimer();
        }

        // * * * Ukrywanie i pokazywanie elementów * * *

        //ukrywanie Generowania statków
        private void HideGeneruj()
        {
            ilosc_statkow.Visibility = Visibility.Hidden;
            slider1Text.Visibility = Visibility.Hidden;
            wygeneruj_trasy.Visibility = Visibility.Hidden;
            slider1.Visibility = Visibility.Hidden;
        }

        // Pokaz generowanie statków
        private void ShowGeneruj()
        {
            ilosc_statkow.Visibility = Visibility.Visible;
            slider1Text.Visibility = Visibility.Visible;
            wygeneruj_trasy.Visibility = Visibility.Visible;
            slider1.Visibility = Visibility.Visible;
        }

        // ukrywanie timera
        private void HideTimer()
        {
            start.Visibility = Visibility.Hidden;
            stop.Visibility = Visibility.Hidden;
            Timer_text.Visibility = Visibility.Hidden;
            TimerBox.Visibility = Visibility.Hidden;
        }

        // ukrywanie zmien trase
        private void HideZmienTrase()
        {
            wybierz_statek.Visibility = Visibility.Hidden;
            slider2.Visibility = Visibility.Hidden;
            slider2Text.Visibility = Visibility.Hidden;
            zmien_trase.Visibility = Visibility.Hidden;
        }

        //pokaz interfejs
        private void Showinterface()
        {
            zmien_trase.Visibility = Visibility.Visible;
            slider2Text.Visibility = Visibility.Visible;
            wybierz_statek.Visibility = Visibility.Visible;
            slider2.Visibility = Visibility.Visible;
            start.Visibility = Visibility.Visible;
            stop.Visibility = Visibility.Visible;
            Timer_text.Visibility = Visibility.Visible;
            TimerBox.Visibility = Visibility.Visible;
        }

        // * * * wczytywanie mapy * * * 

        //przycisk wczytaj
        private void Wczytaj_Click(object sender, RoutedEventArgs e)
        {
            Reset.Visibility = Visibility.Visible;
            legendatext.Visibility = Visibility.Visible;
            legendabox.Visibility = Visibility.Visible;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string nadrzednyFolder1 = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName;
            string nadrzednyFolder2 = Directory.GetParent(nadrzednyFolder1).FullName;
            string nadrzednyFolder3 = Directory.GetParent(nadrzednyFolder2).FullName;
            string nadrzednyFolder4 = Directory.GetParent(nadrzednyFolder3).FullName;

            string mapsDirectory = Path.Combine(nadrzednyFolder4, "Mapy");

            openFileDialog.InitialDirectory = mapsDirectory;


            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                wczytaj.Content = Path.GetFileName(selectedFilePath);
                wczytaj.IsEnabled = false;
                WczytajPlik(openFileDialog.FileName); //mapa z pliku
            }
            Legenda legenda = new Legenda();
            Grid budynkiGrid = legenda.StworzBudynek();

            LegendaContainer.Children.Add(budynkiGrid);
            ShowGeneruj();
        }

        //ładuje obiekty z pliku
        private void WczytajPlik(string sciezka_pliku)
        {
            try
            {
                string[] linie = File.ReadAllLines(sciezka_pliku);
                foreach (string linia in linie)
                {
                    if (linia.StartsWith("punkty(") && linia.EndsWith(")"))
                    {
                        string punkty = linia.Substring(7, linia.Length - 8);
                        string[] parts = punkty.Split(',');

                        if (parts.Length == 2 && double.TryParse(parts[0], out double x) && double.TryParse(parts[1], out double y))
                            CreateObject(x, y);
                        else
                            MessageBox.Show("Nieprawidłowe dane: " + linia+" \n Mapa została wczytana bez uszkodzonych linii !");
                    }
                    else
                        MessageBox.Show("Nieprawidłowy format linii: \""+linia+"\" \nMapa została wczytana bez uszkodzonych linii !");                                                                                                                                                                        
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas wczytywania danych z pliku: " + ex.Message);
            }
        }

        //rysowanie obiektów na mapie
        private void CreateObject(double x, double y)
        {
            var ob = new MapObject(new Punkt(x, y));
            // Twórz obiekt na mapie o określonych koordynatach
            Rectangle kwadraty = new Rectangle
            {
                Width = ob.getA(),
                Height = ob.getB(),
                Fill = new SolidColorBrush(Color.FromRgb(100, 255, 100)),
                Margin = new Thickness(x, y, 0, 0)
            };
            Mapa.Children.Add(kwadraty);
        }


        // * * * Generowanie Statkow * * *

        // tworzenie listy statków 
        List<FlyObject> ListaStatkow = new List<FlyObject>();

        // Wybor ilości obiektów do wygenerowania
        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (slider1 != null && slider1Text != null)
            {
                slider1Text.Text = ((double)Math.Round(slider1.Value)).ToString();
            }
        }

        // przycisk do generowania statków
        Legenda legend= new Legenda();  
        private void wygeneruj_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            ResetFlyObj();
            for (int i = LegendaContainer.Children.Count - 1; i > 0; i--)
                LegendaContainer.Children.RemoveAt(i);
            double ilosc = ((double)Math.Round(slider1.Value));
            slider2.Maximum = ilosc;
            Showinterface();

            for (int i = 1; i <= ilosc; i++)
            {
                var typ = rnd.Next(1, 5);
                FlyObject Statek;
                Grid legendGrid= new Grid();
                switch (typ)
                {
                    case 1:
                        Statek = new Samolot(rnd.Next(120, 180), rnd.Next(120, 380), i);
                        legendGrid=legend.DodajdoLegendy(i + " Samolot", Statek.GetBrush());
                        LegendaContainer.Children.Add(legendGrid);
                        break;
                    case 2:
                        Statek = new Smiglowiec(rnd.Next(120, 380), rnd.Next(120, 380), i);
                        legendGrid = legend.DodajdoLegendy(i + " Śmigłowiec", Statek.GetBrush());
                        LegendaContainer.Children.Add(legendGrid);
                        break;
                    case 3:
                        Statek = new Balon(rnd.Next(120, 380), rnd.Next(120, 380), i);
                        legendGrid = legend.DodajdoLegendy(i + " Balon", Statek.GetBrush());
                        LegendaContainer.Children.Add(legendGrid);
                        break;
                    case 4:
                        Statek = new Szybowiec(rnd.Next(120, 380), rnd.Next(120, 380), i);
                        legendGrid = legend.DodajdoLegendy(i + " Szybowiec", Statek.GetBrush());
                        LegendaContainer.Children.Add(legendGrid);
                        break;
                    default:
                        Statek = null;
                        break;
                }
                if(Statek != null)
                {
                    ListaStatkow.Add(Statek);
                    CreateFlyObject(Statek, Statek.GetBrush());
                    List<Odcinek> TrasaStatek = Statek.getTrasa();
                    foreach (Odcinek odc in TrasaStatek)
                        CreateOdcinek(odc, Statek.GetBrush());
                }
            }
        }

        // * * * Rysowanie na FlyMapie * * *

        // rysowanie obiektow latajacych
        private void CreateFlyObject(FlyObject FlOb, Brush brush1)
        {
            Ellipse FlyObjEl = new Ellipse
            {
                Width = 16,
                Height = 16,
                Fill = brush1,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                // -8 bo przesuwa elipse
                Margin = new Thickness(FlOb.getPoczX() - 8, FlOb.getPoczY() - 8, 0, 0)
            };
            FlyMapa.Children.Add(FlyObjEl);

            TextBlock idEl = new TextBlock();
            idEl.Text = FlOb.getId().ToString();
            idEl.Margin = new Thickness(FlOb.getPoczX() - 4, FlOb.getPoczY() + 4, 0, 0);
            idEl.FontSize = 15;
            idEl.FontWeight = FontWeights.Bold;
            idEl.Foreground = Brushes.Black;
            FlyMapa.Children.Add(idEl);

            TextBlock wysFlOb = new TextBlock();
            wysFlOb.Text = FlOb.getBiezWys().ToString();
            wysFlOb.Margin = new Thickness(FlOb.getPoczX() - 14, FlOb.getPoczY() - 26, 0, 0);
            wysFlOb.FontSize = 12;
            wysFlOb.FontWeight = FontWeights.Bold;
            wysFlOb.Foreground = Brushes.Black;
            wysFlOb.Background = brush1;
            FlyMapa.Children.Add(wysFlOb);
        }

        // rysowanie odcinkow
        private void CreateOdcinek(Odcinek o, Brush brush1)
        {
            var p1 = new Punkt(o.getP1());
            var p2 = new Punkt(o.getP2());
            Line linia1 = new Line();
            {
                linia1.X1 = p1.getX();
                linia1.Y1 = p1.getY();
                linia1.X2 = p2.getX();
                linia1.Y2 = p2.getY();
                linia1.Stroke = brush1;
                linia1.StrokeThickness = 2;
            }
            FlyMapa.Children.Add(linia1);
        }

        // * * * Legenda * * *


        // * * * Zmiana Trasy * * *

        // wybór obiektu do zmiany trasy
        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (slider2 != null && slider2Text != null)
            {
                slider2Text.Text = ((double)Math.Round(slider2.Value)).ToString();
            }
        }

        // przycisk do zmiany trasy
        private void zmiana_Click(object sender, RoutedEventArgs e)
        {
            var wybor = ((int)Math.Round(slider2.Value));

            int czy_ist = 0;
            foreach (var st in ListaStatkow)
            {
                if (st.getId() == wybor)
                {
                    st.zmien_trase();
                    czy_ist = 1;
                }
            }
            if(czy_ist == 0)
                MessageBox.Show("Wybrany statek nr: " + wybor + " został już zniszczony!", " Wybrano zły statek !!!");

            FlyMapa.Children.Clear();
            foreach (var sta in ListaStatkow)
            {
                List<Odcinek> TrasaStatek = sta.getTrasa();
                if (TrasaStatek.Count >= 1)
                {
                    CreateFlyObject(sta, sta.GetBrush());
                    foreach (Odcinek odc in TrasaStatek)
                        CreateOdcinek(odc, sta.GetBrush());
                }
            }
        }

        // * * * przejście timera co sekundę * * *
        private void Timer_Tick(object sender, EventArgs e)
        {
            // timer
            _counter++;
            TimerBox.Text = _counter.ToString();

            // czyszczenie statków
            FlyMapa.Children.Clear();
            int liczniklotow = LegendaContainer.Children.Count - 1;

            // rysowanie satkow na nowo po przejściu do następnego punktu
            foreach (var sta in ListaStatkow)
            {
                List<Odcinek> TrasaStatek = sta.getTrasa();
                if (TrasaStatek.Count >= 1)
                {
                    sta.skok(TrasaStatek[0]);
                    TrasaStatek.RemoveAt(0);
                    CreateFlyObject(sta, sta.GetBrush());
                    foreach (Odcinek odc in TrasaStatek)
                        CreateOdcinek(odc, sta.GetBrush());
                }
            }

            // usuwanie statkow ktore dotarly do konca 
            for(int i=0; i<ListaStatkow.Count ; i++)
            {
                var Tra = ListaStatkow[i].getTrasa();
                if (Tra.Count == 0)
                {
                    LegendaContainer.Children.RemoveAt(i + 1);
                    ListaStatkow.RemoveAt(i);
                    
                }
            }
            if (ListaStatkow.Count == 0)
            {
                MessageBox.Show("Wszystkie loty się zakończyły !", " Koniec lotów !");
                resetAll();
                return;
            }
            // sprawdzanie kolizji
            sprawdzKolizje();
        }
        
        // * * * Sprawdzanie czy istnije niebezpieczenstwo lub kolizja * * *
        private void sprawdzKolizje()
        {
            int ilStat = ListaStatkow.Count - 1;
            for (int i = 0; i < ilStat; i++)
            {
                var x1 = ListaStatkow[i].getPoczX();
                var y1 = ListaStatkow[i].getPoczY();
                var w1 = ListaStatkow[i].getBiezWys();
                for (int j = i + 1; j <= ilStat; j++)
                {
                    var x2 = ListaStatkow[j].getPoczX();
                    var y2 = ListaStatkow[j].getPoczY();
                    var w2 = ListaStatkow[j].getBiezWys();
                    if (Math.Abs(x1 - x2) < 16 && Math.Abs(y1 - y2) < 16 && Math.Abs(w1 - w2) < 100)
                    {
                        MessageBox.Show("Kolizja obiektu nr: " + ListaStatkow[i].getId() + " na wysokości: " + w1 +"\nz obiektm nr: " + ListaStatkow[j].getId() + " na wysokości: " + w2 + "\nOba obiekty zostaną zniszczone !", " Wykryto Kolizję !!!");
                        ListaStatkow.RemoveAt(j);
                        ListaStatkow.RemoveAt(i);
                        LegendaContainer.Children.RemoveAt(j+1);
                        LegendaContainer.Children.RemoveAt(i+1);
                        return;
                    }
                    if (Math.Abs(x1 - x2) < 32 && Math.Abs(y1 - y2) < 32 && Math.Abs(w1 - w2) < 300)
                    {
                        _timer.Stop();
                        this.TimerBox.Background = new SolidColorBrush(Color.FromArgb(50, 255, 0, 0));
                        MessageBoxResult result = MessageBox.Show("Obiekty nr: " + ListaStatkow[i].getId() + " na wysokości: " + w1 + " \noraz obiekt nr: " + ListaStatkow[j].getId() + " na wysokości: " + w2 + " \nsą niebezpiecznie blisko siebie ! \nCzy chcesz zatrzymać czas aby zmienić trasę ?", "Wykryto Niebezpieczeństwo !!!", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.No)
                        {
                            _timer.Start();
                            this.TimerBox.Background = new SolidColorBrush(Color.FromArgb(50, 0, 255, 0));
                        }
                    }
                }
            }
        }
        //////////////////////////////////////////////////////////// koniec Mainwindow
    }
}