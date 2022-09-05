using BlackjackLib.Enum;
using BlackjackLib.SubClass_Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackLib
{
    public class BlackJackEngine
    {
        // Proprietà
        //public double MinBet { get; }
        private List<Card> _cards = new();
        public Player[] Players { get; internal set; }
        public Dealer Dealer { get; internal set; } = new();
        public double MinBet { get; set; }

        // Costruttori
        public BlackJackEngine(Player[] players, double minBet = 0.10)
        {
            this.MinBet = minBet;
            Players = new Player[players.Count()];

            for (int i = 0; i < players.Count(); i++)
                Players[i] = players[i];
        }

        // Metodi
        public void Initialize()
        {
            _cards = new();
            Dealer = new();

            AddDecksAndShuffle(2);
        }
        public void GiveCards()
        {
            foreach (Player p in Players)
            {
                if (!p.IsOut)
                {
                    if (p.IsBot)
                    {
                        double bet = p.Balance / 100;

                        if (bet < MinBet) bet = MinBet;
                        else if (bet > 10_000) bet = 10_000;

                        p.Bet = bet;
                    }
                    else if (p.Bet < MinBet || p.Bet > p.Balance)
                        throw new Exception($"Bet of player {p.Name} not valid");

                    p.Balance -= p.Bet;
                }
            }

            moveCardsFromTo(_cards, Dealer.Cards);

            foreach (Player p in Players)
            {
                moveCardsFromTo(_cards, p.Cards, 2);

                if (p.Points == 21)
                    p.IsDone = true;
            }
        }

        public void Move(Player p, Choice c)
        {
            if (!Players.Contains(p) || p.IsOut)
                throw new Exception($"Player {p.Name} not in game");
            if (p.IsDone || p.Has21OrMore)
                throw new Exception($"Player {p.Name} is done");


            if (c == Choice.Hit)
            {
                moveCardsFromTo(_cards, p.Cards);

                if (p.Has21OrMore)
                    p.IsDone = true;
            }
            if (c == Choice.Stay)
                p.IsDone = true;
        }

        public void Finish()
        {
            foreach (Player p in Players)
            {
                if (!p.IsOut && !p.IsDone)
                    throw new Exception($"Players not done");
            }

            while (Dealer.Points < 17)
                moveCardsFromTo(_cards, Dealer.Cards);

            foreach (Player p in Players)
            {
                if (!p.IsBusted)
                {
                    if (Dealer.IsBusted)
                        p.Balance += p.Bet * 2;
                    else
                    {
                        if (p.Points > Dealer.Points)
                            p.Balance += p.Bet * 2;
                        else if (p.Points == Dealer.Points)
                            p.Balance += p.Bet;
                        else if (p.HasBlackJack)
                            p.Balance += p.Bet * 1.5;
                    }
                }
            }
        }
        private void moveCardsFromTo(List<Card> cards1, List<Card> cards2, int cards = 1)
        {
            for (int i = 0; i < cards; i++)
            {
                Card c = cards1.First();
                cards1.Remove(c);
                cards2.Add(c);
            }
        }
        public void AddDecksAndShuffle(int decks = 1)
        {
            for (int i = 0; i < decks; i++)
            {
                for (int j = 0; j < 4; j++)
                    for (int k = 1; k <= 13; k++)
                        _cards.Add(new Card((Seed)j, k == 1 ? 11 : (k > 10 ? 10 : k), k));
            }

            Shuffle();
        }
        public void Shuffle()
        {
            _cards = _cards.OrderBy(card => Guid.NewGuid()).ToList();
        }
    }
}
