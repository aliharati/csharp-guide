
StartOperation();
void StartOperation()
{
    int distance = AskForNumInRange("Player 1, how far away from the city do you want to station the Manticore? ", 0, 100);
    Console.BackgroundColor = ConsoleColor.Red;
    Console.ForegroundColor = ConsoleColor.Black;
    Console.Clear();
    int cityHealth = 15;
    int manticoreHealth = 10;
    int round = 1;
    while (cityHealth > 0 && manticoreHealth > 0)
    {
        PrintRoundInfo(round, cityHealth, manticoreHealth);
        int damage = Attack(distance, round);
        manticoreHealth -= damage;
        if (manticoreHealth <= 0)
        {
            Console.WriteLine("The Manticore has been destroyed! The city of Consolas has been saved!");
            return;
        }
        cityHealth--;
        round++;

    }
    Console.WriteLine("The city of Consolas has been destroyed!");
    return;

}
int AskForNumInRange(string text, int min, int max)
{
    int num;
    while (true)
    {
        num = AskForNum(text);
        if (num < min || num > max)
        {
            Console.WriteLine("distance in not in range!");
        }
        else break;
    }
    return num;
}
int AskForNum(string text)
{

    Console.Write(text);
    return Convert.ToInt32(Console.ReadLine());
}

int Attack(int distance, int round)
{
    int attackLocation = AskForNum("Enter desired cannon range: ");
    if (attackLocation == distance)
    {
        Console.WriteLine("That round was a DIRECT HIT!");
        return CalculateDamage(round);
    }
    else if (attackLocation < distance) Console.WriteLine("That round FELL SHORT of the target.");
    else Console.WriteLine("That round OVERSHOT the target.");

    return 0;
}

void PrintRoundInfo(int round, int city, int manticore)
{
    Console.WriteLine("-----------------------------------------------------------");
    Console.WriteLine($"STATUS: Round: {round} City: {city}/15 Manticore: {manticore}/10");
    Console.WriteLine($"The cannon is expected to deal {CalculateDamage(round)} damage this round.");
}
int CalculateDamage(int round)
{
    if (round % 3 == 0 && round % 5 == 0) return 10;
    else if (round % 3 == 0 || round % 5 == 0) return 3;
    return 1;
}
