﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
using System.IO; //potrzebne jest do plików tekstowych
using Path = System.IO.Path;
using Microsoft.Win32;

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
            InitializeComponent();
            //poczatek main
            // wyłączenie rozszerzania okna
            this.ResizeMode = ResizeMode.NoResize;
            //timer
            
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += new EventHandler(dispatcherTimer_Tick);
            //LoadMapObjectsFromFile("obiekty.txt"); //nakaz wywołania metody ze zmienną "obiekty.txt"

            //koniec main
        }

        //timer
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            _counter++;
            TimerBox.Text = _counter.ToString();
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _timer.Start();
            this.TimerBox.Background = new SolidColorBrush(Color.FromArgb(50, 0, 255, 0));
        }
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            this.TimerBox.Background = new SolidColorBrush(Color.FromArgb(50, 255, 0, 0));
        }


        //reset
        private void ResetSoft()
        {
            // timer
            _counter = 0;
            TimerBox.Text = _counter.ToString();
            _timer.Stop();
            this.TimerBox.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            // slidery
            zmien_trase.Visibility = Visibility.Hidden;
            slider2Text.Visibility = Visibility.Hidden;
            wybierz_statek.Visibility = Visibility.Hidden;
            slider2.Visibility = Visibility.Hidden;
        }
        private void ResetFun()
        {
            ResetSoft();
            //reset wczytanego pliku
            Mapa.Children.Clear();
            LegendaContainer.Children.Clear();
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ResetFun();
            //wczytywanie 
            wczytaj.Content = "Wczytaj Plik";
            wczytaj.IsEnabled = true;

        }



        //slidery
        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (slider1 != null && slider1Text != null)
            {
                slider1Text.Text = ((int)Math.Round(slider1.Value)).ToString();
            }
        }
        private void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (slider2 != null && slider2Text != null)
            {
                slider2Text.Text = ((int)Math.Round(slider2.Value)).ToString();
            }
        }
        // generowanie statkow
        private void wygeneruj_Click(object sender, RoutedEventArgs e)
        {
            ResetSoft();
            slider2.Maximum = ((int)Math.Round(slider1.Value));
            zmien_trase.Visibility = Visibility.Visible;
            slider2Text.Visibility = Visibility.Visible;
            wybierz_statek.Visibility = Visibility.Visible;
            slider2.Visibility = Visibility.Visible;
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
                WczytajPlik (openFileDialog.FileName); //mapa z pliku
            }
            Rectangle kwadrat = new Rectangle(); //dodaje wczytany kwadrat
            kwadrat.Width = 20;
            kwadrat.Height = 20;
            kwadrat.Fill = Brushes.DarkOliveGreen;
            kwadrat.Stroke = Brushes.Black;
            kwadrat.StrokeThickness = 1;

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

                        if (parts.Length == 2 && int.TryParse(parts[0], out int x) && int.TryParse(parts[1], out int y))
                        {
                            CreateObject(x, y);
                        }
                        else
                        {
                            MessageBox.Show("Nieprawidłowe dane: " + linia);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nieprawidłowy format linii: " + linia);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas wczytywania danych z pliku: " + ex.Message);
            }
        }
        //tworzy obiekty na mapie
        private void CreateObject(int x, int y)
        {
            Random rnd = new Random();
            // Twórz obiekt na kanwie o określonych koordynatach
            Rectangle kwadraty = new Rectangle
            {

                Width = rnd.Next(10,51),
                Height =rnd.Next(10, 51),
                Fill = Brushes.DarkOliveGreen,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                Margin = new Thickness(x, y, 0, 0)
            };
            Mapa.Children.Add(kwadraty);
        }
    }
}


