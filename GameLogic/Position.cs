using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class Position
    {
        public Position(int i_Row, int i_Col)
        {
            Row = i_Row;
            Col = i_Col;
        }

        public int Row { get; set; }

        public int Col { get; set; }

        public void ChangePositionTo(int i_Row, int i_Col)
        {
            Row = i_Row;
            Col = i_Col;
        }

        public void ChangePositionTo(Position i_OtherPosition)
        {
            Row = i_OtherPosition.Row;
            Col = i_OtherPosition.Col;
        }

        public void SetNext(int i_MaxSize)
        {
            ChangePositionTo(Row + ((Col + 1) / i_MaxSize), (Col + 1) % i_MaxSize);
        }
    }
}
