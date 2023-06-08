using System.Collections.Generic;
using System.Windows.Media;
using System;

class Odcinek
{
    private Punkt p1;
    private Punkt p2;
    private double wysokosc;
    private double predkosc;
    private double kierunek;

    public Odcinek(Punkt pp1, double wys, double pred, double kie)
    {
        Random rnd = new Random();
        this.p1 = pp1;
        this.p2 = new Punkt(pp1);
        this.wysokosc = wys;
        this.predkosc = pred;
        this.kierunek = kie;
        wybierzKierunek();
        kierunekPrzes(kierunek);
    }

    private void wybierzKierunek()
    {
        Random rnd = new Random();
        switch (kierunek)
        {
            case 1:
                losKier(8, 2);
                break;
            case 2:
                losKier(1, 3);
                break;
            case 3:
                losKier(2, 4);
                break;
            case 4:
                losKier(3, 5);
                break;
            case 5:
                losKier(4, 6);
                break;
            case 6:
                losKier(5, 7);
                break;
            case 7:
                losKier(6, 8);
                break;
            case 8:
                losKier(7, 1);
                break;
            default:
                kierunek = rnd.Next(1, 9);
                break;
        }
    }
    private void losKier(double a, double b)
    {
        Random rnd = new Random();
        var los = rnd.Next(1, 3);
        switch (los)
        {
            case 1:
                kierunek = a;
                break;
            case 2:
                kierunek = b;
                break;
        }
    }
    private void kierunekPrzes(double kierunek)
    {
        double a = (predkosc * Math.Sqrt(2)) / 2;
        switch (kierunek)
        {
            case 1:
                p2.przesun(predkosc, 0);
                break;
            case 2:
                p2.przesun(a, a);
                break;
            case 3:
                p2.przesun(0, predkosc);
                break;
            case 4:
                p2.przesun(-a, a);
                break;
            case 5:
                p2.przesun(-predkosc, 0);
                break;
            case 6:
                p2.przesun(-a, -a);
                break;
            case 7:
                p2.przesun(0, -predkosc);
                break;
            case 8:
                p2.przesun(a, -a);
                break;
        }
    }
    public double getKierunek() {
        return kierunek;
    }
    public double getWysokosc()
    {
        return wysokosc;
    }
    public Punkt getP1()
    {
        return p1;
    }
    public Punkt getP2()
    {
        return p2;
    }
}
abstract class FlyObject
{
    protected Punkt pocz;
    protected List<Odcinek> Trasa;
    protected Brush brush1;
    public FlyObject(double x, double y)
    {
        this.pocz = new Punkt(x, y);
        Trasa = new List<Odcinek>();
    }
    public virtual Brush GetBrush()
    {
        return brush1;
    }
    public virtual List<Odcinek> getTrasa()
    {
        return Trasa;
    }
    public virtual double getPoczX()
    {
        return pocz.getX();
    }
    public virtual double getPoczY()
    {
        return pocz.getY();
    }
    public virtual void przesun(double x, double y)
    {
        pocz.przesun(x,y);
    }
    // skoki co odcinek
    public virtual void skok(Odcinek odc)
    {
        pocz = odc.getP2();
    }
    // zmiana trasy 
    public abstract void zmien_trase();

}

class Samolot : FlyObject
{

    public Samolot(double x, double y) : base(x,y)
    {
        // kolor czerwony
        brush1 = new SolidColorBrush(Color.FromRgb(255,0,0));
        zmien_trase();
    }

    public override void zmien_trase()
    {
        Random rnd = new Random();
        Trasa.Clear();
        var p1 = new Punkt(pocz.getX(), pocz.getY());
        for (double i = 0; i < rnd.Next(20, 50); i++)
        {
            // wpisywanie do listy odcinkow dla samolot / wysokowsc 900-1200 / predkosc 20-40
            Odcinek odc;
            double kier;
            if (Trasa.Count == 0)
                kier = 0;
            else
                kier = Trasa[Trasa.Count - 1].getKierunek();
            odc = new Odcinek(p1, rnd.Next(1000, 1500), rnd.Next(20, 40), kier);
            Trasa.Add(odc);
            var p2 = odc.getP2();
            p1 = new Punkt(p2);
            if (p2.getX() > 460 || p2.getX() < 40 || p2.getY() > 460 || p2.getY() < 40)
              return;
        }
    }
}
class Smiglowiec : FlyObject {
    public Smiglowiec(double x, double y) : base(x, y)
    {
        // kolor niebieski
        brush1 = new SolidColorBrush(Color.FromRgb(0, 155, 255));
        zmien_trase();
    }
    public override void zmien_trase()
    {
        Random rnd = new Random();
        Trasa.Clear();
        var p1 = new Punkt(pocz.getX(), pocz.getY());
        for (double i = 0; i < rnd.Next(20, 50); i++)
        {
            // wpisywanie do listy odcinkow dla samolot // wysokowsc 100-200 // predkosc 15-30
            Odcinek odc;
            double kier;
            if (Trasa.Count == 0)
                kier = 0;
            else
                kier = Trasa[Trasa.Count - 1].getKierunek();
            odc = new Odcinek(p1, rnd.Next(100, 200), rnd.Next(15, 30), kier);
            Trasa.Add(odc);
            var p2 = odc.getP2();
            p1 = new Punkt(p2);
            if (p2.getX() > 460 || p2.getX() < 40 || p2.getY() > 460 || p2.getY() < 40)
                return;
        }
    }
}
class Balon : FlyObject
{
    public Balon(double x, double y) : base(x, y)
    {
        // kolor rózowy 
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
class Szybowiec : FlyObject
{
    public Szybowiec(double x, double y) : base(x, y)
    {
        // kolor pomaranczowy
        brush1 = new SolidColorBrush(Color.FromRgb(255, 125, 0));
        zmien_trase();
    }
    public override void zmien_trase()
    {
        Random rnd = new Random();
        Trasa.Clear();
        var p1 = new Punkt(pocz.getX(), pocz.getY());
        for (double i = 0; i < rnd.Next(20, 50); i++)
        {
            // wpisywanie do listy odcinkow dla samolot wysokowsc / 100-300 / predkosc 10-20
            Odcinek odc;
            double kier;
            if (Trasa.Count == 0)
                kier = 0;
            else
                kier = Trasa[Trasa.Count - 1].getKierunek();
            odc = new Odcinek(p1, rnd.Next(100, 300), rnd.Next(10, 20), kier);
            Trasa.Add(odc);
            var p2 = odc.getP2();
            p1 = new Punkt(p2);
            if (p2.getX() > 460 || p2.getX() < 40 || p2.getY() > 460 || p2.getY() < 40)
                return;
        }
    }
}