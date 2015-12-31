using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.AI
{
    public class Board
    {
        public int[,] Cells { get; set; }

        public int this[int row, int col] 
        {
            get { return Cells[row, col]; }
            set { Cells[row, col] = value; }
        }

        public Board()
        {
            Cells = new int[3, 3];
        }

        public Board Clone()
        {
            var cloned = new int[3, 3];

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    cloned[col, row] = Cells[col, row];
                }
            }

            return new Board { Cells = cloned };
        }
    }
}
