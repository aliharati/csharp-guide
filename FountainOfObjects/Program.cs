using System.Data;

internal class Program
{
    private static void Main(string[] args)
    {
        CavernExplorer explorer = new CavernExplorer();
        explorer.StartGame();
    }
}

// class, room children unique rooms(monster, pitfall, fountain, entrance)
// class player
// class cavern contains matrix of rooms
// player command interface
// class cavernExplorer

// TODO replace x with y in commands, typecast rooms for display
public class CavernExplorer
{
    public Cavern Cavern { get; set; }
    public Player Player { get; set; }
    public Coordinate LastCoordinate { get; set; } = new Coordinate(0,0);
    public int Turn { get; set; } = 0;

    public CavernExplorer()
    {
        
        string name = Player.getCommand("Please Enter your Name: ");
        string gameSize = Player.getCommand("Would you like to play a small, medium or large game?\n1.Small\n2.Medium\n3.Large\n");
        switch (gameSize.ToLower())
        {
            case "1":
            case "small":
                Cavern = new Cavern(4, 4);
                break;
            case "2":
            case "medium":
                Cavern = new Cavern(6, 6);
                break;
            case "3":
            case "large":
                Cavern = new Cavern(8, 8);
                break;
            default:
                Cavern = new Cavern(4, 4);
                break;

        }
        
        Player = new Player(name,Cavern.Entrance, Cavern.GetRoom(new Coordinate(0,0)));
        
    }

    public void StartGame()
    {   


        while (true)
        {
            Console.Clear();
            if (Turn>0 && Player.Coordinate.Equals(LastCoordinate)) Console.WriteLine("Path is blocked!");
            LastCoordinate = new Coordinate(Player.Coordinate);
            Console.WriteLine(Player.CurrentRoom);
            string playerCommand = Player.getCommand("What do you want to do? ");
            Player.Run(StringToCommand(playerCommand));
            Player.CurrentRoom = Cavern.GetRoom(Player.Coordinate);        
            if (Player.CurrentRoom.GetType() == typeof(EntranceRoom) && Cavern.FountainRoom.FountainOn )
            {
                Console.WriteLine("The Fountain of Objects has been reactivated, and you have escaped with your life!\r\nYou win!");
                return;
            }
            Turn++;
            

        }
    }


    public ICommand? StringToCommand(string command)
    {
        return command switch
        {
            "move north" => new MoveNorth(),
            "move south" => new MoveSouth(),
            "move east" => new MoveEast(),
            "move west" => new MoveWest(),
            "enable fountain" => new EnableFountain(),
            _ => null
        };
    }

    

}
public class Player
{
    public string Name { get; init; }
    public Coordinate Coordinate { get; set;}
    public Room CurrentRoom { get; set; }


    public Player(string name, Coordinate coordinate, Room currentRoom)
    {
        Name = name;
        Coordinate = coordinate;
        CurrentRoom = currentRoom;  
    }

    public void Run(ICommand? command)
    {
        if(command == null)
        {
            Console.WriteLine("Not a valid Comand");
            return;
        }
        command.Run(this);
    }
    public static string getCommand(string text)
    {
        string? answer = null;
        while(answer == null)
        {
            Console.Write(text);
            answer = Console.ReadLine();
            if (answer == null)
            {
                Console.Clear();
                Console.WriteLine("Enter a valid answer");
            }
        }
        return answer;
    }
}

public class Coordinate
{
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;

    public Coordinate() { }
    public Coordinate(int x, int y) {  X = x; Y = y; }
    public Coordinate(Coordinate coord) { X = coord.X; Y = coord.Y; }

    public static Coordinate GenerateRandom(int rowLimit, int colLimit)
    {
        Random rand = new Random();
        int start = 0;
        int row = rand.Next(rowLimit);
        if (row == 0) start = 1;
        int col = rand.Next(start, colLimit);
        return new Coordinate(row, col);

    }

    public override string ToString()
    {
        return $"(Row={X}, Column={Y})";
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (obj.GetType() != typeof(Coordinate)) return false;
        Coordinate coord = (Coordinate)obj;
        return coord.X == X && coord.Y == Y ;
    }

}

public interface ICommand
{
    public void Run(Player player);
}

public class MoveNorth: ICommand
{
    public void Run(Player player)
    {
        player.Coordinate.X += 1;
    }
}
public class MoveSouth : ICommand
{
    public void Run(Player player)
    {       
        player.Coordinate.X -= 1;
    }
}
public class MoveEast : ICommand
{
    public void Run(Player player)
    {
        player.Coordinate.Y += 1;
    }
}
public class MoveWest : ICommand
{
    public void Run(Player player)
    {
        player.Coordinate.Y -= 1;
    }
}
public class EnableFountain : ICommand
{
    public void Run(Player player)
    {
        if (player.CurrentRoom.GetType() == typeof(FountainRoom))
        {
            FountainRoom room = (FountainRoom)player.CurrentRoom;
            room.TurnFountainOn();
        }
        else
        {
            Console.WriteLine("There are no fountains in this room!");
        }
    }
}

public class Room
{
    public Coordinate Coordinate { get; init; }

    public Room(Coordinate coord)
    {
        Coordinate = coord;
    }
    
    public override string ToString()
    {
        return $"You are in the room at {Coordinate}";
        
    }

}

public class FountainRoom : Room 
{
    
    public bool FountainOn {  get; private set; }
    public string RoomMessage {  get; private set; }

    public FountainRoom(Coordinate coord) : base(coord) { 
        FountainOn = false;
        RoomMessage = "\nYou hear water dripping in this room. The Fountain of Objects is here!";
    }

    public void TurnFountainOn()
    {
        if (FountainOn) {
            Console.WriteLine("Fountain is already on!");
            return;
              
        }
        FountainOn = true;
        RoomMessage = "\nYou hear the rushing waters from the Fountain of Objects. It has been reactivated!";
    }
    public void TurnFountainOFF()
    {
        if (!FountainOn)
        {
            Console.WriteLine("Fountain is already off!");
            return;

        }
        FountainOn = false;
        RoomMessage = "\nYou hear water dripping in this room. The Fountain of Objects has been deactivated!";
    }
    public override string ToString()
    {
        return base.ToString()+ RoomMessage;
    }

}

public class EntranceRoom : Room
{
    public EntranceRoom() : base(new Coordinate(0,0)) { }

    public override string ToString()
    {
        string message = base.ToString() + "\nYou see light coming from the cavern entrance.";
        return message;
    }
}

public class Cavern
{
    public Room[,] Rooms { get; init; }
    public Coordinate FountainCoordinate { get; set; }
    public Coordinate Entrance {  get; set; }
    public int Rows { get; init; }
    public int Columns { get; init; }
    public FountainRoom? FountainRoom { get; set; }

    public Cavern(int rows, int columns)
    {
        FountainCoordinate = Coordinate.GenerateRandom(rows,columns);
        Entrance = new Coordinate(0,0);
        Rooms = new Room[rows ,columns];
        Rows = rows;
        Columns = columns;
        PopulateCavern();
        
    }
    public Room GetRoom(Coordinate coord)
    {
        if (coord.Y < 0) coord.Y += 1;
        if (coord.X < 0) coord.X += 1;
        if (coord.Y>=Columns) coord.Y -= 1;
        if (coord.X>=Rows) coord.X -= 1;
        return Rooms[coord.X,coord.Y];
    }
    public void PopulateCavern()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Coordinate newCoordinate = new Coordinate(i,j);
                if (FountainCoordinate.Equals(newCoordinate))
                {
                    Console.WriteLine("fountainRoom added");
                    FountainRoom = new FountainRoom(FountainCoordinate);
                    Rooms[i, j] = FountainRoom;
                }   
                else if (Entrance.Equals(newCoordinate))
                    Rooms[i, j] = new EntranceRoom();
                else
                    Rooms[i, j] = new Room(newCoordinate);
            }
        }
    }
    
}