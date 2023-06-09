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
    public double getKierunek()
    {
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