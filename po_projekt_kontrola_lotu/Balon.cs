using System.Windows.Media;
using System;

class Balon : FlyObject
{
    public Balon(double x, double y, int id) : base(x, y, id)
    {
        // kolor rózowy 
        Random rnd = new Random();
        bierzaca_wysokosc = rnd.Next(100, 500);
        brush1 = new SolidColorBrush(Color.FromRgb(255, 0, 255));
        zmien_trase();
    }
    public override void zmien_trase()
    {
        Random rnd = new Random();
        Trasa.Clear();
        var p1 = new Punkt(pocz.getX(), pocz.getY());
        for (double i = 0; i < rnd.Next(20, 50); i++)
        {
            // wpisywanie do listy odcinkow dla samolot / wysokowsc 100-200 /  predkosc 5-10
            Odcinek odc;
            double kier;
            if (Trasa.Count == 0)
                kier = 0;
            else
                kier = Trasa[Trasa.Count - 1].getKierunek();
            odc = new Odcinek(p1, rnd.Next(100, 500), rnd.Next(5, 10), kier);
            Trasa.Add(odc);
            var p2 = odc.getP2();
            p1 = new Punkt(p2);
            if (p2.getX() > 460 || p2.getX() < 40 || p2.getY() > 460 || p2.getY() < 40)
                return;
        }
    }
}