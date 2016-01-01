using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Minimax;
using TicTacToe.AI;

namespace UnitTest
{
    [TestClass]
    public class TestTictactoe
    {
        [TestMethod]
        public void TestNodeEvaluation()
        {
            var processor = new Processor<Node>();
            var board = new Board();
            var node = new Node(board, 0, 0, 1, 2);
            var lst = node.GetSuccessors();
            var maxscore = lst.Max(n => n.Evaluate());
            var score = node.Evaluate();

            var bestMove = processor.GetBestMove(node, 1);

            Assert.AreEqual(6, score);
            Assert.AreEqual(8, maxscore);

            board[1, 1] = 1;
            lst = node.GetSuccessors();
            maxscore = lst.Max(n => n.Evaluate());

            
        }
    }
}
