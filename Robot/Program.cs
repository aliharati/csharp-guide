using System.Reflection.Metadata.Ecma335;

internal class Program
{
    private static void Main(string[] args)
    {
        //testRobot();
        Sword OGSword = new Sword(Material.Iron, null,27.5,10);
        Console.WriteLine(OGSword);
        Sword techSword = OGSword with { SwordMaterial = Material.Binarium, SwordGem= Gem.Bistone };
        Console.WriteLine(techSword);
        Sword KingSword = OGSword with
        {
            SwordGem = Gem.Diamond,
            SwordMaterial = Material.Steel,
            Length = 47.5,
            CrossGuardWidth = 25
        };
        Console.WriteLine(KingSword);

    }
    public static void testRobot()
    {
        Robot robot = new Robot();
        Console.WriteLine("Please choose from the commands below to control the Robot:\non\noff\nnorth\nsouth\neast\nwest");
        for (int i = 0; i < 3; i++)
        {
            Console.Write("Command: ");
            string command = Console.ReadLine();
            robot.Commands[i] = returnCommand(command);
        }
        robot.Run();
    }
    public static IRobotCommand returnCommand(string command)
    {
        return command switch 
        {
            "on" => new PowerON(),
            "off" => new PowerOff(),
            "north" => new MoveNorth(),
            "south" => new MoveSouth(),
            "east" => new MoveEast(),
            "west" => new MoveWest(),
        };
    }
}

public class Robot
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsPowered { get; set; }
    public IRobotCommand?[] Commands { get; } = new IRobotCommand?[3];
    public void Run()
    {
        foreach (IRobotCommand? command in Commands)
        {
            command?.Run(this);
            Console.WriteLine($"[{X} {Y} {IsPowered}]");
        }
    }
}

public interface IRobotCommand
{
    public void Run(Robot robot);
}

public class MoveNorth: IRobotCommand
{
    public void Run(Robot robot)
    {
        if(!robot.IsPowered) return;
        robot.Y += 1;
    }
}
public class MoveSouth : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (!robot.IsPowered) return;
        robot.Y -= 1;
    }
}
public class MoveEast : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (!robot.IsPowered) return;
        robot.X += 1;
    }
}
public class MoveWest : IRobotCommand
{
    public void Run(Robot robot)
    {
        if (!robot.IsPowered) return;
        robot.X -= 1;
    }
}
public class PowerON : IRobotCommand
{
    public void Run(Robot robot)
    {
        robot.IsPowered = true;
    }
}
public class PowerOff : IRobotCommand
{
    public void Run(Robot robot)
    {
        robot.IsPowered = false;
    }
}

record Sword(Material SwordMaterial, Gem? SwordGem, double Length, double CrossGuardWidth);


enum Material { Wood, Bronze, Iron, Steel, Binarium }
enum Gem { Emerald, Amber, Sapphire, Diamond, Bistone}