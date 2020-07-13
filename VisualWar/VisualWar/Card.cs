using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualWar
{
    class Card
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        // Enum depicting each of the possible suits in a deck of cards
        // also used to grab the correct png file name to display on the gameboard
        public enum Suit
        {
            H,
            D,
            C,
            S
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        // Enum depicting each of the possible facecards in a deck of cards
        // only facecards are here as other values have their name as their quantative value
        // also used to grab the correct png file name to display on the gameboard
        public enum Facecard
        {
            J,
            Q,
            K,
            A
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Property of all cards is the name ( k for king and so on), the numerical value for 2-10
        // the suit from the list of options in a deck of cards
        // the card value which is the underlying value of a card, an 8 is an 8 but a jack is an 11 for example,
        // and a picture which is used to display the image of the specific card on the gameboard when it is played
        public string cardName { get; set; }
        public Suit cardSuit { get; set; }
        public int cardValue { get; set; }


        public Image picture 
        { 
            get
            {
                // Partial path to get to the PNGs used to give the cards a property of an image
                string directory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();

                // Full path including the specific card name and suit code used to name the png file
                string fullPath = directory + @"\PNGs\" + cardName + cardSuit + ".png";
                return Image.FromFile(fullPath); ;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Constructor used to take in the characteristics for a card to be created with the correct values paired with it
        public Card(string name, Suit suit, int value)
        {
            cardName = name;
            cardSuit = suit;
            cardValue = value;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Method used to form a deck of 52 cards in the form of a list, shuffle this list, and return the shuffled deck
        // to later be distributed evenly amongst the two players
        public static List<Card> generateShuffledDeck()
        {
            List<Card> mainDeck = new List<Card>();
            int cardValue;

            foreach (Suit cardSuit in Enum.GetValues(typeof(Suit)))
            {
                // for each suit possible in the deck of cards, create a card with the value of 
                // 2 through 10 with the name 2 through 10 for all suits
                for (cardValue = 2; cardValue < 11; cardValue++)
                {
                    mainDeck.Add(new Card(cardValue.ToString(), cardSuit, cardValue));
                }

                // for each suit possible in the deck of cards, create a card with the value of 
                // 11 through 14 with the name j for jack through a for ace for all suits
                foreach (Facecard cardName in Enum.GetValues(typeof(Facecard)))
                {
                    mainDeck.Add(new Card(cardName.ToString(), cardSuit, cardValue));
                    cardValue++;
                }

            }

            // Randomize the list or "shuffle" the deck
            Random random = new Random();
            int cardsInDeck = 52;
            while (cardsInDeck > 1)
            {
                cardsInDeck--;
                int placeHolder = random.Next(cardsInDeck + 1);
                Card value = mainDeck[placeHolder];
                mainDeck[placeHolder] = mainDeck[cardsInDeck];
                mainDeck[cardsInDeck] = value;
            }


            return mainDeck;
        }
    }
}
