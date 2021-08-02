using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class ComputerLogic
    {
        private static Random s_Rnd = new Random();
        private static BoardGame.eCoin s_ComputerCoin;
        private static BoardGame.eCoin s_OpponentCoin;
        
        public static Position GetNextPosition(BoardGame i_Board, BoardGame.eCoin i_ComputerCoin)
        {
            Position nextPosition;
            s_ComputerCoin = i_ComputerCoin;
            s_OpponentCoin = flipCoin(i_ComputerCoin);
            int maxDepth = 9;

            if(i_Board.AvailableCells > maxDepth)
            {
                nextPosition = getNextRandomPos(i_Board);

                if(isLoosingMove(i_Board, nextPosition))
                {
                    getNextAvailablePos(i_Board, nextPosition);
                }
            }
            else
            {
                nextPosition = getNextSmartPos(i_Board);
            }

            return nextPosition;
        }

        private static void getNextAvailablePos(BoardGame i_Board, Position i_NextPosition)
        {
            Position tempPosition = new Position(0, 0);

            while(!i_Board.IsPosOutOfRange(tempPosition))
            {
                if(i_Board.IsEmptyCell(tempPosition) && !isLoosingMove(i_Board, tempPosition))
                {
                    i_NextPosition.ChangePositionTo(tempPosition);
                    break;
                }

                tempPosition.SetNext(i_Board.Size);
            }
        }

        private static Position getNextRandomPos(BoardGame i_Board)
        {
            Position randomPosition = new Position(-1, -1);
            Position tempPosition = new Position(0, 0);

            int randomAvailablePos = s_Rnd.Next(i_Board.AvailableCells);
            int availablePosCounter = -1;

            while(!i_Board.IsPosOutOfRange(tempPosition))
            {
                if(i_Board.IsEmptyCell(tempPosition))
                {
                    availablePosCounter++;

                    if(availablePosCounter == randomAvailablePos)
                    {
                        randomPosition.ChangePositionTo(tempPosition);
                        break;
                    }
                }

                tempPosition.SetNext(i_Board.Size);
            }

            return randomPosition;
        }

        private static Position getNextSmartPos(BoardGame i_Board)
        {
            Position bestPosition = new Position(-1, -1);
            Position tempPosition = new Position(0, 0);
            bool isMaximize = true;
            int bestScore = int.MinValue;
            int depth = 0;

            while(!i_Board.IsPosOutOfRange(tempPosition))
            {
                if(i_Board.IsEmptyCell(tempPosition))
                {
                    i_Board.SetCoinWithoutNotify(tempPosition, s_ComputerCoin);
                    int score = calcMimMaxScore(i_Board, depth, !isMaximize, s_OpponentCoin);
                    i_Board.RemoveCoinWithoutNotify(tempPosition);

                    if(score > bestScore)
                    {
                        bestScore = score;
                        bestPosition.ChangePositionTo(tempPosition);
                    }
                }

                tempPosition.SetNext(i_Board.Size);
            }

            return bestPosition;
        }

        private static int calcMimMaxScore(BoardGame i_Board, int i_Depth, bool i_IsMaximize, BoardGame.eCoin i_CurrrntCoin)
        {
            int bestScore;
            BoardGame.eCoin opponentCoin = flipCoin(i_CurrrntCoin);
            Position tempPosition = new Position(0, 0);

            if(i_Board.HasFullSequence(s_ComputerCoin))
            {
                bestScore = i_Depth - 100;
            }
            else if(i_Board.HasFullSequence(s_OpponentCoin))
            {
                bestScore = 100 - i_Depth;
            }
            else if(i_Board.IsFull())
            {
                bestScore = 0;
            }
            else
            {
                bestScore = i_IsMaximize ? int.MinValue : int.MaxValue;

                while(!i_Board.IsPosOutOfRange(tempPosition))
                {
                    if(i_Board.IsEmptyCell(tempPosition))
                    {
                        i_Board.SetCoinWithoutNotify(tempPosition, i_CurrrntCoin);
                        int score = calcMimMaxScore(i_Board, i_Depth + 1, !i_IsMaximize, opponentCoin);
                        i_Board.RemoveCoinWithoutNotify(tempPosition);

                        bestScore = i_IsMaximize ? Math.Max(bestScore, score) : Math.Min(bestScore, score);
                    }

                    tempPosition.SetNext(i_Board.Size);
                }
            }

            return bestScore;
        }

        private static BoardGame.eCoin flipCoin(BoardGame.eCoin i_Coin)
        {
            return i_Coin == BoardGame.eCoin.Player1 ? BoardGame.eCoin.Player2 : BoardGame.eCoin.Player1;
        }

        private static bool isLoosingMove(BoardGame i_Board, Position i_NextPosition)
        {
            i_Board.SetCoinWithoutNotify(i_NextPosition, s_ComputerCoin);
            bool isLoosing = i_Board.HasFullSequence(s_ComputerCoin);
            i_Board.RemoveCoinWithoutNotify(i_NextPosition);

            return isLoosing;
        }
    }
}
