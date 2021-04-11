using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tictactoe
{
    public partial class Form1 : Form
    {
        Player currentPlayer;
        List<Button> buttons;       //create a list of buttons
        Random rand = new Random(); //init random number generator class
        int playerWins = 0;         //init player wins to 0
        int computerWins = 0;       //init computer wins to 0


        public enum Player
        {
            X, O
        }

        public Form1()
        {
            InitializeComponent();
            resetGame();
        }

        private void playerClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            currentPlayer = Player.X;
            button.Text = currentPlayer.ToString();     //changes text of button to player value, in this case X
            button.Enabled = false;                     //disables button to be selected
            button.BackColor = System.Drawing.Color.LightGreen; //sets player color to light green
            buttons.Remove(button);                     //removes button from list so the AI can't pick it
            check();
            AImoves.Start();
        }

        //Purposefully left off the "s" so as to not conflict with timer control
        private void AImove(object sender, EventArgs e)
        {
            if (buttons.Count > 0)
            {
                int index = rand.Next(buttons.Count);
                buttons[index].Enabled = false;

                currentPlayer = Player.O;
                buttons[index].Text = currentPlayer.ToString();
                buttons[index].BackColor = System.Drawing.Color.LightSalmon;
                buttons.RemoveAt(index);
                check();
                AImoves.Stop();
            }
        }

        //resets the game when the restart button is clicked
        private void restartGame(object sender, EventArgs e)
        {
            resetGame();
        }

        //Used to load buttons from the form the buttons list
        private void loadButtons()
        {
            buttons = new List<Button> { button1, button2, button3, button4, button5, button6, button7, button8, button9 };
        }

        private void resetGame()
        {
            foreach (Control X in this.Controls)
            {
                //checks each button with a tag of "play"
                if (X is Button && X.Tag == "play")
                {
                    ((Button)X).Enabled = true;             //sets button to clickable again
                    ((Button)X).Text = "?";                 //sets text to default ? from X/O
                    ((Button)X).BackColor = default; //sets background color back to default
                }
            }
            loadButtons(); //calls loadButtons so all the buttons are put back into the array
        }

        private void check()
        {
            string playerChoice = "X";
            string computerChoice = "O";
            //check horizontal
            if (button1.Text == playerChoice && button2.Text == playerChoice && button3.Text == playerChoice
             || button4.Text == playerChoice && button5.Text == playerChoice && button6.Text == playerChoice
             || button7.Text == playerChoice && button8.Text == playerChoice && button9.Text == playerChoice)
            {
                playerWin();
            }
            //check vertical
            else if (button1.Text == playerChoice && button4.Text == playerChoice && button7.Text == playerChoice
                  || button2.Text == playerChoice && button5.Text == playerChoice && button8.Text == playerChoice
                  || button3.Text == playerChoice && button6.Text == playerChoice && button9.Text == playerChoice)
            {
                playerWin();
            }
            //check diagonal
            else if (button1.Text == playerChoice && button5.Text == playerChoice && button9.Text == playerChoice
                  || button3.Text == playerChoice && button5.Text == playerChoice && button7.Text == playerChoice)
            {
                playerWin();
            }

            //check horizontal for AI
            if (button1.Text == computerChoice && button2.Text == computerChoice && button3.Text == computerChoice
             || button4.Text == computerChoice && button5.Text == computerChoice && button6.Text == computerChoice
             || button7.Text == computerChoice && button8.Text == computerChoice && button9.Text == computerChoice)
            {
                AIwin();
            }
            //check vertical for AI
            else if (button1.Text == computerChoice && button4.Text == computerChoice && button7.Text == computerChoice
                  || button2.Text == computerChoice && button5.Text == computerChoice && button8.Text == computerChoice
                  || button3.Text == computerChoice && button6.Text == computerChoice && button9.Text == computerChoice)
            {
                AIwin();
            }
            //check diagonal for AI
            else if (button1.Text == computerChoice && button5.Text == computerChoice && button9.Text == computerChoice
                  || button3.Text == computerChoice && button5.Text == computerChoice && button7.Text == computerChoice)
            {
                AIwin();
            }
        }

        private void playerWin()
        {
            AImoves.Stop();
            MessageBox.Show("Player Wins");
            playerWins++;
            label1.Text = "Player Wins - " + playerWins;
            resetGame();
        }

        private void AIwin()
        {
            AImoves.Stop();
            MessageBox.Show("AI Wins");
            computerWins++;
            label2.Text = "AI Wins - " + computerWins;
            resetGame();
        }
    }
}
