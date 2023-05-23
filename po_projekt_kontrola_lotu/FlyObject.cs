using System.Collections.Generic;
using System.Windows.Media;
using System;

class Odcinek
{
    private Punkt p1;
    private Punkt p2;
    private double wysokosc;
    public double predkosc;
    public Odcinek(Punkt pp1, Punkt pp2, double wys, double pred)
    {
        this.p1 = pp1;
        this.p2 = pp2;
        this.wysokosc = wys;
        this.predkosc = pred;
    }
    public Odcinek(Odcinek o)
    {
        this.p1 = o.p1;
        this.p2 = o.p2;
        this.wysokosc = o.wysokosc;
        this.predkosc = o.predkosc;
    }

    // losowy odcinek o podanej dlugosci z wybranego puntu 
    public Odcinek(Punkt pp1,double wys, double pred)
    {
        this.p1 = pp1;
        this.wysokosc = wys;
        this.predkosc = pred;
        this.p2 = pp1;
        Random rnd = new Random();

        var kierunek = rnd.Next(1, 9);
        if (kierunek==1)
        {
            p2.przesun(pred, 0);
        }
        if (kierunek == 2)
        {
            p2.przesun(-pred, 0);
        }
        if (kierunek == 3)
        {
            p2.przesun(0, pred);
        }
        if (kierunek == 4)
        {
            p2.przesun(0, -pred);
        }
        if (kierunek == 5)
        {
            p2.przesun(pred, 0);
        }
        if (kierunek == 6)
        {
            p2.przesun(pred, 0);
        }
        if (kierunek == 7)
        {
            p2.przesun(pred, 0);
        }
        if (kierunek == 8)
        {
            p2.przesun(pred, 0);
        }


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
}

class Samolot : FlyObject
{
    public Samolot(double x, double y) : base(x,y)
    {
        Random rnd = new Random();
        // kolor czerwony
        brush1 = new SolidColorBrush(Color.FromRgb(255,0,0));
        var p1 = new Punkt(pocz.getX(), pocz.getY());
        for (double i = 0; i < rnd.Next(8, 20); i++)
        {
            var p2 = new Punkt(rnd.Next(20, 480), rnd.Next(20, 480));
            // wpisywanie do listy odcinkow dla samolot
            // wysokowsc 1000-1500
            // predkosc 80-140
            var odc = new Odcinek(p1, p2, rnd.Next(1000, 1500), rnd.Next(80, 140));
            Trasa.Add(odc);
            p1 = new Punkt(p2);
        }
    }
}
class Smiglowiec : FlyObject {
    public Smiglowiec(double x, double y) : base(x, y)
    {
        Random rnd = new Random();
        // koor niebieski
        brush1 = new SolidColorBrush(Color.FromRgb(0, 0, 255));
        var p1 = new Punkt(pocz.getX(), pocz.getY());
        for (double i = 0; i < rnd.Next(2, 4); i++)
        {
            var p2 = new Punkt(rnd.Next(20, 480), rnd.Next(20, 480));
            // wpisywanie do listy odcinkow dla samolot
            // wysokowsc 600-800
            // predkosc 30-70
            var odc = new Odcinek(p1, p2, rnd.Next(600, 800), rnd.Next(30, 70));
            Trasa.Add(odc);
            p1 = new Punkt(p2);
        }
    }
}
class Balon : FlyObject
{
    public Balon(double x, double y) : base(x, y)
    {
        Random rnd = new Random();
        // kolor rózowy 
        brush1 = new SolidColorBrush(Color.FromRgb(255, 0, 255));
        var p1 = new Punkt(pocz.getX(), pocz.getY());
        for (double i = 0; i < rnd.Next(2, 4); i++)
        {
            var p2 = new Punkt(rnd.Next(20, 480), rnd.Next(20, 480));
            // wpisywanie do listy odcinkow dla samolot
            // wysokowsc 100-300
            // predkosc 10-30
            var odc = new Odcinek(p1, p2, rnd.Next(100, 300), rnd.Next(10, 30));
            Trasa.Add(odc);
            p1 = new Punkt(p2);
        }
    }
}
class Szybowiec : FlyObject
{
    public Szybowiec(double x, double y) : base(x, y)
    {
        Random rnd = new Random();
        // kolor pomaranczowy
        brush1 = new SolidColorBrush(Color.FromRgb(255, 125, 0));
        var p1 = new Punkt(pocz.getX(), pocz.getY());
        for (double i = 0; i < rnd.Next(2, 4); i++)
        {
            var p2 = new Punkt(rnd.Next(20, 480), rnd.Next(20, 480));
            // wpisywanie do listy odcinkow dla samolot
            // wysokowsc 400-900
            // predkosc 20-60
            var odc = new Odcinek(p1, p2, rnd.Next(400, 900), rnd.Next(20, 60));
            Trasa.Add(odc);
            p1 = new Punkt(p2);
        }
    }
}