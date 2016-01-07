using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimax
{
    public class MinimaxEvaluator : IEvaluator<INode>
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public MinimaxEvaluator(int minValue, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public int Evaluate(INode node, int depth, PlayerType playerType)
        {
            var score = node.Evaluate();
            if (depth == 0 || Math.Abs(score) >= 1000)
                return score;

            var successors = node.GetSuccessors();

            if (successors != null & successors.Count > 0)
            {
                if (playerType == PlayerType.Max)
                    return successors.Max(n => Evaluate(n, depth - 1, PlayerType.Min));
                else
                    return successors.Min(n => Evaluate(n, depth - 1, PlayerType.Max));
            }

            return score;
        }
    }
}
