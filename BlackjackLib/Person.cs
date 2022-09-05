using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackLib
{
    public abstract class Person
    {
        // Proprietà
        public List<Card> Cards { get; internal set; } = new();
        public int Points { get => countCards(Cards); }
        public bool IsDone { get; internal set; }
        public bool IsBusted { get => countCards(Cards) > 21 ? true : false; }
        public bool HasBlackJack { get => countCards(Cards) == 21 && Cards.Count == 2 ? true : false; }
        public bool Has21OrMore { get => countCards(Cards) >= 21 ? true : false; }

        // Metodi
        private int countCards(List<Card> cards)
        {
            int points = 0, Aces11 = 0;

            foreach (Card c in cards)
            {
                if (c.Figure == 1)
                    Aces11++;
                points += c.Value;
            }

            while (points > 21 && Aces11 > 0)
            {
                Aces11--;
                points -= 10;
            }

            return points;
        }
    }
}