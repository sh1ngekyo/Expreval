using System;
using System.Collections.Generic;
using System.Linq;
using Expreval.Core.Interfaces;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Expreval.Core.Enums;
using Expreval.Core.Models;
using Expreval.Core.Exceptions;
using Expreval.Core.Runtime.Exceptions;

namespace Expreval.Core.Test
{
    public class InterpreterTest
    {
        [Fact]
        public void Eval_ExpressionSameTypes_ReturnsNewBooleanTrue()
        {
            bool A = false, B = false, C = true;

            var expected = !(A) & (B | C) & C;

            var config = new ExpressionConfiguration<bool>();
            config.RegisterVariable(nameof(A), A);
            config.RegisterVariable(nameof(B), B);
            config.RegisterVariable(nameof(C), C);
            config.RegisterFunction('&', new Dummy.Function.BooleanAnd());
            config.RegisterFunction('|', new Dummy.Function.BooleanOr());
            config.RegisterFunction('!', new Dummy.Function.BooleanNot());

            var expr = new Expression<bool>("!(A) & (B | C) & C");
            expr.Configure(config);

            var result = expr.Eval();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Eval_ExpressionSameTypes_ReturnsNewBooleanFalse()
        {
            bool A = false, B = false, C = true;

            var expected = (A) & (B | C) & !C;

            var config = new ExpressionConfiguration<bool>();
            config.RegisterVariable(nameof(A), A);
            config.RegisterVariable(nameof(B), B);
            config.RegisterVariable(nameof(C), C);
            config.RegisterFunction('&', new Dummy.Function.BooleanAnd());
            config.RegisterFunction('|', new Dummy.Function.BooleanOr());
            config.RegisterFunction('!', new Dummy.Function.BooleanNot());

            var expr = new Expression<bool>("(A) & (B | C) & !C");
            expr.Configure(config);

            Assert.Equal(expected, expr.Eval());
        }

        [Fact]
        public void Eval_WrongTokenSequence_ThrowsUnexpectedEndOfTokenSequenceException()
        {
            bool A = false, B = false, C = true;

            var expected = (A) & (B | C) & !C;

            var config = new ExpressionConfiguration<bool>();
            config.RegisterVariable(nameof(A), A);
            config.RegisterVariable(nameof(B), B);
            config.RegisterVariable(nameof(C), C);
            config.RegisterFunction('&', new Dummy.Function.BooleanAnd());
            config.RegisterFunction('|', new Dummy.Function.BooleanOr());
            config.RegisterFunction('!', new Dummy.Function.BooleanNot());

            var expr = new Expression<bool>("|!&");
            expr.Configure(config);

            Assert.Throws<UnexpectedEndOfTokenSequenceException>(()=> expr.Eval());
        }

        [Fact]
        public void Eval_NotConfiguredExpression_ThrowsNotConfiguredExpressionException()
        {
            var expr = new Expression<bool>("(A) & (B | C) & !C");

            Assert.Throws<NotConfiguredExpressionException>(() => expr.Eval<bool>());
        }

        [Fact]
        public void Eval_NullExpression_ThrowsArgumentNullException()
        {
            Expression<bool> expr = null;

            Assert.Throws<ArgumentNullException>(() => expr.Eval<bool>());
        }

        [Fact]
        public void Eval_ExpressionSameTypesPossibleCast_ReturnsNewIEnumerableInt()
        {
            List<int> A = new() { 1, 2, 3 }, B = new() { 4, 5, 6 };

            var config = new ExpressionConfiguration<IEnumerable<int>>();
            config.RegisterVariable(nameof(A), A);
            config.RegisterVariable(nameof(B), B);
            config.RegisterFunction('+', new Dummy.Function.IEnumerableConcat<int, int, int>());

            var expr = new Expression<IEnumerable<int>>("A+B");
            expr.Configure(config);

            var result = expr.Eval().ToList();

            Assert.NotNull(result);

            for (int i = 0, expected = 1; i < result.Count; ++i, ++expected)
            {
                Assert.Equal(expected, result[i]);
            }
        }

        [Fact]
        public void Eval_ExpressionDiffTypesImpossibleCast_ThrowsTargetInvocationException()
        {
            List<int> A = new() { 1, 2, 3 }; 
            List<double> B = new() { 0.5, 1.5, 2.5 };

            var config = new ExpressionConfiguration<dynamic>();
            config.RegisterVariable(nameof(A), A);
            config.RegisterVariable(nameof(B), B);
            config.RegisterFunction('+', new Dummy.Function.IEnumerableConcat<string, int, double>());

            var expr = new Expression<dynamic>("A+B");
            expr.Configure(config);

            Assert.Throws<System.Reflection.TargetInvocationException>(() => expr.Eval());
        }
    }
}
