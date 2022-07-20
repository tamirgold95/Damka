namespace Ex02_01
{
    using System;

    public class DamkaUser
    {
        private static int m_LastUserID = 0;

        private readonly string r_UserName;

        public int NumOfMen { get; set; }

        public int NumOfKings { get; set; }

        public int TotalScore { get; set; }

        private readonly int r_UserID;

        public DamkaUser(string i_Username)
        {
            r_UserName = i_Username;
            r_UserID = m_LastUserID++;
        }

        public string UserName
        {
            get { return r_UserName; }
        }

        public int UserID
        {
            get { return r_UserID; }
        }

        public int GetCurrentPoints()
        {
            return NumOfMen + (NumOfKings * 4);
        }
    }
}
