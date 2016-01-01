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
        /// Implementation of INode in order to run Minimax/Advanced
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
            var pScore = Evaluate(_player);
            if (pScore >= 100000) return 100000;

            var oScore = Evaluate(_opponent);
            if (oScore >= 100000) return -100000;
            
            var score = Evaluate(_player) + Evaluate(_opponent) * (-1);
            return score;
        }

        private int Evaluate(int player)
        {
            var score = 0;

            var vectors = new int[,,] {{{0 ,0}, {0, 1}}, 
                                       {{1, 0}, {0, 1}}, 
                                       {{2, 0}, {0, 1}}, 
                                       {{0, 0}, {1, 0}}, 
                                       {{0, 1}, {1, 0}}, 
                                       {{0, 2}, {1, 0}}, 
                                       {{0, 0}, {1, 1}}, 
                                       {{0, 2}, {1, -1}}};

            // eight lines: 3cols, 3 rows, 2 diagonals
            for (int i = 0; i < 8; i++)
            {   
                var lineScore = 0;
                var py = vectors[i, 0, 0];
                var px = vectors[i, 0, 1];
                var dy = vectors[i, 1, 0];
                var dx = vectors[i, 1, 1];
                var nodeCount = 0;

                // 3 cells each line
                for (int k = 0; k < 3; k++)
                {
                    var col = px + dx * k;
                    var row = py + dy * k;
                    

                    if (_board[row, col] == player)
                    {
                        nodeCount++;
                        lineScore += (int)Math.Pow(10, nodeCount);
                    }
                        
                    else if (_board[row, col] == 0)
                        lineScore += 1;
                    else
                    {
                        lineScore = 0;
                        break;
                    }
                }
                
                if (lineScore >= 1000)
                {
                    score = 100000;
                    break;
                }
                score += lineScore;
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
