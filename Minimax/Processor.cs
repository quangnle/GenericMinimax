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
                var maxScore = -1000;
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
            if (depth == 0 || Math.Abs(score) >= 1000)
                return score;

            var successors = node.GetSuccessors();

            if (successors != null & successors.Count > 0)
            {
                if (playerType == PlayerType.Max)
                    return successors.Max(n => Evaluate((T)n, depth - 1, PlayerType.Min));
                else
                    return successors.Min(n => Evaluate((T)n, depth - 1, PlayerType.Max));
            }

            return score;
        }
    }
}
