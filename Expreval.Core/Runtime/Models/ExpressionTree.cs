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
    public class ExpressionTreeNode<TData>
    {
        public NodeType Type { get; private set; }
        public ExpressionTreeNode<TData> Left { get; private set; }
        public ExpressionTreeNode<TData> Right { get; private set; }
        public TData Data { get; }
        public IFunction Function { get; }

        internal ExpressionTreeNode(IFunction func, NodeType type, ExpressionTreeNode<TData> left, ExpressionTreeNode<TData> right)
        {
            Function = func;
            Type = type;
            Left = left;
            Right = right;
            Data = default;
        }

        internal ExpressionTreeNode(TData value)
        {
            Data = value;
            Type = NodeType.Leaf;
            Left = null;
            Left = null;
            Function = default;
        }
    }
}
