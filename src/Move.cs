namespace Ex02_01
{
    using System;
    using System.Collections.Generic;

    public class Move
    {
        private readonly DamkaUser r_User;

        public Board CurrentBoard { get; set; }

        private readonly List<string> r_AllPossibleMoves = new List<string>();

        public Move(DamkaUser i_User, Board i_Board)
        {
            r_User = i_User;
            CurrentBoard = i_Board;
            foreach (Checker checker in CurrentBoard.GameBoard)
            {
                if (checker != null && checker.OwnerID == r_User.UserID)
                {
                    r_AllPossibleMoves.AddRange(checker.OptionalMoves);
                }
            }
        }

        public int UserID
        {
            get { return r_User.UserID; }
        }

        public List<string> AllPossibleMoves
        {
            get { return r_AllPossibleMoves; }
        }

        public void UpdatePossibleMoves()
        {
            r_AllPossibleMoves.Clear();
            foreach (Checker checker in CurrentBoard.GameBoard)
            {
                if (checker != null && checker.OwnerID == r_User.UserID && checker.CapturingIsPossible)
                {
                    r_AllPossibleMoves.AddRange(checker.OptionalMoves);
                }
            }

            if (r_AllPossibleMoves.Count == 0)
            {
                foreach (Checker checker in CurrentBoard.GameBoard)
                {
                    if (checker != null && checker.OwnerID == r_User.UserID)
                    {
                        r_AllPossibleMoves.AddRange(checker.OptionalMoves);
                    }
                }
            }
        }

        private bool isValidMove(string i_UserMove)
        {
            bool validMove = false;
            if (i_UserMove.Length == 5 && i_UserMove[2] == '>')
            {
                foreach (Checker checker in CurrentBoard.GameBoard)
                {
                    if (checker != null)
                    {
                        if (checker.OwnerID == UserID && AllPossibleMoves.Contains(i_UserMove))
                        {
                                validMove = true;
                        }
                    }
                }
            }

            return validMove;
        }

        public bool CurrentMove(ref string io_UserMove, DamkaUser i_User)
        {
            bool playerQuits = false;

            if (!isValidMove(io_UserMove) && io_UserMove != "Q")
            {
                io_UserMove = "Invalid";
            }
            else if (io_UserMove == "Q")
            {
                playerQuits = true;
            }
            else
            {
                if (Math.Abs(io_UserMove[0] - io_UserMove[3]) != 1)
                {
                    int colIndex = ((io_UserMove[0] + io_UserMove[3]) / 2) - 'A';
                    int rowIndex = ((io_UserMove[1] + io_UserMove[4]) / 2) - 'a';
                    char firstLetter = (char)((io_UserMove[0] + io_UserMove[3]) / 2);
                    char secondLetter = (char)((io_UserMove[1] + io_UserMove[4]) / 2);

                    updateCapturedPlayer(rowIndex, colIndex);
                }

                CurrentBoard.GameBoard[io_UserMove[1] - 'a', io_UserMove[0] - 'A'].Location = io_UserMove.Substring(3, 2);
                Checker movingChecker = CurrentBoard.GameBoard[io_UserMove[1] - 'a', io_UserMove[0] - 'A'];
                CurrentBoard.GameBoard[io_UserMove[4] - 'a', io_UserMove[3] - 'A'] = movingChecker;
                CurrentBoard.GameBoard[io_UserMove[1] - 'a', io_UserMove[0] - 'A'] = null;

                char bottomRowChar = (char)(CurrentBoard.Size - 1 + 'a');

                if ((movingChecker.CharRep() == 'X' && io_UserMove[4] == 'a') || (movingChecker.CharRep() == 'O' && io_UserMove[4] == bottomRowChar))
                {
                    movingChecker.Rank = "king";
                    i_User.NumOfKings++;
                    i_User.NumOfMen--;
                }

                foreach (Checker checker in CurrentBoard.GameBoard)
                {
                    if (checker != null)
                    {
                        checker.UpdateOptionalMoves(CurrentBoard);
                    }
                }
            }

            return playerQuits;
        }

        private void updateCapturedPlayer(int i_RowIndex, int i_ColIndex)
        {
            char capturedCheckerRank = CurrentBoard.GameBoard[i_RowIndex, i_ColIndex].CharRep();
            CurrentBoard.GameBoard[i_RowIndex, i_ColIndex] = null;

            if (UserID != CurrentBoard.User0.UserID)
            {
                if (capturedCheckerRank == 'X')
                {
                    CurrentBoard.User0.NumOfMen--;
                }
                else
                {
                    CurrentBoard.User0.NumOfKings--;
                }
            }
            else
            {
                if (capturedCheckerRank == 'O')
                {
                    CurrentBoard.User1.NumOfMen--;
                }
                else
                {
                    CurrentBoard.User1.NumOfKings--;
                }
            }
        }
    }
}
