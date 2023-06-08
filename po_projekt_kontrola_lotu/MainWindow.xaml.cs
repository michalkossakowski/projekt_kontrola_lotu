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

        // timer zmienne globalne
        private DispatcherTimer _timer;
        private double _counter = 0;

        // mainwindow
        public MainWindow()
        {
            ///////////////////////////////////////////////// poczatek main

            // wyłączenie rozszerzania okna
            this.ResizeMode = ResizeMode.NoResize;

            //timer
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += new EventHandler(dispatcherTimer_Tick);

            ///////////////////////////////////////////////// koniec main
        }

        ///////////////////////////////////////////////// Obsługa Timer
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

        /////////////////////////////////////////////////// Resetowanie
        //przycisk reset
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            //reset timera mapy i legendy
            ResetFun();
            //reset wczytywania pliku
            wczytaj.Content = "Wczytaj Plik";
            wczytaj.IsEnabled = true;
            //reset obiektow
            ResetFlyObj();
            // ukrywanie interfejsu
            HideGeneruj();
            HideTimer();
            HideZmienTrase();
        }
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
        //ukrywanie Generowania statków
        private void HideGeneruj()
        {
            ilosc_statkow.Visibility = Visibility.Hidden;
            slider1Text.Visibility = Visibility.Hidden;
            wygeneruj_trasy.Visibility = Visibility.Hidden;
            slider1.Visibility = Visibility.Hidden;
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

        ///////////////////////////////////////////// wczytywanie mapy

        //rysowanie obiektów na mapie
        private void CreateObject(double x, double y)
        {
            Random rnd = new Random();
            Brush br1 = new SolidColorBrush(Color.FromRgb(100, 255, 100));
            // Twórz obiekt na mapie o określonych koordynatach
            Rectangle kwadraty = new Rectangle
            {
                Width = rnd.Next(10, 51),
                Height = rnd.Next(10, 51),
                Fill = br1,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                Margin = new Thickness(x, y, 0, 0)
            };
            Mapa.Children.Add(kwadraty);
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
                            MessageBox.Show("Nieprawidłowe dane: " + linia);
                    }
                    else
                        MessageBox.Show("Nieprawidłowy format linii: " + linia);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas wczytywania danych z pliku: " + ex.Message);
            }
        }

        //przycisk wczytaj
        private void Wczytaj_Click(object sender, RoutedEventArgs e)
        {
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
            Rectangle kwadrat = new Rectangle(); //dodaje wczytany kwadrat
            Brush br1 = new SolidColorBrush(Color.FromRgb(100, 255, 100));
            kwadrat.Width = 20;
            kwadrat.Height = 20;
            kwadrat.Fill = br1;
            kwadrat.Stroke = Brushes.Black;
            kwadrat.StrokeThickness = 1;
            kwadrat.Margin = new Thickness(1, 5, 1, 5);

            TextBlock opis = new TextBlock(); //dodaje komentarz 
            opis.Text = "Budynki";
            opis.VerticalAlignment = VerticalAlignment.Center;
            opis.Margin = new Thickness(5, 0, 0, 0);
            Grid legendGrid = new Grid(); //dzieli legendę na dwie kolumny. Jedną obrazkową, drugą opisową
            legendGrid.ColumnDefinitions.Add(new ColumnDefinition());
            legendGrid.ColumnDefinitions.Add(new ColumnDefinition());

            legendGrid.Children.Add(kwadrat); //wpisuje kwadrat i komentarz do legendy
            legendGrid.Children.Add(opis);
            Grid.SetColumn(kwadrat, 0);
            Grid.SetColumn(opis, 1);

            LegendaContainer.Children.Add(legendGrid);
            ShowGeneruj();

        }
        private void ShowGeneruj()
        {
            ilosc_statkow.Visibility = Visibility.Visible;
            slider1Text.Visibility = Visibility.Visible;
            wygeneruj_trasy.Visibility = Visibility.Visible;
            slider1.Visibility = Visibility.Visible;
        }

        ///////////////////////////// obiekty latajace
        //slidery
        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (slider1 != null && slider1Text != null)
            {
                slider1Text.Text = ((double)Math.Round(slider1.Value)).ToString();
            }
        }
        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (slider2 != null && slider2Text != null)
            {
                slider2Text.Text = ((double)Math.Round(slider2.Value)).ToString();
            }
        }
        // tworzenie listy statków 
        List<FlyObject> ListaStatkow = new List<FlyObject>();
        // rysowanie obiektow latajacych
        private void CreateFlyObject(FlyObject FlOb,Brush brush1)
        {
            Ellipse FlyObj = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = brush1,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                // -5 bo przesuwa elipse
                Margin = new Thickness(FlOb.getPoczX()-5, FlOb.getPoczY()-5, 0, 0)
            };

            // Sprawdzenie kolizji z istniejącymi obiektami
            Rect newObjectRect = new Rect(FlOb.getPoczX() - 5, FlOb.getPoczY() - 5, 10, 10);
            foreach (var existingObject in ListaStatkow)
            {
                Rect existingObjectRect = new Rect(existingObject.getPoczX() - 5, existingObject.getPoczY() - 5, 10, 10);
                if (newObjectRect.IntersectsWith(existingObjectRect))
                {
                    // Usunięcie nachodzących punktów
                    FlyMapa.Children.Remove(FlyObj);

                    // Wyświetlenie komunikatu o kolizji obiektów
                    MessageBox.Show("Kolizja obiektów!");

                    return; // Zakończenie tworzenia obiektu w przypadku kolizji
                }
            }

            FlyMapa.Children.Add(FlyObj);
            ListaStatkow.Add(FlOb);
        }
        // rysowanie odcinkow
        private void CreateOdcinek(Odcinek o, Brush brush1)
        {
            Line linia1 = new Line();
            {
                var p1 = new Punkt(o.getP1());
                var p2 = new Punkt(o.getP2());

                linia1.X1 =p1.getX();
                linia1.Y1 =p1.getY();
                linia1.X2 = p2.getX();
                linia1.Y2 = p2.getY();
                linia1.Stroke = brush1;
                linia1.StrokeThickness = 2;
            }
            FlyMapa.Children.Add(linia1);
        }       


        public void DodajdoLegendy(string nazwa, Brush kolor)
        {
            Grid legendGrid = new Grid //tworzy siatkę, która posiada dwie kolumny
            {
                ColumnDefinitions =
                {
                new ColumnDefinition(),
                new ColumnDefinition()
                }
            };
        Ellipse kolo = new Ellipse(); //dodaje wczytane koło 
        kolo.Width = 20;
        kolo.Height = 20;  
        kolo.Fill = kolor;
        kolo.Stroke = Brushes.Black;
        kolo.StrokeThickness = 1;
        kolo.Margin = new Thickness(1, 5, 1, 5);

            TextBlock opisKola = new TextBlock(); //dodaje opis dla koła
        opisKola.Text = nazwa;
        opisKola.VerticalAlignment = VerticalAlignment.Center;
        opisKola.Margin = new Thickness(5, 0, 0, 0);

        legendGrid.Children.Add(kolo); //wpisuje koło i opis do legendy
        legendGrid.Children.Add(opisKola);

        Grid.SetColumn(kolo, 0);//ustawia wizualne przedstawienie po lewej
        Grid.SetColumn(opisKola, 1);// ustawia opis po prawej
        LegendaContainer.Children.Add(legendGrid);
        }
     

        //pokaz doubleerfejs
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
        private void wygeneruj_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            ResetFlyObj();
            for (int i = LegendaContainer.Children.Count - 1; i > 0; i--)
                LegendaContainer.Children.RemoveAt(i);
            double ilosc = ((double)Math.Round(slider1.Value));
            slider2.Maximum = ilosc ;
            Showinterface();


            //komentarz
            for (double i = 0; i < ilosc; i++)
            {
                var typ = rnd.Next(1, 5);
                if (typ == 1)
                {
                    var Statek = new Samolot(rnd.Next(120, 180), rnd.Next(120, 380));
                    ListaStatkow.Add(Statek);
                    CreateFlyObject(Statek, Statek.GetBrush());
                    List<Odcinek> TrasaStatek = Statek.getTrasa();
                    foreach (Odcinek odc in TrasaStatek)
                       CreateOdcinek(odc, Statek.GetBrush());
                    DodajdoLegendy("Samolot", Statek.GetBrush());
                }
                if (typ == 2)
                {
                    var Statek = new Smiglowiec(rnd.Next(120, 380), rnd.Next(120, 380));
                    ListaStatkow.Add(Statek);
                    CreateFlyObject(Statek, Statek.GetBrush());
                    List<Odcinek> TrasaStatek = Statek.getTrasa();
                    foreach (Odcinek odc in TrasaStatek)
                        CreateOdcinek(odc, Statek.GetBrush());
                    DodajdoLegendy("Śmigłowiec", Statek.GetBrush());
                }
                if(typ == 3) {
                    var Statek = new Balon(rnd.Next(120, 380), rnd.Next(120, 380));
                    ListaStatkow.Add(Statek);
                    CreateFlyObject(Statek, Statek.GetBrush());
                    List<Odcinek> TrasaStatek = Statek.getTrasa();
                    foreach (Odcinek odc in TrasaStatek)
                        CreateOdcinek(odc, Statek.GetBrush());
                    DodajdoLegendy("Balon", Statek.GetBrush());
                }
                if (typ == 4)
                {
                    var Statek = new Szybowiec(rnd.Next(120, 380), rnd.Next(120, 380));
                    ListaStatkow.Add(Statek);
                    CreateFlyObject(Statek, Statek.GetBrush());
                    List<Odcinek> TrasaStatek = Statek.getTrasa();
                    foreach (Odcinek odc in TrasaStatek)
                        CreateOdcinek(odc, Statek.GetBrush());
                    DodajdoLegendy("Szybowiec", Statek.GetBrush());
                }
            }
        }

        // zmiana trast
        private void zmiana_Click(object sender, RoutedEventArgs e)
        {
            var wybor = ((int)Math.Round(slider2.Value))-1;
            var statek = ListaStatkow[wybor];
            statek.zmien_trase();
            // MessageBox.Show("Zmieniono trasę statku nr:"+wybor+" ", "Switch 1");
            
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

        ///////////////////////////// timer, ruch statkow niedokonczony
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // timer
            _counter++;
            TimerBox.Text = _counter.ToString();
            //statki 
            FlyMapa.Children.Clear();
            int liczniklotow = LegendaContainer.Children.Count-1;
            foreach (var sta in ListaStatkow)
            {
                List<Odcinek> TrasaStatek = sta.getTrasa();
                if (TrasaStatek.Count>=1)
                {
                    sta.skok(TrasaStatek[0]);
                    TrasaStatek.RemoveAt(0);
                    CreateFlyObject(sta, sta.GetBrush());
                    foreach (Odcinek odc in TrasaStatek)
                        CreateOdcinek(odc, sta.GetBrush());
                }
                if(TrasaStatek.Count==0)
                {
                    liczniklotow--;
                    if(liczniklotow == 0)
                    {                     
                        for (int i = LegendaContainer.Children.Count - 1; i >= 0; i--)
                            LegendaContainer.Children.RemoveAt(i);
                    }
                }
            }


            // czy kolizja



        }





        /////////////////////////////////////////////// koniec window
    }
}


