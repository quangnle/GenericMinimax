using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimax
{
    public interface IEvaluator<T> where T : INode
    {
        int MaxValue { get; set; }
        int MinValue { get; set; }

        int Evaluate(T node, int depth, PlayerType playerType);
    }
}
