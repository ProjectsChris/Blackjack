using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackLib.SubClass_Person
{
    public class Player : Person
    {
        // Proprietà
        private double _bet;
        public String Name { get; }
        public double Balance { get; set; }
        public double Bet { get => _bet; set => makeBet(_bet); }
        public bool IsBot { get; }
        public bool IsOut { get; internal set; }
        //public bool IsOut { get => Balance < 0.10 ? true : false; }

        // Costruttori
        public Player(String name, double balance, bool isBot = false)
        {
            this.Name = name;
            this.Balance = balance;
            this.IsBot = isBot;
        }

        // Metodi
        private double makeBet(double bet)
        {
            if (Balance < 0.10)
                IsOut = true;
            else
                IsOut = false;

            if (bet < 0.10 || bet > Balance)
                throw new Exception($"Bet of player {Name} not valid");


            Balance -= bet;
            return bet;
        }
        public override string ToString()
        {
            String playerString = "";

            playerString += $"Name: {Name}";

            if (IsBot)
                playerString += "[bot]";

            playerString += $" | Balance: {Balance}$";

            return playerString;
        }
    }
}
