using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Ex02_01;

namespace AmericanCheckers
{
    public partial class FormAmericanCheckers : Form
    {
        public FormAmericanCheckers()
        {
            InitializeComponent();
            this.Top = 0;
        }

        public event Action Activated;

        public Button[,] DamkaBoardButtons { get; set; }

        public StringBuilder CurrentPotentialMove { get; set; } = new StringBuilder();

        public void AmericanCheckersGame_Load()
        {
            int boardSize = DamkaGame.CurrentBoard.Size;
            string nameOfPlayerOne = DamkaGame.CurrentBoard.User0.UserName;
            string nameOfPlayerTwo = DamkaGame.CurrentBoard.User1.UserName;

            this.labelPlayer1.Text = nameOfPlayerOne + ":";
            this.labelPlayer2.Text = nameOfPlayerTwo + ":";
            this.labelFirstPlayerScore.Left = this.labelPlayer1.Left + this.labelPlayer1.Width + 5;
            this.labelSecondPlayerScore.Left = this.labelPlayer2.Left + this.labelPlayer2.Width + 5;

            int tileSize = 80;
            int gridSize = boardSize;
            this.Text = "Damka";
            DamkaBoardButtons = new Button[gridSize, gridSize];

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    Button newButton = new Button
                    {
                        Size = new Size(tileSize, tileSize),
                        Location = new Point(tileSize * j, (tileSize * i) + 50)
                    };

                    Controls.Add(newButton);

                    DamkaBoardButtons[i, j] = newButton;
                    newButton.Click += button_clicked;

                    newButton.Tag = new Point(i, j);
                    if ((i + j) % 2 == 0)
                    {
                        newButton.BackColor = Color.Gray;
                        newButton.Enabled = false;
                    }
                    else
                    {
                        newButton.BackColor = Color.White;
                    }
                }
            }
        }

        private void button_clicked(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            Point location = (Point)clickedButton.Tag;
            int row = location.X;
            int column = location.Y;
            string locationLetterRep = string.Format("{0}{1}", (char)(column + 'A'), (char)(row + 'a'));
            if (CurrentPotentialMove.Length == 0)
            {
                if (clickedButton.BackgroundImage != null)
                {
                    checkerSelected(clickedButton);
                    CurrentPotentialMove.AppendFormat("{0}>", locationLetterRep);
                }
            }
            else if (CurrentPotentialMove.Length == 3)
            {
                if (locationLetterRep == CurrentPotentialMove.ToString(0, 2))
                {
                    checkerDeselected(clickedButton);
                    CurrentPotentialMove.Clear();
                }
                else
                {
                    Button markedButton = this.letterToLocation();
                    CurrentPotentialMove.AppendFormat("{0}", locationLetterRep);
                    if (this.Activated != null)
                    {
                        Activated.Invoke();
                    }
                    
                    checkerDeselected(clickedButton);

                    while (DamkaGame.CurrentBoard.User1.UserName == "Computer" && DamkaGame.TurnOfPlayer == 1)
                    {
                        if (this.Activated != null)
                        {
                            Activated.Invoke();
                        }
                    }
                }
            }
        }

        public void UpdateBoard(Board io_CurrentBoard)
        {
            for (int i = 0; i < io_CurrentBoard.GameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < io_CurrentBoard.GameBoard.GetLength(1); j++)
                {
                    Button newButton = DamkaBoardButtons[i, j];
                    if (io_CurrentBoard.GameBoard[i, j] == null)
                    {
                        newButton.BackgroundImage = null;
                    }
                    else
                    {
                        char typeOfChecker = io_CurrentBoard.GameBoard[i, j].CharRep();

                        convertToImage(newButton, typeOfChecker);
                        newButton.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                }
            }
        }

        private Button letterToLocation()
        {
            int row = CurrentPotentialMove[1] - 'a';
            int column = CurrentPotentialMove[0] - 'A';
            return DamkaBoardButtons[row, column];
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            CurrentPotentialMove.Clear();
            CurrentPotentialMove.Append("Q");

            if (this.Activated != null)
            {
                Activated.Invoke();
            }
        }

        public void UpdateScoreLabels(int i_FirstPlayerScore, int i_SecondPlayerScore)
        {
            this.labelFirstPlayerScore.Text = i_FirstPlayerScore.ToString();
            this.labelSecondPlayerScore.Text = i_SecondPlayerScore.ToString();
        }

        private void checkerSelected(Button i_ClickedButton)
        {
            Checker clickedButtonChecker = DamkaGame.CurrentBoard.GameBoard[(i_ClickedButton.Location.Y - 50) / 80, i_ClickedButton.Location.X / 80];
            char typeOfChecker = clickedButtonChecker.CharRep();

            if (typeOfChecker == 'X')
            {
                i_ClickedButton.BackgroundImage = Properties.Resources.ClickedManX;
            }
            else if (typeOfChecker == 'Z')
            {
                i_ClickedButton.BackgroundImage = Properties.Resources.ClickedKingX;
            }
            else if (typeOfChecker == 'O')
            {
                i_ClickedButton.BackgroundImage = Properties.Resources.ClickedManO;
            }
            else
            {
                i_ClickedButton.BackgroundImage = Properties.Resources.ClickedKingO;
            }
        }

        private void checkerDeselected(Button i_ClickedButton)
        {
            Checker clickedButtonChecker = DamkaGame.CurrentBoard.GameBoard[(i_ClickedButton.Location.Y - 50) / 80, i_ClickedButton.Location.X / 80];

            if (clickedButtonChecker == null)
            {
                return;
            }

            char typeOfChecker = clickedButtonChecker.CharRep();

            convertToImage(i_ClickedButton, typeOfChecker);
        }

        private void convertToImage(Button i_Checker, char i_CeckerType)
        {
            if (i_CeckerType == 'X')
            {
                i_Checker.BackgroundImage = Properties.Resources.ManX;
            }
            else if (i_CeckerType == 'Z')
            {
                i_Checker.BackgroundImage = Properties.Resources.KingX;
            }
            else if (i_CeckerType == 'O')
            {
                i_Checker.BackgroundImage = Properties.Resources.ManO;
            }
            else
            {
                i_Checker.BackgroundImage = Properties.Resources.KingO;
            }
        }
    }
}