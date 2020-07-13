using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualWar
{
    class Player
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Property of all players is their half of the deck they will play with
        public Queue<Card> playerDeck { get; set; }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Method used to take in a shuffled list of cards (the starting deck) and then split it up
        // between the two players in the game of war to fill their individual decks
        public static Tuple<Queue<Card>, Queue<Card>> fillPlayerDecks(List<Card> shuffledDeck)
        {
            Queue<Card> player1Deck = new Queue<Card>();
            Queue<Card> player2Deck = new Queue<Card>();

            for (int z = 0; z < 26; z++)
            {
                player1Deck.Enqueue(shuffledDeck[z]);
                player2Deck.Enqueue(shuffledDeck[51 - z]);
            }

            return Tuple.Create(player1Deck, player2Deck);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Used in debugging but no longer in use in final product

        /*        public static void printPlayerDecks(Player player1, Player player2)
                {
                    Console.WriteLine("player 1 deck is below");

                    foreach (Card card in player1.playerDeck)
                    {
                        Console.WriteLine(card.cardName + " of " + card.cardSuit + " value of " + card.cardValue);
                    }
                    Console.WriteLine("");
                    Console.WriteLine("");

                    Console.WriteLine("player 2 deck is below");

                    foreach (Card card in player2.playerDeck)
                    {
                        Console.WriteLine(card.cardName + " of " + card.cardSuit + " value of " + card.cardValue);
                    }

                    Console.WriteLine("");
                    Console.WriteLine("");
                }*/
    }
}
