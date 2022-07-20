namespace Ex02_01
{
    using System;
    using System.Collections.Generic;

    public class Checker
    {
        public string Rank { get; set; } = "man";

        public string Location { get; set; }

        public List<string> OptionalMoves { get; set; } = new List<string>();

        private readonly int r_OwnerID;

        private readonly char[] r_Allies = new char[2];

        public bool CapturingIsPossible { get; set; } = false;

        public Checker(string i_Location, DamkaUser i_Owner)
        {
            Location = i_Location;
            r_OwnerID = i_Owner.UserID;
            if (OwnerID == 0)
            {
                r_Allies[0] = 'X';
                r_Allies[1] = 'Z';
            }
            else
            {
                r_Allies[0] = 'O';
                r_Allies[1] = 'Q';
            }
        }

        public char[] Allies
        {
            get { return r_Allies; }
        }

        public int OwnerID
        {
            get { return r_OwnerID; }
        }

        public void UpdateOptionalMoves(Board io_CurrentBoard)
        {
            OptionalMoves.Clear();
            if (Rank == "king")
            {
                List<string> movesUp = calculateOptionalMoves(io_CurrentBoard, "Up");
                List<string> movesDown = calculateOptionalMoves(io_CurrentBoard, "Down");

                if (movesUp.Count == 0)
                {
                    OptionalMoves = movesDown;
                }
                else if (movesDown.Count == 0)
                {
                    OptionalMoves = movesUp;
                }
                else
                {
                    bool capturingUp = Math.Abs(movesUp[0][0] - Location[0]) == 2;
                    bool capturingDown = Math.Abs(movesDown[0][0] - Location[0]) == 2;

                    if (capturingUp && !capturingDown)
                    {
                        OptionalMoves = movesUp;
                    }
                    else if (!capturingUp && capturingDown)
                    {
                        OptionalMoves = movesDown;
                    }
                    else
                    {
                        OptionalMoves.AddRange(movesUp);
                        OptionalMoves.AddRange(movesDown);
                    }
                }
            }
            else
            {
                if (OwnerID == 0)
                {
                    OptionalMoves = calculateOptionalMoves(io_CurrentBoard, "Up");
                }
                else
                {
                    OptionalMoves = calculateOptionalMoves(io_CurrentBoard, "Down");
                }
            }

            for (int i = 0; i < OptionalMoves.Count; i++)
            {
                OptionalMoves[i] = string.Format("{0}>{1}", Location, OptionalMoves[i]);
            }
        }

        private List<string> calculateOptionalMoves(Board io_CurrentBoard, string i_UpOrDown)
        {
            char columnChar = Location[0];
            char rowChar = Location[1];
            int columnNum = columnChar - 'A';
            int rowNum = rowChar - 'a';

            List<string> capturingMoves = new List<string>();
            List<string> nonCapturingMoves = new List<string>();
            List<string> finalOptionalMoves = new List<string>();

            if ((rowNum == 0 && i_UpOrDown == "Up") || (rowNum == io_CurrentBoard.Size - 1 && i_UpOrDown == "Down"))
            {
            }
            else if (columnNum == 0)
            {
                EdgeMoves(io_CurrentBoard, capturingMoves, nonCapturingMoves, "Right", i_UpOrDown);
            }
            else if (columnNum == io_CurrentBoard.Size - 1)
            {
                EdgeMoves(io_CurrentBoard, capturingMoves, nonCapturingMoves, "Left", i_UpOrDown);
            }
            else
            {
                NonEdgeMoves(io_CurrentBoard, capturingMoves, nonCapturingMoves, i_UpOrDown);
            }

            if (capturingMoves.Count != 0)
            {
                finalOptionalMoves = capturingMoves;
                CapturingIsPossible = true;
            }
            else
            {
                finalOptionalMoves = nonCapturingMoves;
                if (Rank == "man" && i_UpOrDown == "Down")
                {
                    CapturingIsPossible = false;
                }
                else if (i_UpOrDown == "Up")
                {
                    CapturingIsPossible = false;
                }
            }

            return finalOptionalMoves;
        }

        private void EdgeMoves(Board io_CurrentBoard, List<string> io_CapturingMoves, List<string> io_NonCapturingMoves, string i_MoveLeftOrRight, string i_MoveUpOrDown)
        {
            char columnChar = Location[0];
            char rowChar = Location[1];
            int columnNum = columnChar - 'A';
            int rowNum = rowChar - 'a';
            int movingLeftOrRight = 1;
            int movingUpOrDown = 1;
            char potentialMove = '\0';

            if (i_MoveLeftOrRight == "Left")
            {
                movingLeftOrRight = -1;
            }

            if (i_MoveUpOrDown == "Up")
            {
                movingUpOrDown = -1;
            }

            bool noVerticalWay = (rowNum == 1 && i_MoveUpOrDown == "Up") || (rowNum == io_CurrentBoard.Size - 1 && i_MoveUpOrDown == "Down");

            if (io_CurrentBoard.GameBoard[rowNum + movingUpOrDown, columnNum + movingLeftOrRight] != null)
            {
                potentialMove = io_CurrentBoard.GameBoard[rowNum + movingUpOrDown, columnNum + movingLeftOrRight].CharRep();
            }

            if (potentialMove == Allies[0] || potentialMove == Allies[1])
            {
            }
            else if (potentialMove == '\0')
            {
                string move = string.Empty;
                move = move + (char)(columnChar + movingLeftOrRight) + (char)(rowChar + movingUpOrDown);
                io_NonCapturingMoves.Add(move);
            }
            else
            {
                if (rowNum + (2 * movingUpOrDown) < io_CurrentBoard.Size && rowNum + (2 * movingUpOrDown) >= 0 && columnNum + (2 * movingLeftOrRight) < io_CurrentBoard.Size && columnNum + (2 * movingLeftOrRight) >= 0)
                {
                    if (noVerticalWay || io_CurrentBoard.GameBoard[rowNum + (2 * movingUpOrDown), columnNum + (2 * movingLeftOrRight)] != null)
                    {
                    }
                    else
                    {
                        string move = string.Empty;
                        move = move + (char)(columnChar + (2 * movingLeftOrRight)) + (char)(rowChar + (2 * movingUpOrDown));
                        io_CapturingMoves.Add(move);
                    }
                }
            }
        }

        private void NonEdgeMoves(Board io_CurrentBoard, List<string> io_CapturingMoves, List<string> io_NonCapturingMoves, string i_MoveUpOrDown)
        {
            char columnChar = Location[0];
            char rowChar = Location[1];
            int columnNum = columnChar - 'A';
            int rowNum = rowChar - 'a';
            int movingUpOrDown = 1;
            char potentialMoveRight = '\0';
            char potentialMoveLeft = '\0';
            bool noVerticalWay = (rowNum == 1 && i_MoveUpOrDown == "Up") || (rowNum == io_CurrentBoard.Size - 1 && i_MoveUpOrDown == "Down");

            if (i_MoveUpOrDown == "Up")
            {
                movingUpOrDown = -1;
            }

            if (io_CurrentBoard.GameBoard[rowNum + movingUpOrDown, columnNum + 1] != null)
            {
                potentialMoveRight = io_CurrentBoard.GameBoard[rowNum + movingUpOrDown, columnNum + 1].CharRep();
            }

            if (io_CurrentBoard.GameBoard[rowNum + movingUpOrDown, columnNum - 1] != null)
            {
                potentialMoveLeft = io_CurrentBoard.GameBoard[rowNum + movingUpOrDown, columnNum - 1].CharRep();
            }

            if (potentialMoveRight == '\0')
            {
                string move = string.Empty;
                move = move + (char)(columnChar + 1) + (char)(rowChar + movingUpOrDown);
                io_NonCapturingMoves.Add(move);
            }
            else if (potentialMoveRight == r_Allies[0] || potentialMoveRight == r_Allies[1])
            {
            }
            else
            {
                if (rowNum + (2 * movingUpOrDown) < io_CurrentBoard.Size && rowNum + (2 * movingUpOrDown) >= 0 && columnNum + 2 < io_CurrentBoard.Size && columnNum + 2 >= 0)
                {
                    if (noVerticalWay || columnNum == io_CurrentBoard.Size - 2 || io_CurrentBoard.GameBoard[rowNum + (2 * movingUpOrDown), columnNum + 2] != null)
                    {
                    }
                    else
                    {
                        string move = string.Empty;
                        move = move + (char)(columnChar + 2) + (char)(rowChar + (2 * movingUpOrDown));
                        io_CapturingMoves.Add(move);
                    }
                }
            }

            if (potentialMoveLeft == '\0')
            {
                string move = string.Empty;
                move = move + (char)(columnChar - 1) + (char)(rowChar + movingUpOrDown);
                io_NonCapturingMoves.Add(move);
            }
            else if (potentialMoveLeft == r_Allies[0] || potentialMoveLeft == r_Allies[1])
            {
            }
            else
            {
                if (rowNum + (2 * movingUpOrDown) < io_CurrentBoard.Size && rowNum + (2 * movingUpOrDown) >= 0 && columnNum - 2 < io_CurrentBoard.Size && columnNum - 2 >= 0)
                {
                    if (noVerticalWay || columnNum == 1 || io_CurrentBoard.GameBoard[rowNum + (2 * movingUpOrDown), columnNum - 2] != null)
                    {
                    }
                    else
                    {
                        string move = string.Empty;
                        move = move + (char)(columnChar - 2) + (char)(rowChar + (2 * movingUpOrDown));
                        io_CapturingMoves.Add(move);
                    }
                }
            }
        }

        public char CharRep()
        {
            char boardRep = 'X';
            if (Rank == "man")
            {
                if (OwnerID == 1)
                {
                    boardRep = 'O';
                }
            }
            else
            {
                if (OwnerID == 0)
                {
                    boardRep = 'Z';
                }
                else
                {
                    boardRep = 'Q';
                }
            }

            return boardRep;
        }
    }
}
