﻿internal class Program
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
        Console.WriteLine($"Yellow: {yellow}");


        Card[] cards = new Card[56];
        int s = 0;
        for (int i=1; i < 5; i++){
            for (int j = 1; j < 15; j++)
            {
                Card card = new(getColor(i), getRank(j));
                cards[s++] = card;
                //Console.WriteLine(card);

            }
        }
        //TestDoor();
        TestPasswordValidator();
        
        
        
    }
    public static void TestPasswordValidator()
    {
        bool validPass;
        do
        {
            string newPass = AskUserText("Enter new Password: ");
            validPass = PasswordValidator.isValid(newPass);
        }while (!validPass);
        Console.WriteLine("Password Accepted");
    }
    public static void TestDoor()
    {
        string pass = AskUserText("Choose a pass for the door: ");
        Door door = new(pass);
        bool continueRun = true;
        while (continueRun)
        {
            int decision = AskUserNum("Choose what to do with the door:\n1.Open\n2.Close\n3.Unlock\n4.Lock\n5.Change Pass\n6.Quit\n");
            switch (decision)
            {
                case 1:
                    door.Open(); break;
                case 2:
                    door.Close(); break;
                case 3:
                    door.Unlock(AskUserText("Enter pass: ")); break;
                case 4:
                    door.Lock(); break;
                case 5:
                    string oldPass = AskUserText("Enter old pass: ");
                    string newPass = AskUserText("Enter new pass:");
                    door.ChangePassword(oldPass, newPass);
                    break;
                case 6:
                    continueRun = false; break;
                default:
                    break;
            }
        }
    }
    public static CardColor getColor(int color)
    {
        return color switch
        {
            1 => CardColor.Red,
            2 => CardColor.Green,
            3 => CardColor.Blue,
            4 => CardColor.Yellow
        };
    }

    public static string AskUserText(string text)
    {
        Console.Write(text);
        return Console.ReadLine();
    }
    public static int AskUserNum(string text)
    {
        Console.Write(text);
        return Convert.ToInt32(Console.ReadLine());
    }
    public static CardRank getRank(int rank)
    {
        return rank switch
        {
            1 => CardRank.One,
            2 => CardRank.Two,
            3 => CardRank.Three,
            4 => CardRank.Four,
            5 => CardRank.Five,
            6 => CardRank.Six,
            7 => CardRank.Seven,
            8 => CardRank.Eight,
            9 => CardRank.Nine,
            10 => CardRank.Ten,
            11 => CardRank.Currency,
            12 => CardRank.Caret,
            13 => CardRank.Percent,
            14 => CardRank.Ampersand
            
        };
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

    public Color(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
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

class Card
{
    
    public CardColor Color { get; }
    public CardRank Rank { get; }
    public Card(CardColor color, CardRank rank) {
        Color = color;
        Rank = rank;
    }

    public override string ToString()
    {
        return $"The {Color} {Rank}";
    }

    public bool IsSymbol => Rank == CardRank.Ampersand || Rank == CardRank.Caret || Rank == CardRank.Currency || Rank == CardRank.Percent;
    public bool IsNumber => !IsSymbol;
}

class Door
{
    public DoorState State { get; private set; }
    private string Passcode {  get; set; }
    public Door(string passcode)
    {
        Passcode = passcode;
        State = DoorState.Locked;
    }

    public void ChangePassword(string currentPass, string newPass)
    {
        if (CheckPass(currentPass)) Passcode = newPass;
        else Console.WriteLine("Wrong Password");
    }
    public void Unlock(string pass)
    {
        if (!CheckPass(pass))
        {
            Console.WriteLine("Wrong PassWord");
            return;
        }
        if(State != DoorState.Locked) 
        {
            Console.WriteLine("Door already unlocked");
            return;
        }
        State = DoorState.Closed;
    }
    public void Open()
    {
        if(State != DoorState.Closed)
        {
            Console.WriteLine("Door needs to be closed and unlocked to open!");
            return;
        }
        State = DoorState.Open;
    }
    public void Close()
    {
        if( State != DoorState.Open)
        {
            Console.WriteLine("Door need to be open to close!");
            return;
        }  
        State = DoorState.Closed;
    }
    public void Lock() { 
        if (State != DoorState.Closed)
        {
            Console.WriteLine("Door needs to be closed to be locked!");
            return;
        }
        State = DoorState.Locked;
    }

    public bool CheckPass(string pass)
    {
        return pass == Passcode;
    }

}

class PasswordValidator
{
    public static int LengthMin { get; private set; } = 6;
    public static int LengthMax { get; private set; } = 13;
    public static int Uppercases { get; private set; } = 1;
    public static int Lowercases { get; private set; } = 1;
    public static int Numbers { get; private set; } = 1;
    public static char[] Forbidden { get; private set; } = new char[] { 'T', '&' };

    public static bool isValid(string pass)
    {
        if (pass == null) return false;
        bool validLength = pass.Length < LengthMin && pass.Length > LengthMax ? true : false;

        bool validUppercase = false;
        bool validLowercase = false;
        bool validNumbers = false;
        bool containsForbidden = false;
        foreach (char c in pass)
        {
            if (char.IsUpper(c)) validUppercase = true;
            if (char.IsLower(c)) validLowercase = true;
            if (char.IsDigit(c)) validNumbers = true;
            if (Forbidden.Contains(c)) containsForbidden = true;
        }
        return validLength && validUppercase && validLowercase && validNumbers && !containsForbidden;
    }
}
enum DoorState { Open, Closed, Locked}
enum CardColor { Red, Green, Blue, Yellow }
enum CardRank { One, Two, Three, Four, Five, Six, Seven, Eight, Nine ,Ten, Currency, Percent, Caret, Ampersand }