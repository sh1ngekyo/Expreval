using Expreval.Core.Enums;
using Expreval.Core.Interfaces;
using Expreval.Core.Models;
using Expreval.Core.Runtime;
using Expreval.Core.Runtime.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Expreval.Core.Test.Runtime
{
    public class LexerTest
    {
        private ExpressionConfiguration<dynamic> SetupTestExpressionConfig((string Name, dynamic Value)[] vars, (char Prefix, IFunction Function)[] funcs)
        {
            var config = new ExpressionConfiguration<dynamic>();
            foreach (var item in vars)
                config.RegisterVariable(item.Name, item.Value);
            foreach (var item in funcs)
                config.RegisterFunction(item.Prefix, item.Function);
            return config;
        }

        [Fact]
        public void LexExpression_DefaultDynamicCase_ReturnsListOfTokens()
        {
            var expectedData = new TokenType[] {TokenType.Variable, TokenType.BinaryFunction, TokenType.Variable };

            var vars = new (string Name, dynamic Value)[] { ("A", 10), ("B", 20) };
            var mock = new Moq.Mock<IFunction>();
            mock.SetupGet(x => x.Type).Returns(Enums.FunctionType.Binary);
            var funcs = new (char Prefix, IFunction Function)[] { ('+', mock.Object) };
            var config = SetupTestExpressionConfig(vars, funcs);
            var expr = new Expression<dynamic>("A+B");
            expr.Configure(config);

            var result = new Lexer<dynamic>(expr).LexExpression();

            Assert.Equal(expectedData.Length, result.Count);

            for (var i = 0; i < result.Count; ++i)
            {
                Assert.Equal(expectedData[i], result[i].Type);
            }
        }

        [Fact]
        public void LexExpression_UndefinedToken_ThrowsUnknownTokenException()
        {
            var vars = new (string Name, dynamic Value)[] { ("A", 10), ("B", 20) };
            var mock = new Moq.Mock<IFunction>();
            mock.SetupGet(x => x.Type).Returns(Enums.FunctionType.Binary);
            var funcs = new (char Prefix, IFunction Function)[] { ('+', mock.Object) };
            var config = SetupTestExpressionConfig(vars, funcs);
            var expr = new Expression<dynamic>("A*B");
            expr.Configure(config);

            Assert.Throws<UnknownTokenException>(()=> { new Lexer<dynamic>(expr).LexExpression(); });
        }

        [Fact]
        public void LexExpression_NotBalanced_ThrowsNotBalancedBracketsException()
        {
            var vars = new (string Name, dynamic Value)[] { ("A", 10), ("B", 20) };
            var mock = new Moq.Mock<IFunction>();
            mock.SetupGet(x => x.Type).Returns(Enums.FunctionType.Binary);
            var funcs = new (char Prefix, IFunction Function)[] { ('+', mock.Object) };
            var config = SetupTestExpressionConfig(vars, funcs);
            var expr = new Expression<dynamic>("(A+B))");
            expr.Configure(config);

            Assert.Throws<NotBalancedBracketsException>(() => { new Lexer<dynamic>(expr).LexExpression(); });
        }
    }
}
