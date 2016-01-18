using System;                                                                                          
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimax
{
    public class AlphaBetaEvaluator : IEvaluator<INode>
    {
        public int MaxValue { get; set; }
        public int MinValue { get; set; }

        public AlphaBetaEvaluator(int minValue, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public int Evaluate(INode node, int depth, PlayerType playerType)
        {
            var score = AlphaBeta(node, depth - 1, MinValue, MaxValue, playerType);
            return score;
        }

        private int AlphaBeta(INode node, int depth, int alpha, int beta, PlayerType playerType)
        {
            var score = node.Evaluate();
            if (depth == 0 || node.IsTerminated)
                return score;

            var successors = node.GetSuccessors();
            if (successors != null && successors.Count != 0)
            {
                score = 0;

                if (playerType == PlayerType.Max)
                {
                    score = MinValue;
                    foreach (var item in successors)
                    {
                        score = Math.Max(score, AlphaBeta(item, depth - 1, alpha, beta, PlayerType.Min));
                        alpha = Math.Max(score, alpha);
                        if (beta <= alpha)
                            break;
                    }
                }
                else
                {
                    score = MaxValue;
                    foreach (var item in successors)
                    {
                        score = Math.Min(score, AlphaBeta(item, depth - 1, alpha, beta, PlayerType.Max));
                        beta = Math.Min(score, beta);
                        if (beta <= alpha)
                            break;
                    }
                }
            }

            return score;
        }
    }
}

