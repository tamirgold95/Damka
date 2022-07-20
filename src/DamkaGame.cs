using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ex02_01;

namespace AmericanCheckers
{
    public class DamkaGame
    {
        public static Board CurrentBoard { get; set; }

        public static int TurnOfPlayer { get; set; } = 0;

        private FormAmericanCheckers gameForm;

        public bool CaptureMove { get; set; } = false;

        public bool CheckerDoesNotHaveAdditionalCapture { get; set; } = false;

        public string PrevUserMove { get; set; }

        public DamkaGame()
        {
            GameSettings currentGameSettings = new GameSettings();
            Application.Run(currentGameSettings);
            DamkaUser firstPlayer = new DamkaUser(currentGameSettings.firstPlayerName);
            DamkaUser secondPlayer = new DamkaUser(currentGameSettings.secondPlayerName);
            CurrentBoard = new Board(currentGameSettings.boardSize, firstPlayer, secondPlayer);
            gameForm = new FormAmericanCheckers();
            gameForm.AmericanCheckersGame_Load();
            startNewGame();
            gameForm.Activated += playTurn;
            Application.Run(gameForm);
        }

        private static void changeTurn()
        {
            if (TurnOfPlayer == 0)
            {
                TurnOfPlayer = 1;
            }
            else
            {
                TurnOfPlayer = 0;
            }
        }

        private static string computerMove(Board io_Board, Move i_ComputerUser)
        {
            return i_ComputerUser.AllPossibleMoves[0];
        }

        private static string buildGameEndMessage(string i_Reason, DamkaUser io_losingPlayer, DamkaUser io_winningPlayer)
        {
            string finalMessage = string.Empty;
            string losingPlayerName = io_losingPlayer.UserName;
            string winningPlayerName = io_winningPlayer.UserName;

            if (losingPlayerName == "Computer")
            {
                losingPlayerName = "The computer";
            }

            if (winningPlayerName == "Computer")
            {
                winningPlayerName = "The computer";
            }

            finalMessage = string.Format(
                @"{0} {1}. {2} wins!
His total score is now {3} points.
Would you like to play again?",
losingPlayerName,
i_Reason,
winningPlayerName,
io_winningPlayer.TotalScore);

            return finalMessage;
        }

        private void playTurn()
        {
            bool firstGame = true;
            DamkaUser firstPlayer = CurrentBoard.User0;
            DamkaUser secondPlayer = CurrentBoard.User1;
            Move player0 = new Move(firstPlayer, CurrentBoard);
            Move player1 = new Move(secondPlayer, CurrentBoard);
            string userInput = gameForm.CurrentPotentialMove.ToString();
            string secondUserInput = string.Empty;
            bool isSingleUser = secondPlayer.UserName == "Computer";

            if (TurnOfPlayer == 0 && firstGame)
            {
                player0.UpdatePossibleMoves();
                if (CaptureMove && CurrentBoard.GameBoard[PrevUserMove[4] - 'a', PrevUserMove[3] - 'A'].CapturingIsPossible)
                {
                    secondUserInput = userInput;
                    if (secondUserInput[0] != PrevUserMove[3] || secondUserInput[1] != PrevUserMove[4])
                    {
                        gameForm.CurrentPotentialMove.Clear();
                    }

                    makeMove(player0, ref userInput, firstPlayer, secondPlayer, ref firstGame);
                }
                else if (CaptureMove && !CurrentBoard.GameBoard[PrevUserMove[4] - 'a', PrevUserMove[3] - 'A'].CapturingIsPossible)
                {
                    CheckerDoesNotHaveAdditionalCapture = true;
                }
                else
                {
                    makeMove(player0, ref userInput, firstPlayer, secondPlayer, ref firstGame);
                }

                checkIfOpponentLost(player1, firstPlayer, secondPlayer, ref firstGame);
            }
            else
            {
                player1.UpdatePossibleMoves();
                if (CaptureMove && CurrentBoard.GameBoard[PrevUserMove[4] - 'a', PrevUserMove[3] - 'A'].CapturingIsPossible)
                {
                    secondUserInput = userInput;
                    if (isSingleUser)
                    {
                        secondUserInput = CurrentBoard.GameBoard[PrevUserMove[4] - 'a', PrevUserMove[3] - 'A'].OptionalMoves[0];
                    }

                    if (secondUserInput[0] != PrevUserMove[3] || secondUserInput[1] != PrevUserMove[4])
                    {
                        gameForm.CurrentPotentialMove.Clear();
                    }

                    userInput = secondUserInput;
                    makeMove(player1, ref userInput, secondPlayer, firstPlayer, ref firstGame);
                }
                else if (CaptureMove && !CurrentBoard.GameBoard[PrevUserMove[4] - 'a', PrevUserMove[3] - 'A'].CapturingIsPossible)
                {
                    CheckerDoesNotHaveAdditionalCapture = true;
                }
                else
                {
                    if (isSingleUser)
                    {
                        userInput = computerMove(CurrentBoard, player1);
                    }

                    makeMove(player1, ref userInput, secondPlayer, firstPlayer, ref firstGame);
                }

                checkIfOpponentLost(player0, secondPlayer, firstPlayer, ref firstGame);
            }

            if (userInput != "Invalid" && firstGame)
            {
                changeTurnIfNeeded(userInput);
            }
            else if (firstGame)
            {
                MessageBox.Show("Illegal Move");
            }
            
            gameForm.CurrentPotentialMove.Clear();
            gameForm.UpdateBoard(CurrentBoard);
        }

        private void changeTurnIfNeeded(string userInput)
        {
            PrevUserMove = userInput;
            if (Math.Abs(userInput[0] - userInput[3]) != 2 || CheckerDoesNotHaveAdditionalCapture)
            {
                changeTurn();
                CheckerDoesNotHaveAdditionalCapture = false;
                CaptureMove = false;
            }
            else
            {
                gameForm.UpdateBoard(CurrentBoard);
                int row = userInput[4] - 'a';
                int column = userInput[3] - 'A';
                if (!CurrentBoard.GameBoard[row, column].CapturingIsPossible)
                {
                    changeTurn();
                    CaptureMove = false;
                }
                else
                {
                    CaptureMove = true;
                }
            }
        }

        private void checkIfOpponentLost(Move player0, DamkaUser secondPlayer, DamkaUser firstPlayer, ref bool firstGame)
        {
            player0.UpdatePossibleMoves();
            if (player0.AllPossibleMoves.Count == 0)
            {
                gameForm.UpdateBoard(CurrentBoard);
                secondPlayer.TotalScore += secondPlayer.GetCurrentPoints() - firstPlayer.GetCurrentPoints();
                firstGame = gameEnd("has nowhere to go");
            }
        }

        private void makeMove(Move player0, ref string userInput, DamkaUser firstPlayer, DamkaUser secondPlayer, ref bool firstGame)
        {
            bool playerQuits = player0.CurrentMove(ref userInput, firstPlayer);
            if (playerQuits)
            {
                secondPlayer.TotalScore += 10;
                firstGame = gameEnd("quits");
            }
        }

        private bool gameEnd(string i_Reason)
        {
            string gameEndMessage = string.Empty;
            if ((i_Reason == "quits" && TurnOfPlayer == 0) || (i_Reason != "quits" && TurnOfPlayer == 1))
            {
                gameEndMessage = buildGameEndMessage(i_Reason, CurrentBoard.User0, CurrentBoard.User1);
            }
            else
            {
                gameEndMessage = buildGameEndMessage(i_Reason, CurrentBoard.User1, CurrentBoard.User0);
            }

            if (MessageBox.Show(gameEndMessage, "Game End", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                startNewGame();
            }
            else
            {
                gameForm.Close();
            }

            gameForm.UpdateScoreLabels(CurrentBoard.User0.TotalScore, CurrentBoard.User1.TotalScore);

            return false;
        }

        private void startNewGame()
        {
            Checker[,] cleanBoard = new Checker[CurrentBoard.Size, CurrentBoard.Size];
            CurrentBoard.GameBoard = cleanBoard;
            CurrentBoard.ActivateBoard();
            gameForm.UpdateBoard(CurrentBoard);
            TurnOfPlayer = 0;
            CaptureMove = false;
            CurrentBoard.User0.NumOfMen = CurrentBoard.User1.NumOfMen = (CurrentBoard.Size * (CurrentBoard.Size - 2)) / 4;
            CurrentBoard.User0.NumOfKings = CurrentBoard.User1.NumOfKings = 0;
        }
    }
}
