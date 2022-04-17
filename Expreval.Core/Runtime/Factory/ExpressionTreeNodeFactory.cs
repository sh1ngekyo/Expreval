using Expreval.Core.Enums;
using Expreval.Core.Interfaces;
using Expreval.Core.Runtime.Models;

namespace Expreval.Core.Runtime.Factory
{
    public static class ExpressionTreeNodeFactory
    {
        public static ExpressionTreeNode CreateNodeBinary(IFunction func, ExpressionTreeNode left, ExpressionTreeNode right)
            => new(func, NodeType.Binary, left, right);

        public static ExpressionTreeNode CreateNodeUnary(IFunction func, ExpressionTreeNode child)
            => new(func, NodeType.Unary, child, null);

        public static ExpressionTreeNode CreateNodeVar(dynamic value)
            => new(value);
    }
}
