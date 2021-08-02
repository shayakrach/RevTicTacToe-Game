using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Player
    {
        private readonly BoardGame.eCoin r_Coin;
        private readonly string r_Name;
        private readonly bool r_isComputer;
        private double m_Score;

        public Player(string i_Name, BoardGame.eCoin i_Coin, bool i_IsComputer)
        {
            r_Name = i_IsComputer ? "Computer" : i_Name;
            r_Coin = i_Coin;
            r_isComputer = i_IsComputer;
            m_Score = 0;
        }

        public Player(string i_Name, BoardGame.eCoin i_Coin) : this(i_Name, i_Coin, false)
        {
        }

        public string Name
        {
            get
            {
                return r_Name;
            }
        }

        public double Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public BoardGame.eCoin Coin
        {
            get
            {
                return r_Coin;
            }
        }

        public bool IsComputer
        {
            get
            {
                return r_isComputer;
            }
        }
    }
}
