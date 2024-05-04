using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games
{
    internal class Player
    {
        public string Name { get; init; }
        public int Score { get; set; }
        public Hand? Hand { get; private set; }

        public Player(string name) {
            Name = name;
            Score = 0;
        }

        public void ChooseHand(Hand? hand)
        {
            Hand = hand;
        }
    }

    internal class RPSGame
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public int Round { get; private set; }

        public RPSGame()
        {
            Player1 = new Player(AskPlayer("Player1 enter your name: "));
            Player2 = new Player(AskPlayer("Player2 enter your name: "));
            Round = 1;

        }
        public void StartGame()
        {
            while (true)
            {
                
                PrintGameStatus();
                Round++;
                PlayRound();
                if (Player1.Score==5 || Player2.Score == 5)
                {
                    if (Player1.Score == 5)
                    {
                        Console.WriteLine($"{Player1.Name} Won the Game.");
                    }
                    else if (Player2.Score == 5)
                    {
                        Console.WriteLine($"{Player2.Name} Won the Game.");
                    }
                    string anotherGame = AskPlayer("Do you want to play another Game?(Y/N)");
                    if (anotherGame == "N") return;
                    else
                    {
                        Player1.Score = 0;
                        Player2.Score = 0;
                        Round = 1;
                    }
                }
                
            }
        }
        public void PlayRound()
        {
            
            bool player1ChoiceValid = false;
            bool player2ChoiceValid = false;
            Hand? p1Choice;
            Hand? p2Choice;
            do
            {
                p1Choice = GetPlayerChoice(AskPlayer("Player 1 Choose your hand: "));
                Console.Clear();
                if (p1Choice != null) player1ChoiceValid = true;
                else Console.WriteLine("Invalid Choice");
            } while (!player1ChoiceValid);
            do
            {
                p2Choice = GetPlayerChoice(AskPlayer("Player 2 Choose your hand: "));
                Console.Clear();
                if (p2Choice != null) player2ChoiceValid = true;
                else Console.WriteLine("Invalid Choice");
            } while (!player2ChoiceValid);
            Player1.ChooseHand(p1Choice);
            Player2.ChooseHand(p2Choice);
            Result? result = CompareHands(p1Choice, p2Choice);
            if(result == Result.Win)
            {
                Player1.Score += 1;
                Console.WriteLine($"{p1Choice} beats {p2Choice}");
                Console.WriteLine($"{Player1.Name} Won!");
                return;
            }
            else if(result == Result.Lose)
            {
                Player2.Score += 1;
                Console.WriteLine($"{p2Choice} beats {p1Choice}");
                Console.WriteLine($"{Player2.Name} Won!");
                return;
            }
            Console.WriteLine($"Both Chose {p1Choice}, It's a Draw!");
            return;

        }
        public Result? CompareHands(Hand? p1Choice, Hand? p2Choice)
        {
            if (p1Choice == p2Choice) return Result.Draw;
            Result? result = null;
            switch (p1Choice){
                case Hand.Rock:
                    if (p2Choice == Hand.Paper) { result = Result.Lose; break; }
                    else { result = Result.Win; break; }
                case Hand.Paper:
                    if (p2Choice == Hand.Scissors) { result = Result.Lose; break; }
                    else { result = Result.Win; break; }
                case Hand.Scissors:
                    if (p2Choice == Hand.Rock) { result = Result.Lose; break; }
                    else { result = Result.Win; break; }
            }
            return result;
        }
        public Hand? GetPlayerChoice(string choice)
        {

            return choice switch
            {
                "rock" => Hand.Rock,
                "paper" => Hand.Paper,
                "scissors" => Hand.Scissors,
                _ => null
            };
        }
        private void PrintGameStatus()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine($"Round: {Round} | {Player1.Name}'s Score: {Player1.Score} | {Player2.Name}'s Score: {Player2.Score} ");
            Console.WriteLine("------------------------------------------");
        }
        public static string? AskPlayer(string text)
        {
            Console.Write(text);
            return Console.ReadLine();
        }
    }




    enum Hand { Rock, Paper, Scissors }
    enum Result{Win, Lose, Draw}
}
