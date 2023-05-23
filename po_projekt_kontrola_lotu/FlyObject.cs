using System.Collections.Generic;
using System.Windows.Media;
using System;

class Odcinek
{
    private Punkt p1;
    private Punkt p2;
    private int wysokosc;
    public int predkosc;
    public Odcinek(Punkt pp1, Punkt pp2, int wys, int pred)
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

    public FlyObject(int x, int y)
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

    public virtual int getPoczX()
    {
        return pocz.getX();
    }
    public virtual int getPoczY()
    {
        return pocz.getY();
    }
    public virtual void przesun(int x, int y)
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
    public Samolot(int x, int y) : base(x,y)
    {
        Random rnd = new Random();
        // kolor czerwony
        brush1 = new SolidColorBrush(Color.FromRgb(255,0,0));
        var p1 = new Punkt(pocz.getX(), pocz.getY());
        for (int i = 0; i < rnd.Next(8, 20); i++)
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
    public Smiglowiec(int x, int y) : base(x, y)
    {
        Random rnd = new Random();
        // koor niebieski
        brush1 = new SolidColorBrush(Color.FromRgb(0, 0, 255));
        var p1 = new Punkt(pocz.getX(), pocz.getY());
        for (int i = 0; i < rnd.Next(2, 4); i++)
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
    public Balon(int x, int y) : base(x, y)
    {
        Random rnd = new Random();
        // kolor rózowy 
        brush1 = new SolidColorBrush(Color.FromRgb(255, 0, 255));
        var p1 = new Punkt(pocz.getX(), pocz.getY());
        for (int i = 0; i < rnd.Next(2, 4); i++)
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
    public Szybowiec(int x, int y) : base(x, y)
    {
        Random rnd = new Random();
        // kolor pomaranczowy
        brush1 = new SolidColorBrush(Color.FromRgb(255, 125, 0));
        var p1 = new Punkt(pocz.getX(), pocz.getY());
        for (int i = 0; i < rnd.Next(2, 4); i++)
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