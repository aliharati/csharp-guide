using System.Reflection.Metadata.Ecma335;
using System.Xml;

internal class Program
{
    private static void Main(string[] args)
    {
        Point p1 = new Point(2,3);
        Point p2 = new Point(-4,0);
        Console.WriteLine(p1);
        Console.WriteLine(p2);
        Console.WriteLine(Point.Origin);

        Color random = new Color(24, 56, 124);
        Color yellow = Color.Yellow();
        Console.WriteLine($"Random Color: {random}");
        Console.WriteLine($"Yelow: {yellow}");
    }
}
class Point
{
    public static Point Origin { get; } = new();
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;  
    }
    public Point(): this(0, 0) { }

    public override string ToString()
    {
        return "x: " +  X + " y: " + Y;
    }
}

class Color
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }

    public Color(int r, int g, int b)
    {
        R = Convert.ToByte(r);
        G = Convert.ToByte(g);
        B = Convert.ToByte(b);
    }
    public Color(): this(0,0,0) { }

    public override string ToString()
    {
        return $"Red: {R}| Green: {G}| Blue: {B}";
    }
    public static Color White() => new(255, 255, 255);
    public static Color Black() => new(0, 0, 0);
    public static Color Red() => new(255, 0, 0);
    public static Color Orange() => new(255, 163, 0);
    public static Color Yellow() => new(255, 255, 0);
    public static Color Green() => new(0, 128, 0);
    public static Color Blue() => new(0, 0, 255);
    public static Color Purple => new(128, 0, 128);

}