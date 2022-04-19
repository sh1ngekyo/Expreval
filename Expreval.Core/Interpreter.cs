using Expreval.Core.Enums;
using Expreval.Core.Interfaces;
using Expreval.Core.Exceptions;
using Expreval.Core.Models;
using Expreval.Core.Runtime;
using Expreval.Core.Runtime.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Expreval.Core
{
    public static class Interpreter
    {
        public static Expression<T> ToExpression<T>(this string expressionRepresentation) 
            => string.IsNullOrWhiteSpace(expressionRepresentation)
                ? throw new ArgumentException($"'{nameof(expressionRepresentation)}' cannot be null or whitespace.", nameof(expressionRepresentation))
                : new Expression<T>(expressionRepresentation);

        public static Expression<T> ToConfiguredExpression<T>(this string expressionRepresentation, IConfiguration<T> configuration)
        {
            if (string.IsNullOrWhiteSpace(expressionRepresentation))
                throw new ArgumentException($"'{nameof(expressionRepresentation)}' cannot be null or empty.", nameof(expressionRepresentation));

            if (configuration is null)
                throw new ArgumentNullException(nameof(configuration));

            var expr = new Expression<T>(expressionRepresentation);
            expr.Configure(configuration);
            return expr;
        }

        public static T Eval<T>(this Expression<T> inputExpression) 
            => inputExpression.CreateNewExpressionTree().Eval<T>();

        public static ExpressionTreeNode<T> CreateNewExpressionTree<T>(this Expression<T> inputExpression)
        {
            if (inputExpression is null)
                throw new ArgumentNullException(nameof(inputExpression));
            if (!inputExpression.IsConfigrured)
                throw new NotConfiguredExpressionException(inputExpression.Representation);

            return new Parser(new Lexer<T>(inputExpression).LexExpression()).ParseTokens<T>();
        }

        public static T Eval<T>(this ExpressionTreeNode<T> expr)
        {
            if (expr.Type == NodeType.Binary)
                return (T)GetCallMethod(expr.Function, "IBinaryFunction`3").Invoke(expr.Function, new object[] { Eval<T>(expr.Right), Eval<T>(expr.Left) });
            if (expr.Type == NodeType.Unary)
                return (T)GetCallMethod(expr.Function, "IUnaryFunction`2").Invoke(expr.Function, new object[] { Eval<T>(expr.Left) });
            return expr.Data;
        }

        private static MethodInfo GetCallMethod(object handler, string functionInterface)
        {
            var _interface = handler.GetType().GetInterface(functionInterface);
            return _interface.GetMethod("Call", RemoveReturnType(_interface.GetGenericArguments()));
        }

        private static Type[] RemoveReturnType(Type[] args)
            => args.Skip(1).ToArray();
    }
}
