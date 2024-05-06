using System.Reflection.Metadata.Ecma335;

internal class Program
{
    private static void Main(string[] args)
    {
        Robot robot = new Robot();
        Console.WriteLine("Please choose from the commands below to control the Robot:\non\noff\nnorth\nsouth\neast\nwest");
        for (int i = 0; i < 3; i++) {
            Console.Write("Command: ");
            string command = Console.ReadLine();
            robot.Commands[i] = returnCommand(command);
        }
        robot.Run();


    }

    public static RobotCommand returnCommand(string command)
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
    public RobotCommand?[] Commands { get; } = new RobotCommand?[3];
    public void Run()
    {
        foreach (RobotCommand? command in Commands)
        {
            command?.Run(this);
            Console.WriteLine($"[{X} {Y} {IsPowered}]");
        }
    }
}

public abstract class RobotCommand
{
    public abstract void Run(Robot robot);
}

public class MoveNorth: RobotCommand
{
    public override void Run(Robot robot)
    {
        if(!robot.IsPowered) return;
        robot.Y += 1;
    }
}
public class MoveSouth : RobotCommand
{
    public override void Run(Robot robot)
    {
        if (!robot.IsPowered) return;
        robot.Y -= 1;
    }
}
public class MoveEast : RobotCommand
{
    public override void Run(Robot robot)
    {
        if (!robot.IsPowered) return;
        robot.X += 1;
    }
}
public class MoveWest : RobotCommand
{
    public override void Run(Robot robot)
    {
        if (!robot.IsPowered) return;
        robot.X -= 1;
    }
}
public class PowerON : RobotCommand
{
    public override void Run(Robot robot)
    {
        robot.IsPowered = true;
    }
}
public class PowerOff : RobotCommand
{
    public override void Run(Robot robot)
    {
        robot.IsPowered = false;
    }
}