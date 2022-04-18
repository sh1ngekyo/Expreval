using Expreval.Core.Enums;
using Expreval.Core.Interfaces;
using Expreval.Core.Runtime.Models;

namespace Expreval.Core.Runtime.Factory
{
    public static class ExpressionTreeNodeFactory
    {
        public static ExpressionTreeNode<T> CreateNodeBinary<T>(IFunction func, ExpressionTreeNode<T> left, ExpressionTreeNode<T> right)
            => new(func, NodeType.Binary, left, right);

        public static ExpressionTreeNode<T> CreateNodeUnary<T>(IFunction func, ExpressionTreeNode<T> child)
            => new(func, NodeType.Unary, child, null);

        public static ExpressionTreeNode<T> CreateNodeVar<T>(T value)
            => new(value);
    }
}
