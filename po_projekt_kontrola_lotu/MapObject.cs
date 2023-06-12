using System;

public class MapObject
{
    private Punkt p;
    private int a;
    private int b;
    public MapObject(Punkt p)
    {
        Random rnd = new Random();

        this.p = new Punkt(p);
        this.a = rnd.Next(10, 51);
        this.b = rnd.Next(10, 51);
    }
    public int getA()
    {
        return a;
    }
    public int getB()
    {
        return b;
    }
    public Punkt GetPunkt()
    {
        return p;
    }

}
