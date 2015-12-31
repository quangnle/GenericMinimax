using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Minimax;

namespace TicTacToe.AI
{
    public class Node : INode
    {
        private Board _board;
        private int _nCol;
        public int Column { get { return _nCol; } }

        private int _nRow;
        public int Row { get { return _nRow; } }

        private int _player;
        private int _opponent;

        /// <summary>
        /// Implementation of INode in order to run Minimax/advanced
        /// </summary>
        /// <param name="board">Current state of the node</param>
        /// <param name="nRow">Row position of the newly added chess piece</param>
        /// <param name="nCol">Col position of the newly added chess piece</param>
        public Node(Board board, int nRow, int nCol, int player, int opponent)
        {
            _board = board;
            _nRow = nRow;
            _nCol = nCol;
            _player = player;
            _opponent = opponent;
        }

        public int Evaluate()
        {
            int[,] dX = {{-1, 1}, {-1, 1}, {0, 0}, {1, -1}};
            int[,] dY = {{0, 0}, {-1, 1}, {-1, 1}, {-1, 1}};

            var score = 0;
            for (int i = 0; i < 4; i++)
            {   
                var dscore = 0;
                var isOccupied = false;
                for (int j = 0; j < 2; j++)
                {
                    var nx = _nCol;
                    var ny = _nRow;
                    do
                    {
                        nx = nx + dX[i, j];
                        ny = ny + dY[i, j];
                        if (nx >= 0 && nx < 3 && ny >= 0 && ny < 3)
                        {
                            if (_board[ny, nx] == _player)
                                dscore = (dscore + 1) * 2;
                            else if (_board[ny, nx] == 0)
                                dscore = dscore + 1;
                            else
                            {
                                dscore = 0;
                                isOccupied = true;
                                break;
                            }
                        }
                    } while (nx >= 0 && nx < 3 && ny >= 0 && ny < 3);

                    if (isOccupied) break;
                }
                score += dscore;
            }

            return score;
        }

        public List<INode> GetSuccessors()
        {
            var result = new List<INode>();
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (_board[row, col] == 0)
                    {
                        var newBoard = _board.Clone();
                        newBoard[row, col] = _opponent;
                        var newNode = new Node(newBoard, row, col, _opponent, _player);
                        result.Add(newNode);
                    }
                }
            }

            return result;  
        }
    }
}
