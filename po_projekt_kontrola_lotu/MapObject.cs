using System.Collections.Generic;
using System.Windows.Media;
using System;

class MapObject
{
    private Punkt p;
    private int a;
    private int b;
    public MapObject(Punkt p, int a, int b)
    {
        this.p = new Punkt(p);
        this.a = a; 
        this.b = b;
    }
}