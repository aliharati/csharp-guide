using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Games
{
    internal class TicTacToe
    {
        public Player Player1 {  get; set; }
        public Player Player2 { get; set; }
        private Player turnPlayer;
        public int Turn {  get; private set; }
        public Board? GameBoard { get; set; }


        public void StartGame()
        {
            GameBoard = new Board();
            string p1Name = Player.Choose("Player 1 What is your name? ");
            Sign p1Sign = Player.ConvertChoiceToSign(Player.Choose("Player 1 Choose your Sign(X\\O): "));
            string p2Name = Player.Choose("Player 2 What is your name? ");
            Sign p2Sign = Player.ConvertChoiceToSign(Player.Choose("Player 2 Choose your Sign(X\\O): "));
            Player1 = new Player(p1Name, p1Sign);
            Player2 = new Player(p2Name, p2Sign);
            Turn = 1;
            bool finish = false;
            while (!finish && Turn <=9)
            {   

                PlayRound();
                Turn++;
                finish = GameBoard.CheckWin();
            }
            if (Turn == 10)
                Console.WriteLine($"It's a Draw!");
            else
                Console.WriteLine($"{turnPlayer.Name} Won!");
            
            Console.WriteLine(GameBoard.ToString());
        }

        private void PlayRound()
        {
            Console.Clear();

            if (Turn%2==0) turnPlayer = Player2;
            else turnPlayer = Player1;
            Console.WriteLine($"It's {turnPlayer.Name}'s turn");
            Console.WriteLine(GameBoard.ToString());

            bool correctChoice = false;
            while (!correctChoice)
            {
                (int X, int Y) spot = turnPlayer.ChoosePosition();
                correctChoice= GameBoard.AssignPosition(spot.X, spot.Y, turnPlayer.Sign);
            }
            
        }
        
    }

    internal class Board
    {
        public char[,] Spots { get; private set; }
        public Board()
        {
            Spots = new char[3, 3];
            ResetBoard();
            
        }
        public bool AssignPosition(int x, int y,Sign sign)
        {
            if (Spots[x, y] != ' ')
            {
                Console.WriteLine($"Spot is already Occupied by {Spots[x, y]}");
                return false;
            }
            Spots[x,y] = Player.ConvertSignToChar(sign);
            return true;
        }

        public void ResetBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Spots[i, j] = ' ';
                }
            }
        }

        public bool CheckWin()
        {
            for (int i = 0;i < 3; i++)
            {
                if (Spots[i,0] == Spots[i,1] && Spots[i,0] == Spots[i,2] && Spots[i,0] != ' '){
                    return true;
                }
                if (Spots[0, i] == Spots[1, i] && Spots[0, i] == Spots[2, i] && Spots[0, i] != ' ')
                {
                    return true;
                }

            }
            if (Spots[0, 0] == Spots[1, 1] && Spots[1, 1] == Spots[2, 2] && Spots[0, 0] != ' ')
            {
                return true;
            }
            if (Spots[0, 2] == Spots[1, 1] && Spots[1, 1] == Spots[2, 0] && Spots[0, 2] != ' ')
            {
                return true;
            }
            return false;
        } 



        public override string ToString()
        {
            return $" {Spots[0, 0]} | {Spots[0, 1]} | {Spots[0, 2]} \n---+---+---\n {Spots[1, 0]} | {Spots[1, 1]} | {Spots[1, 2]} \n---+---+---\n {Spots[2, 0]} | {Spots[2, 1]} | {Spots[2, 2]} ";
        }
    }
    internal class Player
    {
        public string Name { get; private set; }
        public Sign Sign { get; private set; }
        public Player(string name, Sign sign) 
        {
            Name = name;
            Sign = sign;    
        }
        public static string Choose(String text) {
            Console.Write(text);
            return Console.ReadLine();
        }

        public static Sign ConvertChoiceToSign(String text) {
            return text switch
            {
                "X" => Sign.X,
                "O" => Sign.O
            };
        }
        public static char ConvertSignToChar(Sign sign)
        {
            return sign switch
            {
                 Sign.X=> 'X',
                 Sign.O=> 'O',
            };
        }
        public (int, int) ChoosePosition()
        {
            int position = Convert.ToInt32(Choose($"{Name} choose your next position(1-9): "))-1;
            int x = position/3;
            int y = position%3;
            return (x, y);
        }
    }

}
enum Sign { X, O }