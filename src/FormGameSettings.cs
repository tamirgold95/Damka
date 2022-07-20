using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AmericanCheckers
{
    public partial class GameSettings : Form
    {
        public GameSettings()
        {
            InitializeComponent();
        }

        public FormAmericanCheckers chosenGame { get; set; }

        public int boardSize { get; set; }

        public string firstPlayerName { get; set; }

        public string secondPlayerName { get; set; }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if (textBoxPlayer2.Enabled)
            {
                textBoxPlayer2.Enabled = false;
                textBoxPlayer2.Text = "[Computer]";
            }
            else
            {
                textBoxPlayer2.Enabled = true;
                textBoxPlayer2.Text = string.Empty;
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            firstPlayerName = this.textBoxPlayer1.Text;

            if (this.checkBoxPlayer2.Checked)
            {
                secondPlayerName = this.textBoxPlayer2.Text;
            }
            else
            {
                secondPlayerName = "Computer";
            }

            if (radioButtonSize6.Checked)
            {
                boardSize = 6;
            }
            else if (radioButtonSize8.Checked)
            {
                boardSize = 8;
            }
            else
            {
                boardSize = 10;
            }

            this.Close();
        }
    }
}
