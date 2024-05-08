internal class Program
{
    private static void Main(string[] args)
    {
        ColoredItem<Sword> blueSword = new ColoredItem<Sword>(new Sword(), ConsoleColor.Blue);
        ColoredItem<Bow> redBow = new ColoredItem<Bow>(new Bow(), ConsoleColor.Red);
        ColoredItem<Axe> greenAxe = new ColoredItem<Axe>(new Axe(), ConsoleColor.Green);

        blueSword.Display();
        redBow.Display();
        greenAxe.Display();

    }
}

public class Sword { }
public class Bow { }
public class Axe { }

public class ColoredItem<T> where T : class 
{
    public T Item { get; set; }
    ConsoleColor Color { get; set; }
    public ColoredItem(T item, ConsoleColor color)
    {
        Item = item;
        Color = color;
    }

    public void Display() {
        Console.ForegroundColor = Color;
        Console.WriteLine(Item.ToString());
    }

}