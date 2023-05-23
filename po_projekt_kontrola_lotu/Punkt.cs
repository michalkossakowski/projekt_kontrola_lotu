using System.Collections.Generic;
using System.Windows.Media;
using System;

class Punkt
{
    private int x;
    private int y;
    public Punkt()
    {
        this.x = 0;
        this.y = 0;
    }
    public Punkt(int xx, int yy)
    {
        this.x = xx;
        this.y = yy;
    }
    public Punkt(Punkt p)
    {
        this.x = p.x;
        this.y = p.y;
    }
    public void przesun(int px, int py)
    {
        this.x += px;
        this.y += py;
    }
    public override string ToString()
    {
        return "(" + this.x + "," + this.y + ")";
    }
    public int getX()
    {
        return x;
    }
    public int getY()
    {
        return y;
    }

    public void setX(int sx)
    {
        this.x = sx;
    }
    public void setY(int sy)
    {
        this.y = sy;
    }
}