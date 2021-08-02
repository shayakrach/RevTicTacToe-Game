using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class RevTicTacToe
    {
        public event Action StatusChanged;
        
        public event Action TurnChanged;

        public enum eStatus
        {
            Player1Won,
            Player2Won,
            Tie,
            NotFinished,
            ExitGame
        }

        private eStatus m_Status;
        private readonly BoardGame r_Board;
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private bool m_CurrentTurnIsPlayer1;
        private bool m_StartingPlayerIsPlayer1;
        private int m_RoundNumber;
        
        public RevTicTacToe(BoardGame i_Board, Player i_Player1, Player i_Player2)
        {
            r_Board = i_Board;
            r_Player1 = i_Player1;
            r_Player2 = i_Player2;
            m_CurrentTurnIsPlayer1 = false;
            m_StartingPlayerIsPlayer1 = true;
            m_RoundNumber = 0;
            m_Status = eStatus.NotFinished;
        }

        public Player Player1
        {
            get
            {
                return r_Player1;
            }
        }

        public Player Player2
        {
            get
            {
                return r_Player2;
            }
        }

        public BoardGame Board
        {
            get
            {
                return r_Board;
            }
        }

        public int RoundNumber
        {
            get
            {
                return m_RoundNumber;
            }
        }

        public bool CurrentTurnIsPlayer1
        {
            get
            {
                return m_CurrentTurnIsPlayer1;
            }
        }

        public eStatus Status
        {
            get
            {
                return m_Status;
            }

            set
            {
                if(m_Status != value)
                {
                    m_Status = value;
                    OnStatusChanged();
                }
            }
        }

        public void NewRound()
        {
            r_Board.Clear();
            m_RoundNumber++;
            updateScores();
            m_Status = eStatus.NotFinished;
            switchToStartingPlayer();
        }

        private void switchToStartingPlayer()
        {
            m_CurrentTurnIsPlayer1 = m_StartingPlayerIsPlayer1;
            switchPlayer(ref m_StartingPlayerIsPlayer1);
        }

        private void switchPlayer(ref bool io_CurrentTurnIsPlayer1)
        {
            io_CurrentTurnIsPlayer1 = !io_CurrentTurnIsPlayer1;
            OnTurnChanged();
        }

        public void NextTurn(Position i_NextMove, BoardGame.eCoin i_CurrnetCoin)
        {
            if(r_Board.IsPosOutOfRange(i_NextMove))
            {
                m_Status = eStatus.ExitGame;
            }
            else
            {
                r_Board.SetCoin(i_NextMove, i_CurrnetCoin);

                if(r_Board.HasFullSequence(i_CurrnetCoin))
                {
                    Status = m_CurrentTurnIsPlayer1 ? eStatus.Player2Won : eStatus.Player1Won;
                }
                else if(r_Board.IsFull())
                {
                    Status = eStatus.Tie;
                }
                else
                {
                    switchPlayer(ref m_CurrentTurnIsPlayer1); 
                }
            }
        }

        private void updateScores()
        {
            switch(m_Status)
            {
                case eStatus.Player1Won:
                    r_Player1.Score++;
                    break;
                case eStatus.Player2Won:
                    r_Player2.Score++;
                    break;
                case eStatus.Tie:
                    r_Player1.Score += 0.5;
                    r_Player2.Score += 0.5;
                    break;
                default:
                    break;
            }
        }

        public BoardGame.eCoin GetCurrentPlayerCoin()
        {
            return CurrentTurnIsPlayer1 ? r_Player1.Coin : r_Player2.Coin;
        }

        public bool IsCurrentPlayerComputer()
        {
            return CurrentTurnIsPlayer1 ? r_Player1.IsComputer : r_Player2.IsComputer;
        }

        protected virtual void OnStatusChanged()
        {
            if(StatusChanged != null)
            {
                StatusChanged.Invoke();
            }
        }

        protected virtual void OnTurnChanged()
        {
            if(StatusChanged != null)
            {
                TurnChanged.Invoke();
            }
        }
    }
}
