using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualWar
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        // create the players and use number of clicks to give a first time play hand button action that 
        // only happens on the first click 
        int NumberOfClick = 0;
        private Player player1 = new Player();
        private Player player2 = new Player();

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Play hand button used to execute each hand in the war game to the next hand
        private void button1_Click(object sender, EventArgs e)
        {
 
            // hide the War title and war action text boxes and who lost last game
            textBox1.Visible = false;
            textBox7.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;


            ++NumberOfClick;
            // On the first click of play hand
            // give each player their shuffled decks and show the text boxes describing card counts and 
            // player titles
            switch (NumberOfClick)
            {
                case 1:
                    List<Card> shuffledDeck = Card.generateShuffledDeck();
                    var playerDecks = Player.fillPlayerDecks(shuffledDeck);

                    textBox2.Visible = true;
                    textBox3.Visible = true;
                    textBox6.Visible = true;
                    textBox8.Visible = true;

                    player1.playerDeck = playerDecks.Item1;
                    player2.playerDeck = playerDecks.Item2;
                    break;
            }

            // temp decks used to observe wars and show correct face up cards without disturbing the 
            // actual player decks to check inside
            Queue<Card> playerDeck1Temp = new Queue<Card>(player1.playerDeck);
            Queue<Card> playerDeck2Temp = new Queue<Card>(player2.playerDeck);

            // Partial path to get to the PNGs used for the background of the card table as well as the cards and back of card images
            string directory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();

            // show the green background image over the playspace
            Graphics g = this.CreateGraphics();
            Image img = Image.FromFile(directory + @"\background.png");
            g.DrawImage(img, 0, 0, this.Width, this.Height);

            // show faceup the top two cards from each players deck
            var cardOnTop = player1.playerDeck.Peek();
            g.DrawImage(cardOnTop.picture, this.Width / 5, this.Height / 5, (this.Width / 4) - 12, this.Height / 3);
            cardOnTop = player2.playerDeck.Peek();
            g.DrawImage(cardOnTop.picture, (this.Width / 5) + 200, this.Height / 5, (this.Width / 4) - 12, this.Height / 3);


            // if there's a war then show 3 face down cards and the deciding card face up to show how the winner won
            if ((player1.playerDeck.Peek().cardValue == player2.playerDeck.Peek().cardValue) & (player1.playerDeck.Count() > 4) & (player2.playerDeck.Count() > 4))
            {

                textBox7.Visible = true;

                img = Image.FromFile(directory + @"\PNGs\red_back.png");
                g.DrawImage(img, (this.Width / 10) + 50, this.Height / 5, (this.Width / 6), this.Height / 5);
                g.DrawImage(img, (this.Width / 10) + 50, this.Height / 4, (this.Width / 6), this.Height / 5);
                g.DrawImage(img, (this.Width / 10) + 50, this.Height / 3, (this.Width / 6), this.Height / 5);

                g.DrawImage(img, (this.Width / 3) + 170, this.Height / 5, (this.Width / 6), this.Height / 5);
                g.DrawImage(img, (this.Width / 3) + 170, this.Height / 4, (this.Width / 6), this.Height / 5);
                g.DrawImage(img, (this.Width / 3) + 170, this.Height / 3, (this.Width / 6), this.Height / 5);

                playerDeck1Temp.Dequeue();
                playerDeck1Temp.Dequeue();
                playerDeck1Temp.Dequeue();
                playerDeck1Temp.Dequeue();

                cardOnTop = playerDeck1Temp.Peek();
                g.DrawImage(cardOnTop.picture, (this.Width / 10) + 50, this.Height / 2, (this.Width / 6), this.Height / 5);

                playerDeck2Temp.Dequeue();
                playerDeck2Temp.Dequeue();
                playerDeck2Temp.Dequeue();
                playerDeck2Temp.Dequeue();

                cardOnTop = playerDeck2Temp.Peek();

                g.DrawImage(cardOnTop.picture, (this.Width / 3) + 170, this.Height / 2, (this.Width / 6), this.Height / 5);
            }

            // update the player card counts
            textBox2.Text = player2.playerDeck.Count.ToString();
            textBox3.Text = player1.playerDeck.Count.ToString();

            int loser = Gameplay.awardWinningPlayer(player1, player2);

            switch (loser)
            {
                case 1:
                    // player 1 loses show and update relevant
                    textBox4.Visible = true;
                    textBox7.Visible = false;
                    textBox2.Text = player2.playerDeck.Count.ToString();
                    textBox3.Text = player1.playerDeck.Count.ToString();

                    // Code to trigger restart if click playhand again
                    NumberOfClick = 0;

                    break;

                case 2:
                    // player 2 loses show and update relevant
                    textBox5.Visible = true;
                    textBox7.Visible = false;
                    textBox2.Text = player2.playerDeck.Count.ToString();
                    textBox3.Text = player1.playerDeck.Count.ToString();

                    // Code to trigger restart if click playhand again
                    NumberOfClick = 0;
                    break;
            }
            if (player1.playerDeck.Count() == 0)
            {
                // player 1 loses show and update relevant
                textBox4.Visible = true;
                textBox7.Visible = false;
                textBox2.Text = player2.playerDeck.Count.ToString();
                textBox3.Text = player1.playerDeck.Count.ToString();

                // Code to trigger restart if click playhand again
                NumberOfClick = 0;
            }
            if (player2.playerDeck.Count() == 0)
            {
                // player 1 loses show and update relevant
                textBox4.Visible = true;
                textBox7.Visible = false;
                textBox2.Text = player2.playerDeck.Count.ToString();
                textBox3.Text = player1.playerDeck.Count.ToString();

                // Code to trigger restart if click playhand again
                NumberOfClick = 0;
            }
        }
            
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        }
    }
}