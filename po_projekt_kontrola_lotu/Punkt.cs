using System.Collections.Generic;
using System.Windows.Media;
using System;
class Punkt
{
    private double x;
    private double y;
    public Punkt()
    {
        this.x = 0;
        this.y = 0;
    }
    public Punkt(double xx, double yy)
    {
        this.x = xx;
        this.y = yy;
    }
    public Punkt(Punkt p)
    {
        this.x = p.x;
        this.y = p.y;
    }
    public void przesun(double px, double py)
    {
        this.x += px;
        this.y += py;
    }
    public double getX()
    {
        return x;
    }
    public double getY()
    {
        return y;
    }
    public void setX(double sx)
    {
        this.x = sx;
    }
    public void setY(double sy)
    {
        this.y = sy;
    }
}