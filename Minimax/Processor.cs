using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimax
{
    public class Processor<T> where T : INode
    {

        public INode GetBestMove(T node, int depth)
        {
            if (depth == 0) return node;

            var successors = node.GetSuccessors();
            if (successors != null && successors.Count > 0)
            {
                var maxScore = 0;
                var chosenNode = successors[0];
                foreach (var successor in successors)
                {
                    var evalScore = Evaluate((T)successor, depth - 1, PlayerType.Min);
                    if (evalScore > maxScore)
                    {
                        maxScore = evalScore;
                        chosenNode = successor;
                    }
                }

                return chosenNode;
            }

            return node;
        }

        private int Evaluate(T node, int depth, PlayerType playerType)
        {
            var score = node.Evaluate();
            if (depth == 0 || Math.Abs(score) >= 100000)
                return score;

            var successors = node.GetSuccessors();

            if (successors != null & successors.Count > 0)
            {
                var bestValue = 100000;
                if (playerType == PlayerType.Max)
                    bestValue = -100000;

                foreach (var successor in successors)
                {   
                    var sucPlayerType = playerType == PlayerType.Max ? PlayerType.Min : PlayerType.Max;
                    var val = Evaluate((T)successor, depth - 1, sucPlayerType);
                    if (playerType == PlayerType.Max)
                        bestValue = Math.Max(bestValue, val);
                    else
                        bestValue = Math.Min(bestValue, val);
                }

                return bestValue;
            }

            return node.Evaluate();
        }
    }
}
