using Expreval.Core.Enums;
using Expreval.Core.Interfaces;
using Expreval.Core.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Runtime.Models
{
    public class ExpressionTreeNode
    {
        public NodeType Type { get; private set; }
        public ExpressionTreeNode Left { get; private set; }
        public ExpressionTreeNode Right { get; private set; }
        public dynamic Data { get; }

        internal ExpressionTreeNode(IFunction func, NodeType type, ExpressionTreeNode left, ExpressionTreeNode right)
        {
            Data = func;
            Type = type;
            Left = left;
            Right = right;
        }

        internal ExpressionTreeNode(dynamic value)
        {
            Data = value;
            Type = NodeType.Leaf;
            Left = null;
            Left = null;
        }
    }
}
