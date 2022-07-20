namespace Ex02_01
{
    using System;
    using System.Text;

    public class Board
    {
        private readonly int r_Size;
        private readonly DamkaUser r_User0;
        private readonly DamkaUser r_User1;
        private Checker[,] m_Board;

        public Board(int i_BoardSize, DamkaUser i_User0, DamkaUser i_User1)
        {
            r_Size = i_BoardSize;
            r_User0 = i_User0;
            r_User1 = i_User1;
            i_User0.NumOfMen = (r_Size * (r_Size - 2)) / 4;
            i_User1.NumOfMen = i_User0.NumOfMen;

            m_Board = new Checker[r_Size, r_Size];
        }

        public int Size
        {
            get { return r_Size; }
        }

        public DamkaUser User0
        {
            get { return r_User0; }
        }

        public DamkaUser User1
        {
            get { return r_User1; }
        }

        public Checker[,] GameBoard
        {
            get { return m_Board; }
            set { m_Board = value; }
        }

        public void ActivateBoard()
        {
            int rowLength = (int)Math.Sqrt(m_Board.Length);
            for (int i = 0; i < (rowLength / 2) - 1; i++)
            {
                for (int j = (i + 1) % 2; j < rowLength; j += 2)
                {
                    string location = string.Empty;
                    location = location + (char)(j + 'A') + (char)(i + 'a');
                    m_Board[i, j] = new Checker(location, r_User1);
                }
            }

            for (int i = (rowLength / 2) + 1; i < rowLength; i++)
            {
                for (int j = (i + 1) % 2; j < rowLength; j += 2)
                {
                    string location = string.Empty;
                    location = location + (char)(j + 'A') + (char)(i + 'a');
                    m_Board[i, j] = new Checker(location, r_User0);
                }
            }

            foreach (Checker checker in m_Board)
            {
                if (checker != null)
                {
                    checker.UpdateOptionalMoves(this);
                }
            }
        }
    }
}
