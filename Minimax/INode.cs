using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimax
{
    public interface INode
    {   
        int Evaluate();
        bool IsTerminated { get; } 
        List<INode> GetSuccessors();
    }
}
