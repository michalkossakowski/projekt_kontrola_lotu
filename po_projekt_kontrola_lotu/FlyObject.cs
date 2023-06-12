using System.Collections.Generic;
using System.Windows.Media;
using System;
public abstract class FlyObject
{
    protected int id;
    protected Punkt pocz;
    protected List<Odcinek> Trasa;
    protected Brush brush1;
    protected double biezaca_wysokosc;

    public FlyObject(double x, double y,int id)
    {
        this.id = id;
        this.pocz = new Punkt(x, y);
        Trasa = new List<Odcinek>();
    }
    public virtual int getId()
    {
        return id;
    }
    public virtual double getBiezWys()
    {
        return biezaca_wysokosc;
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
        biezaca_wysokosc = odc.getWysokosc();
    }

    // zmiana trasy 
    public abstract void zmien_trase();
}