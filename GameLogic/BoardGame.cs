using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class BoardGame
    {
        public delegate void CellChangedDelegate(int i_Row, int i_Col, eCoin i_Coin);

        public event CellChangedDelegate CellChanged;

        public enum eCoin
        {
            Player1,
            Player2,
            Empty
        }

        private readonly int r_Size;
        private eCoin[,] m_Board;
        private int m_AvailableCells;

        public BoardGame(int i_BoardSize) : this(new eCoin[i_BoardSize, i_BoardSize], i_BoardSize)
        {
            this.Clear();
        }

        public BoardGame(eCoin[,] i_Board, int i_BoardSize)
        {
            m_Board = new eCoin[i_BoardSize, i_BoardSize];
            r_Size = i_BoardSize;
        }

        public int Size
        {
            get
            {
                return r_Size;
            }
        }

        public int AvailableCells
        {
            get
            {
                return m_AvailableCells;
            }
        }

        private bool hasFullRow(eCoin i_Coin)
        {
            bool hasFullRow = false;

            for(int row = 0; row < r_Size; row++)
            {
                hasFullRow = isFullRow(i_Coin, row);

                if(hasFullRow)
                {
                    break;
                }
            }

            return hasFullRow;
        }

        private bool isFullRow(eCoin i_Coin, int i_Row)
        {
            int coinsCount = 0;

            for(int i = 0; i < r_Size; i++)
            {
                if(m_Board[i_Row, i] == i_Coin)
                {
                    coinsCount++;
                }
            }

            return coinsCount == r_Size;
        }

        private bool hasFullCol(eCoin i_Coin)
        {
            bool hasFullCol = false;

            for(int col = 0; col < r_Size; col++)
            {
                hasFullCol = isFullCol(i_Coin, col);

                if(hasFullCol)
                {
                    break;
                }
            }

            return hasFullCol;
        }

        private bool isFullCol(eCoin i_Coin, int i_Col)
        {
            int coinsCount = 0;

            for(int i = 0; i < r_Size; i++)
            {
                if(m_Board[i, i_Col] == i_Coin)
                {
                    coinsCount++;
                }
            }

            return coinsCount == r_Size;
        }

        private bool hasFullDiagonal(eCoin i_Coin)
        {
            int coinsOnMainDiagonal = 0;
            int coinsOnSecondaryDiagonal = 0;

            for(int i = 0, j = r_Size - 1; i < r_Size; i++, j--)
            {
                if(m_Board[i, i] == i_Coin)
                {
                    coinsOnMainDiagonal++;
                }

                if(m_Board[i, j] == i_Coin)
                {
                    coinsOnSecondaryDiagonal++;
                }
            }

            return coinsOnMainDiagonal == r_Size || coinsOnSecondaryDiagonal == r_Size;
        }

        public bool HasFullSequence(eCoin i_Coin)
        {
            return hasFullDiagonal(i_Coin) || hasFullCol(i_Coin) || hasFullRow(i_Coin);
        }

        public bool IsFull()
        {
            return m_AvailableCells == 0;
        }

        public void Clear()
        {
            for(int i = 0; i < r_Size; i++)
            {
                for(int j = 0; j < r_Size; j++)
                {
                    changeCellCoin(i, j, eCoin.Empty);
                }
            }

            m_AvailableCells = r_Size * r_Size;
        }

        public bool IsEmptyCell(Position i_CellPosition)
        {
            return m_Board[i_CellPosition.Row, i_CellPosition.Col] == eCoin.Empty;
        }

        public void SetCoin(Position i_CellPosition, eCoin i_Coin)
        {
            if(IsEmptyCell(i_CellPosition))
            {
                m_AvailableCells--;
            }

            changeCellCoin(i_CellPosition.Row, i_CellPosition.Col, i_Coin);
        }

        public void RemoveCoin(Position i_CellPosition)
        {
            if(!IsEmptyCell(i_CellPosition))
            {
                m_AvailableCells++;
            }

            changeCellCoin(i_CellPosition.Row, i_CellPosition.Col, eCoin.Empty);
        }

        public void SetCoinWithoutNotify(Position i_CellPosition, eCoin i_Coin)
        {
            if(IsEmptyCell(i_CellPosition))
            {
                m_AvailableCells--;
            }

            m_Board[i_CellPosition.Row, i_CellPosition.Col] = i_Coin;
        }

        public void RemoveCoinWithoutNotify(Position i_CellPosition)
        {
            if(!IsEmptyCell(i_CellPosition))
            {
                m_AvailableCells++;
            }

            m_Board[i_CellPosition.Row, i_CellPosition.Col] = eCoin.Empty;
        }

        public bool IsPosOutOfRange(Position i_CellPosition)
        {
            return i_CellPosition.Row < 0 || i_CellPosition.Row >= r_Size || i_CellPosition.Col < 0 || i_CellPosition.Col >= r_Size;
        }

        public eCoin GetCoin(Position i_CellPosition)
        {
            return m_Board[i_CellPosition.Row, i_CellPosition.Col];
        }

        public eCoin GetCoin(int i_Row, int i_Col)
        {
            return m_Board[i_Row, i_Col];
        }

        private void changeCellCoin(int i_Row, int i_Col, eCoin i_Coin)
        {
            m_Board[i_Row, i_Col] = i_Coin;
            OnCellChanged(i_Row, i_Col, i_Coin);
        }

        protected virtual void OnCellChanged(int i_Row, int i_Col, eCoin i_Coin)
        {
            if(CellChanged != null)
            {
                CellChanged(i_Row, i_Col, i_Coin);
            }
        }
    }
}
