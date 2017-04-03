using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    class Program
    {

        static void Main(string[] args)
        {
            List<Card> playdeck = new List<Card>();
            List<Card> playerhand = new List<Card>();
            List<Card> dealerhand = new List<Card>();

            playdeck = Card.Shuffle();
            string choice;

            int playercurrent, dealercurrent, count = 1, dealcount = 1;
            bool keepgoing = true;


            playerhand.Add(Card.Deal(playdeck));
            dealerhand.Add(Card.Deal(playdeck));
            playerhand.Add(Card.Deal(playdeck));
            dealerhand.Add(Card.Deal(playdeck));

            Console.WriteLine($"Dealer shows {dealerhand[0]}");
            Console.Write($"Player shows {playerhand[0]} & {playerhand[1]}\n");




            do
            {

                playercurrent = playerhand.Sum(card => card.GetCardValue());

                if (playercurrent > 21)
                {
                    Console.WriteLine($"Your cards total to {playercurrent} which means you lose the game.");
                    Environment.Exit(0);
                }else if (playercurrent == 21){
                    Console.WriteLine($"Your cards total to {playercurrent} which means you win the game!");
                    Environment.Exit(0);
                }
                Console.Write("Would you like to Hit or Stay?");
                choice = Console.ReadLine();

                if (choice == "Hit")
                {
                    playerhand.Add(Card.Deal(playdeck));
                    count++;
                    Console.WriteLine($"You were dealt {playerhand[count]}");
                }

            } while (choice != "Stay");

            Console.WriteLine($"Dealer reveals their second card to be {dealerhand[1]}.  Dealer total is {dealerhand.Sum(card => card.GetCardValue())}");

            do
            {

                dealercurrent = dealerhand.Sum(card => card.GetCardValue());
                if (dealercurrent > 21)
                {
                    Console.WriteLine($"Dealer has busted {dealercurrent} which means you win.");
                    keepgoing = false;
                }
                else if (dealercurrent > playercurrent)
                {
                    Console.WriteLine("You lose because the value of the Dealer's hand is higher than yours.");
                    keepgoing = false;
                }
                else if (dealercurrent < 16)
                {
                    dealerhand.Add(Card.Deal(playdeck));
                    Console.WriteLine($"Dealer was dealt {dealerhand[dealcount]}");
                    dealcount++;
                    
                }
                else if (dealercurrent == 21)
                {
                    Console.WriteLine("You lose because the Dealer's hand is 21.");
                    keepgoing = false;
                }
                else if (dealercurrent >= 16 && dealercurrent < 21 && dealercurrent >= playercurrent)
                {
                    Console.WriteLine($"Dealer has {dealercurrent} and you have {playercurrent} Dealer wins.");
                    keepgoing = false;
                }
                else if (dealercurrent >= 16 && dealercurrent < 21 && dealercurrent < playercurrent)
                {
                    Console.WriteLine($"Dealer has {dealercurrent} and you have {playercurrent} You win.");
                    keepgoing = false;
                }

            } while (keepgoing);




            



        }

    }
    
    public enum Suit
    {
        Hearts,
        Clubs,
        Diamonds,
        Spades
    }

    public enum Rank
    {
        Ace,
        Deuce,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    public class Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }

        public Card(Suit s, Rank r)
        {
            this.Suit = s;
            this.Rank = r;
        }

        public int GetCardValue()
        {
            int rv = 0;
            switch (this.Rank)
            {
                case Rank.Ace:
                    rv = 11;
                    break;
                case Rank.Deuce:
                    rv = 2;
                    break;
                case Rank.Three:
                    rv = 3;
                    break;
                case Rank.Four:
                    rv = 4;
                    break;
                case Rank.Five:
                    rv = 5;
                    break;
                case Rank.Six:
                    rv = 6;
                    break;
                case Rank.Seven:
                    rv = 7;
                    break;
                case Rank.Eight:
                    rv = 8;
                    break;
                case Rank.Nine:
                    rv = 9;
                    break;
                case Rank.Ten:
                    rv = 10;
                    break;
                case Rank.Jack:
                    rv = 10;
                    break;
                case Rank.Queen:
                    rv = 10;
                    break;
                case Rank.King:
                    rv = 10;
                    break;
                default:
                    break;
            }
            return rv;
        }



        public override string ToString()
        {
            return $"The {this.Rank} of {this.Suit}";
        }


        public static List<Card> Shuffle()
        {
            var deck = new List<Card>();

            foreach (Rank r in Enum.GetValues(typeof(Rank)))
            {
                foreach (Suit s in Enum.GetValues(typeof(Suit)))
                {
                    deck.Add(new Card(s, r));
                }
            }

            var randomDeck = deck.OrderBy(card => Guid.NewGuid()).ToList();

            return randomDeck;
        }

        public static Card Deal(List<Card> tempdeck)
        {
            Card dealt = new Card(0,0);
            dealt = tempdeck[0];
            tempdeck.Remove(dealt);
            return dealt;
        }
    }



}


