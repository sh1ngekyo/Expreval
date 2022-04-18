using Expreval.Core.Enums;
using Expreval.Core.Interfaces;
using Expreval.Core.Runtime;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Expreval.Core.Test.Runtime
{
    public class ParserTest
    {
        private IToken CreateDummyToken(TokenType type, dynamic value)
        {
            var mock = new Moq.Mock<IToken>();
            mock.SetupGet(x => x.Type).Returns(type);
            mock.SetupGet(x => x.Value).Returns(value);
            return mock.Object;
        }

        [Fact]
        public void ParseTokens_DefaultCase_ReturnsExpressionTree()
        {
            var polishTokens = new List<IToken>()
            {
                CreateDummyToken(TokenType.UnaryFunction, null),
                CreateDummyToken(TokenType.OpenBracket, null),
                CreateDummyToken(TokenType.Variable, null),
                CreateDummyToken(TokenType.BinaryFunction, null),
                CreateDummyToken(TokenType.Variable, null),
                CreateDummyToken(TokenType.CloseBracket, null),
            };

            var root = new Parser(polishTokens).ParseTokens<object>();

            Assert.NotNull(root);
            Assert.Equal(NodeType.Unary, root.Type);

            Assert.NotNull(root.Left); 
            Assert.Null(root.Right);
            Assert.Equal(NodeType.Binary, root.Left.Type);

            Assert.NotNull(root.Left.Left);
            Assert.NotNull(root.Left.Right);
            Assert.Equal(NodeType.Leaf, root.Left.Left.Type);
            Assert.Equal(NodeType.Leaf, root.Left.Right.Type);
        }
    }
}
