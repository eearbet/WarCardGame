using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;


namespace VisualWar
{
    class Gameplay
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Method used to take both players, the decks of these players are attached to the players themselves
        // weather the war is a repeat war or not, which changes the way a war opperates, and the previous common pile
        // which is used to keep track of repeat wars
        // returns 3 possible integers, 0 for no player lost due to the war
        // 1 for player 1 lost ( ran out of cards) and 2 for player 2
        public static int executeWar(Player player1, Player player2, bool repeatWar = false, Queue<Card> oldPile = null)
        {
            // create a common pile (the pile that's in the middle of the table during a war) 
            // to hold the cards that have not yet been decided where to go
            Queue<Card> commonPile = new Queue<Card>();

            if (repeatWar)
            {
                // update the common pile to get the common pile from the previous war 
                // that is still being fought over
                commonPile = oldPile;


                // for this repeat war, their decks must have at least 3 cards  
                if (player1.playerDeck.Count < 3)
                {
                    // player 1 loses
                    Console.WriteLine("player 1 lost -- out of cards for the repeat war");
                    return 1;
                }
                else if (player2.playerDeck.Count < 3)
                {
                    // player 2 loses
                    Console.WriteLine("player 2 lost -- out of cards for the repeat war");
                    return 2;
                }
                // Else both players have enough cards to complete the repeat war
                else
                {
                    // get the cards off the top of their decks down to the third card which is what the 
                    // battle for the common pile will be over
                    for (int x = 0; x < 2; x++)
                    {
                        commonPile.Enqueue(player1.playerDeck.Dequeue());
                        commonPile.Enqueue(player2.playerDeck.Dequeue());
                    }

                    // if these now match again, then we have yet another repeat war to go to
                    if (player1.playerDeck.Peek().cardValue == player2.playerDeck.Peek().cardValue)
                    {

                        Console.WriteLine("Repeat War from a repeat war unlucky");
                        executeWar(player1, player2, true, commonPile);
                    }
                    // Else if player 1 wins the war
                    else if (player1.playerDeck.Peek().cardValue > player2.playerDeck.Peek().cardValue)
                    {

                        Console.WriteLine("War Ended with Player 1 winning by " + player1.playerDeck.Peek().cardName + " of " + player1.playerDeck.Peek().cardSuit + " over the " + player2.playerDeck.Peek().cardName + " of " + player2.playerDeck.Peek().cardSuit);

                        commonPile.Enqueue(player1.playerDeck.Dequeue());
                        commonPile.Enqueue(player2.playerDeck.Dequeue());


                        // List off all cards in the common pile to show the complete list of winnings in CLI
                        // and award the cards in the common pile to the winning player
                        Console.WriteLine("Total winnings were");
                        foreach (Card card in commonPile)
                        {
                            Console.WriteLine(card.cardName + " of " + card.cardSuit);
                            //player1.playerDeck.Enqueue(commonPile.Dequeue());
                            player1.playerDeck.Enqueue(card);
                        }
                    }
                    else
                    {
                        Console.WriteLine("War Ended with Player 2 winning by " + player2.playerDeck.Peek().cardName + " of " + player2.playerDeck.Peek().cardSuit + " over the " + player1.playerDeck.Peek().cardName + " of " + player1.playerDeck.Peek().cardSuit);

                        commonPile.Enqueue(player1.playerDeck.Dequeue());
                        commonPile.Enqueue(player2.playerDeck.Dequeue());

                        // List off all cards in the common pile to show the complete list of winnings in CLI
                        // and award the cards in the common pile to the winning player
                        Console.WriteLine("Total winnings were");
                        foreach (Card card in commonPile)
                        {
                            Console.WriteLine(card.cardName + " of " + card.cardSuit);
                            //player2.playerDeck.Enqueue(commonPile.Dequeue());
                            player2.playerDeck.Enqueue(card);

                        }
                    }
                }
                // nobody lost the game in the war
                return 0; 
            }
            else ////////////////////////////////////////////////// THIS IS THE START OF THE NORMAL WAR BEHAVIOR /////////////////////////////////////////////////////
            {
                // for this normal war, their decks must have at least 5 cards  
                if (player1.playerDeck.Count < 5)
                {
                    // player 1 loses
                    Console.WriteLine("player 1 lost -- out of cards for the repeat war");
                    return 1;
                }
                else if (player2.playerDeck.Count < 5)
                {
                    // player 2 loses
                    Console.WriteLine("player 2 lost -- out of cards for the repeat war");
                    return 2;
                }
                // Else both players have enough cards to complete the war
                else
                {
                    // get the cards off the top of their decks down to the 5th card which is what the 
                    // battle for the common pile will be over
                    for (int x = 0; x < 4; x++)
                    {
                        commonPile.Enqueue(player1.playerDeck.Dequeue());
                        commonPile.Enqueue(player2.playerDeck.Dequeue());
                    }

                    // if these now match , then we have a repeat war to go to
                    if (player1.playerDeck.Peek().cardValue == player2.playerDeck.Peek().cardValue)
                    {
                        // this means repeat war
                        Console.WriteLine("Repeat War");
                        executeWar(player1, player2, true, commonPile);
                    }
                    // Else if player 1 wins the war award them the common pile
                    else if (player1.playerDeck.Peek().cardValue > player2.playerDeck.Peek().cardValue)
                    {
                        Console.WriteLine("War Ended with Player 1 winning by " + player1.playerDeck.Peek().cardName + " of " + player1.playerDeck.Peek().cardSuit + " over the " + player2.playerDeck.Peek().cardName + " of " + player2.playerDeck.Peek().cardSuit);

                        commonPile.Enqueue(player1.playerDeck.Dequeue());
                        commonPile.Enqueue(player2.playerDeck.Dequeue());
            
                        // List off all cards in the common pile to show the complete list of winnings in CLI
                        // and award the cards in the common pile to the winning player
                        Console.WriteLine("Total winnings were");
                        foreach (Card card in commonPile)
                        {
                            Console.WriteLine(card.cardName + " of " + card.cardSuit);
                            //player1.playerDeck.Enqueue(commonPile.Dequeue());
                            player1.playerDeck.Enqueue(card);
                        }
                    }
                    // Else if player 2 wins the war award them the common pile
                    else
                    {
                        Console.WriteLine("War Ended with Player 2 winning by " + player2.playerDeck.Peek().cardName + " of " + player2.playerDeck.Peek().cardSuit + " over the " + player1.playerDeck.Peek().cardName + " of " + player1.playerDeck.Peek().cardSuit);

                        commonPile.Enqueue(player1.playerDeck.Dequeue());
                        commonPile.Enqueue(player2.playerDeck.Dequeue());

                        // List off all cards in the common pile to show the complete list of winnings in CLI
                        // and award the cards in the common pile to the winning player
                        Console.WriteLine("Total winnings were");
                        foreach (Card card in commonPile)
                        {
                            Console.WriteLine(card.cardName + " of " + card.cardSuit);
                            //player2.playerDeck.Enqueue(commonPile.Dequeue());
                            player2.playerDeck.Enqueue(card);

                        }
                    }
                }
                // nobody lost the game in the war
                return 0;
            }
        }


        // outside of a war scenario, award the winning player of the two card battle
        // returns 3 possible integers, 0 for no player lost
        // 1 for player 1 lost ( ran out of cards) and 2 for player 2
        public static int awardWinningPlayer(Player player1, Player player2)
        {
            // If player 1 wins the battle award them the two cards and check if player 2 is out of cards
            if (player1.playerDeck.Peek().cardValue > player2.playerDeck.Peek().cardValue)
            {
                Console.WriteLine("Player 1 will be awarded the " + player2.playerDeck.Peek().cardName + " of " + player2.playerDeck.Peek().cardSuit + " and the " + player1.playerDeck.Peek().cardName + " of " + player1.playerDeck.Peek().cardSuit);

                player1.playerDeck.Enqueue(player2.playerDeck.Dequeue());
                player1.playerDeck.Enqueue(player1.playerDeck.Dequeue());

                if (player2.playerDeck.Count() == 0)
                {
                    return 2;
                }
                return 0;
            }
            // Else if player 2 wins the battle award them the two cards and check if player 1 is out of cards
            else if (player2.playerDeck.Peek().cardValue > player1.playerDeck.Peek().cardValue)
            {
                Console.WriteLine("Player 2 will be awarded the " + player1.playerDeck.Peek().cardName + " of " + player1.playerDeck.Peek().cardSuit + " and the " + player2.playerDeck.Peek().cardName + " of " + player2.playerDeck.Peek().cardSuit);

                player2.playerDeck.Enqueue(player1.playerDeck.Dequeue());
                player2.playerDeck.Enqueue(player2.playerDeck.Dequeue());

                if (player1.playerDeck.Count() == 0)
                {
                    return 1;
                }
                return 0;
            }
            else
            {
                // Else it's a match and we need to execute a war
                Console.WriteLine("War incomming with Player 1's " + player1.playerDeck.Peek().cardName + " of " + player1.playerDeck.Peek().cardSuit + " and player 2's " + player2.playerDeck.Peek().cardName + " of " + player2.playerDeck.Peek().cardSuit);
                return executeWar(player1, player2);
            }
        }
    }
}
