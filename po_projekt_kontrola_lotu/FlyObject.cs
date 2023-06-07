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
        
        if (kierunek == 0)
            kierunek = rnd.Next(1, 9);
        else if (kierunek == 1)
            losKier(8, 2);
        else if (kierunek == 2)
            losKier(1, 3);
        else if (kierunek == 3)
            losKier(2, 4);
        else if (kierunek == 4)
            losKier(3, 5);
        else if (kierunek == 5)
            losKier(4, 6);
        else if (kierunek == 6)
            losKier(5, 7);
        else if (kierunek == 7)
            losKier(6, 8);
        else if (kierunek == 8)
            losKier(7, 1);
        kierunekFun(kierunek);
    }

    private void losKier(double a, double b)
    {
        Random rnd = new Random();
        var los = rnd.Next(1, 3);
        if (los == 1)
            kierunek = a;
        if (los == 2)
            kierunek = b;
    }
    private int sprawdzZakres()
    {
        if (p2.getX() > 480 || p2.getX() < 20 || p2.getY() > 480 || p2.getY() < 20)
        {
            if (kierunek == 1)
                losKier(4, 6);
            else if (kierunek == 2)
                losKier(5, 7);
            else if (kierunek == 3)
                losKier(6, 8);
            else if (kierunek == 4)
                losKier(1, 7);
            else if (kierunek == 5)
                losKier(8, 2);
            else if (kierunek == 6)
                losKier(1, 3);
            else if (kierunek == 7)
                losKier(4, 2);
            else if (kierunek == 8)
                losKier(5, 3);
            return 1;
        }
        else { 
            return 0;
        }
    }
    private void kierunekFun(double kierunek)
    {
        double a = (predkosc * Math.Sqrt(2)) / 2;
        if (kierunek == 1)
        {
            p2.przesun(predkosc, 0);
            if (sprawdzZakres() == 1)
                p2.przesun(-predkosc, 0);
        }
        if (kierunek == 2)
        {
            p2.przesun(a, a);
            if (sprawdzZakres() == 1)
                p2.przesun(-a, -a);
        }
        if (kierunek == 3)
        {
            p2.przesun(0, predkosc);
            if (sprawdzZakres() == 1)
                p2.przesun(0,-predkosc);
        }
        if (kierunek == 4)
        {
            p2.przesun(-a, a);
            if (sprawdzZakres() == 1)
                p2.przesun(a,-a);
        }
        if (kierunek == 5)
        {
            p2.przesun(-predkosc, 0);
            if (sprawdzZakres() == 1)
                p2.przesun(predkosc, 0);
        }
        if (kierunek == 6)
        {
            p2.przesun(-a, -a);
            if (sprawdzZakres() == 1)
                p2.przesun(a,a);
        }
        if (kierunek == 7)
        {
            p2.przesun(0, -predkosc);
            if (sprawdzZakres() == 1)
                p2.przesun(0,predkosc);
        }
        if (kierunek == 8)
        {
            p2.przesun(a, -a);
            if (sprawdzZakres() == 1)
                p2.przesun(-a,a);
        }
    }

    public double getKierunek() {
        return kierunek;
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
        Random rnd = new Random();
        // kolor czerwony
        brush1 = new SolidColorBrush(Color.FromRgb(255,0,0));
        var p1 = new Punkt(pocz.getX(), pocz.getY());
        for (double i = 0; i < rnd.Next(20, 50); i++)
        {
            // wpisywanie do listy odcinkow dla samolot
            // wysokowsc 900-1200
            // predkosc 20-40
            Odcinek odc;
            if (Trasa.Count == 0)
                odc = new Odcinek(p1, rnd.Next(1000, 1500), rnd.Next(20, 40), 0);
            else
                odc = new Odcinek(p1, rnd.Next(1000, 1500), rnd.Next(20, 40), Trasa[Trasa.Count - 1].getKierunek());

            Trasa.Add(odc);
            var p2 = odc.getP2();
            p1 = new Punkt(p2);
        }
    }

    public override void zmien_trase()
    {
        Random rnd = new Random();
        Trasa.Clear();
        var p1 = new Punkt(pocz.getX(), pocz.getY());
        for (double i = 0; i < rnd.Next(20, 50); i++)
        {
            // wpisywanie do listy odcinkow dla samolot
            // wysokowsc 900-1200
            // predkosc 20-40
            Odcinek odc;
            if (Trasa.Count == 0)
                odc = new Odcinek(p1, rnd.Next(1000, 1500), rnd.Next(20, 40), 0);
            else
                odc = new Odcinek(p1, rnd.Next(1000, 1500), rnd.Next(20, 40), Trasa[Trasa.Count - 1].getKierunek());

            Trasa.Add(odc);
            var p2 = odc.getP2();
            p1 = new Punkt(p2);
        }
    }
}
class Smiglowiec : FlyObject {
    public Smiglowiec(double x, double y) : base(x, y)
    {
        Random rnd = new Random();
        // kolor niebieski
        brush1 = new SolidColorBrush(Color.FromRgb(0, 0, 255));
        var p1 = new Punkt(pocz.getX(), pocz.getY());
        for (double i = 0; i < rnd.Next(20, 50); i++)
        {
            // wpisywanie do listy odcinkow dla smiglowiec
            // wysokowsc 100-200
            // predkosc 15-30
            Odcinek odc;
            if(Trasa.Count == 0)
                odc = new Odcinek(p1, rnd.Next(100, 200), rnd.Next(15, 30), 0);
            else
                odc = new Odcinek(p1, rnd.Next(100, 200), rnd.Next(15, 30), Trasa[Trasa.Count - 1].getKierunek());
            
            Trasa.Add(odc);
            var p2 = odc.getP2();
            p1 = new Punkt(p2);
        }
    }

    public override void zmien_trase()
    {
        Random rnd = new Random();
        Trasa.Clear();
        var p1 = new Punkt(pocz.getX(), pocz.getY());
        for (double i = 0; i < rnd.Next(20, 50); i++)
        {
            // wpisywanie do listy odcinkow dla samolot
            // wysokowsc 900-1200
            // predkosc 20-40
            Odcinek odc;
            if (Trasa.Count == 0)
                odc = new Odcinek(p1, rnd.Next(1000, 1500), rnd.Next(20, 40), 0);
            else
                odc = new Odcinek(p1, rnd.Next(1000, 1500), rnd.Next(20, 40), Trasa[Trasa.Count - 1].getKierunek());

            Trasa.Add(odc);
            var p2 = odc.getP2();
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
        for (double i = 0; i < rnd.Next(20, 50); i++)
        {
            // wpisywanie do listy odcinkow dla balon
            // wysokowsc 100-500
            // predkosc 5-10
            Odcinek odc;
            if (Trasa.Count == 0)
                odc = new Odcinek(p1, rnd.Next(100, 500), rnd.Next(5, 10), 0);
            else
                odc = new Odcinek(p1, rnd.Next(100, 500), rnd.Next(5, 10), Trasa[Trasa.Count - 1].getKierunek());

            Trasa.Add(odc);
            var p2 = odc.getP2();
            p1 = new Punkt(p2);
        }
    }

    public override void zmien_trase()
    {
        Random rnd = new Random();
        Trasa.Clear();
        var p1 = new Punkt(pocz.getX(), pocz.getY());
        for (double i = 0; i < rnd.Next(20, 50); i++)
        {
            // wpisywanie do listy odcinkow dla samolot
            // wysokowsc 900-1200
            // predkosc 20-40
            Odcinek odc;
            if (Trasa.Count == 0)
                odc = new Odcinek(p1, rnd.Next(1000, 1500), rnd.Next(20, 40), 0);
            else
                odc = new Odcinek(p1, rnd.Next(1000, 1500), rnd.Next(20, 40), Trasa[Trasa.Count - 1].getKierunek());

            Trasa.Add(odc);
            var p2 = odc.getP2();
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
        for (double i = 0; i < rnd.Next(20, 50); i++)
        {
            // wpisywanie do listy odcinkow dla szybowiec
            // wysokowsc 100-300
            // predkosc 10-20
            Odcinek odc;
            if (Trasa.Count == 0)
                odc = new Odcinek(p1, rnd.Next(100, 300), rnd.Next(10, 20), 0);
            else
                odc = new Odcinek(p1, rnd.Next(100, 300), rnd.Next(10, 20), Trasa[Trasa.Count - 1].getKierunek());

            Trasa.Add(odc);
            var p2 = odc.getP2();
            p1 = new Punkt(p2);
        }
    }

    public override void zmien_trase()
    {
        Random rnd = new Random();
        Trasa.Clear();
        var p1 = new Punkt(pocz.getX(), pocz.getY());
        for (double i = 0; i < rnd.Next(20, 50); i++)
        {
            // wpisywanie do listy odcinkow dla samolot
            // wysokowsc 900-1200
            // predkosc 20-40
            Odcinek odc;
            if (Trasa.Count == 0)
                odc = new Odcinek(p1, rnd.Next(1000, 1500), rnd.Next(20, 40), 0);
            else
                odc = new Odcinek(p1, rnd.Next(1000, 1500), rnd.Next(20, 40), Trasa[Trasa.Count - 1].getKierunek());

            Trasa.Add(odc);
            var p2 = odc.getP2();
            p1 = new Punkt(p2);
        }
    }
}