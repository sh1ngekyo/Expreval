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
        public static T Eval<T>(this Expression inputExpression)
        {
            return (T)(inputExpression.CreateNewExpressionTree().Eval());
        }

        public static ExpressionTreeNode CreateNewExpressionTree(this Expression inputExpression)
        {
            if (inputExpression is null)
                throw new ArgumentNullException(nameof(inputExpression));
            if (!inputExpression.IsConfigrured)
                throw new NotConfiguredExpressionException(inputExpression.Representation);

            return new Parser(new Lexer(inputExpression).LexExpression()).ParseTokens();
        }

        private static dynamic Eval(this ExpressionTreeNode expr)
        {
            if (expr.Type == NodeType.Binary)
                return GetCallMethod(expr.Data, "IBinaryFunction`3").Invoke(expr.Data, new object[] { Eval(expr.Right), Eval(expr.Left) });
            if (expr.Type == NodeType.Unary)
                return GetCallMethod(expr.Data, "IUnaryFunction`2").Invoke(expr.Data, new object[] { Eval(expr.Left) });
            return expr.Data;
        }

        private static MethodInfo GetCallMethod(dynamic handler, string functionInterface)
        {
            var _interface = handler.GetType().GetInterface(functionInterface);
            return _interface.GetMethod("Call", RemoveReturnType(_interface.GetGenericArguments()));
        }

        private static Type[] RemoveReturnType(Type[] args)
            => args.Skip(1).ToArray();
    }
}
