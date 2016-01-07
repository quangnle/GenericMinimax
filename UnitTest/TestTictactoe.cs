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
            var processor = new Processor(new MinimaxEvaluator(-1000, 1000));
            var board = new Board();
            var node = new Node(board, 0, 0, PlayerType.Max, PlayerType.Min);
            var score = node.Evaluate();

            var bestMove = processor.GetBestMove(node, 1);

            Assert.AreEqual(6, score);

        }
    }
}
