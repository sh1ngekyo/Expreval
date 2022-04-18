using Expreval.Core.Enums;
using Expreval.Core.Interfaces;
using Expreval.Core.Models;
using Expreval.Core.Runtime.Extensions;
using Expreval.Core.Runtime.Factory;
using Expreval.Core.Runtime.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Runtime
{
    public sealed class Parser
    {
        private readonly List<IToken> infixTokens;

        public Parser(List<IToken> infixTokens)
        {
            this.infixTokens = infixTokens;
        }

        public ExpressionTreeNode<T> ParseTokens<T>()
        {
            var tokensEnumerator = infixTokens.TransformToPolish().GetEnumerator();
            tokensEnumerator.MoveNext();
            var root = BuildExpressionTree<T>(ref tokensEnumerator);
            return root;
        }

        private ExpressionTreeNode<T> BuildExpressionTree<T>(ref List<IToken>.Enumerator tokensEnumerator)
        {
            var value = tokensEnumerator.Current.Value;
            if (tokensEnumerator.Current.Type == TokenType.UnaryFunction)
            {
                tokensEnumerator.MoveNext();
                var operand = BuildExpressionTree<T>(ref tokensEnumerator);
                return ExpressionTreeNodeFactory.CreateNodeUnary<T>(value, operand);
            }
            else if (tokensEnumerator.Current.Type == TokenType.BinaryFunction)
            {
                tokensEnumerator.MoveNext();
                var left = BuildExpressionTree<T>(ref tokensEnumerator);
                var right = BuildExpressionTree<T>(ref tokensEnumerator);
                return ExpressionTreeNodeFactory.CreateNodeBinary<T>(value, left, right);
            }
            var lit = ExpressionTreeNodeFactory.CreateNodeVar<T>(value);
            tokensEnumerator.MoveNext();
            return lit;
        }
    }
}
