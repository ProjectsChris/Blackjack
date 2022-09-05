using BlackjackLib.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackLib
{
    public class Card
    {
        // Proprietà
        public Seed Seed { get; }
        public int Value { get; }
        public int Figure { get; }

        // Costruttori
        internal Card(Seed seed, int value, int figure)
        {
            this.Seed = seed;
            this.Value = value;
            this.Figure = figure;
        }

        // Metodi
        public override String ToString()
        {
            String cardName = "";

            switch (Figure)
            {
                case 1:
                    cardName += "A";
                    break;
                case 11:
                    cardName += "J";
                    break;
                case 12:
                    cardName += "Q";
                    break;
                case 13:
                    cardName += "K";
                    break;
                default:
                    cardName += Figure;
                    break;
            }

            cardName += " ";

            switch (Seed)
            {
                case Seed.Club:
                    cardName += "Club";
                    break;
                case Seed.Diamond:
                    cardName += "Diamond";
                    break;
                case Seed.Heart:
                    cardName += "Hearth";
                    break;
                case Seed.Spade:
                    cardName += "Spade";
                    break;
            }

            return cardName;
        }
    }
}