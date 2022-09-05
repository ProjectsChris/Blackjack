using BlackjackLib;
using BlackjackLib.Enum;
using BlackjackLib.SubClass_Person;

Console.WriteLine(" - BlackJack C# -");
Console.Write("\nQuanti giocatori giocano ? ");

int nPlayers = Convert.ToInt32(Console.ReadLine());
Player[] players = new Player[nPlayers];

for (int i = 0; i < nPlayers; i++)
{
    String choice;
    double balance;
    bool isBot = false;

    Console.Write($"\nInserisci nome giocatore {i} : ");
    String name = Console.ReadLine();

    do
    {
        System.Console.Write($"Inserisci saldo : ");
        balance = Convert.ToDouble(Console.ReadLine());
    } while (balance < 0.10);

    do
    {
        System.Console.Write("Il giocatore è un bot ? (y/n) : ");
        choice = Console.ReadLine();

        if (choice.ToLower() == "y")
            isBot = true;
        else if (choice.ToLower() == "n")
            isBot = false;
    } while (choice.ToLower() != "y" && choice.ToLower() != "n");

    players[i] = new Player(name, balance, isBot);
}

BlackJackEngine engine = new(players);
engine.Initialize();

foreach (Player p in engine.Players)
{
    if (!p.IsBot)
    {
        double bet;

        do
        {
            Console.Write($"\nInserisci puntata per giocatore {p.Name} : ");
            bet = Convert.ToDouble(Console.ReadLine());

            if (bet >= 0.10)
                p.Bet = bet;

        } while (bet < 0.10);
    }
}

engine.GiveCards();

foreach (Player p in engine.Players)
    PrintStats(p);

System.Console.WriteLine("\n---------------\n");

foreach (Player p in engine.Players)
{
    PrintDealerStats(engine.Dealer);
    PrintStats(p);

    if (!p.IsOut && !p.IsDone && !p.IsBot)
    {
        while (!p.IsDone)
        {
            String choice;

            do
            {
                Console.Write($"\nGiocatore {p.Name} chiedi carta (H) o rimani (S) ? ");
                choice = Console.ReadLine();

                if (choice.ToLower() == "h")
                    engine.Move(p, Choice.Hit);
                else if (choice.ToLower() == "s")
                    engine.Move(p, Choice.Stay);

            } while (choice.ToLower() != "h" && choice.ToLower() != "s");

            PrintStats(p);

        }
    }
}

engine.Finish();
PrintDealerStats(engine.Dealer);
foreach (Player p in engine.Players)
    PrintStats(p);

static void PrintDealerStats(Dealer d)
{
    Console.WriteLine($"\nDealer points {d.Points} | cards:");
    foreach (Card c in d.Cards)
    {
        Console.Write($"- {c.ToString()} -");
    }
}

static void PrintStats(Player p)
{
    Console.WriteLine($"\nGiocatore {p.Name} | puntata : {p.Bet} | saldo : {p.Balance} | Carte :");

    foreach (Card c in p.Cards)
    {
        Console.Write($"- {c.ToString()} -");
    }

    Console.Write($" punteggio: {p.Points}\n");
}
