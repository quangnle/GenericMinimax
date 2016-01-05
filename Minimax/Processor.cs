using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimax
{
    public class Processor
    {
        public IEvaluator<INode> Evaluator { get; set; }

        public Processor(IEvaluator<INode> evaluator)
        {
            Evaluator = evaluator;
        }

        public INode GetBestMove(INode node, int depth)
        {
            if (depth == 0) return node;

            var successors = node.GetSuccessors();
            if (successors != null && successors.Count > 0)
            {
                var maxScore = -1000;
                var chosenNode = successors[0];
                foreach (var successor in successors)
                {
                    var evalScore = Evaluator.Evaluate(successor, depth - 1, PlayerType.Min);
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
    }
}
