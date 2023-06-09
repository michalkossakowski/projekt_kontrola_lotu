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
using po_projekt_kontrola_lotu;

class Legenda
{
    private string nazwa;
    private Brush kolor;
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
        kolo.Margin = new Thickness(0, 5, 1, 5);

        TextBlock opisKola = new TextBlock(); //dodaje opis dla koła
        opisKola.Text = nazwa;
        opisKola.VerticalAlignment = VerticalAlignment.Center;
        opisKola.Margin = new Thickness(0, 0, 0, 0);

        legendGrid.Children.Add(kolo); //wpisuje koło i opis do legendy
        legendGrid.Children.Add(opisKola);

        Grid.SetColumn(kolo, 0);//ustawia wizualne przedstawienie po lewej
        Grid.SetColumn(opisKola, 1);// ustawia opis po prawej
        LegendaContainer.Children.Add(legendGrid);
    }

}