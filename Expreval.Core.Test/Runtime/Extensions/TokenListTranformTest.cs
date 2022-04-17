using Xunit;
using Moq;
using Expreval.Core.Enums;
using System.Collections.Generic;
using Expreval.Core.Interfaces;
using Expreval.Core.Runtime.Extensions;
using System;

namespace Expreval.Core.Test.Runtime.Extensions
{
    public class TokenListTranformTest
    {
        [Fact]
        public void TransformToPolish_InfixTokenList_ReturnsListOfTokensInPolish()
        {
            var inputTypes = new TokenType[] 
            { 
                TokenType.UnaryFunction,
                TokenType.OpenBracket,
                TokenType.Variable,
                TokenType.BinaryFunction,
                TokenType.Variable,
                TokenType.CloseBracket 
            };

            var expectedTypes = new TokenType[] 
            {
                TokenType.UnaryFunction,
                TokenType.BinaryFunction,
                TokenType.Variable,
                TokenType.Variable 
            };

            var infixList = new List<IToken>();
            for (var i = 0; i < inputTypes.Length; ++i)
            {
                var token = new Mock<IToken>();
                token.SetupGet(x => x.Type).Returns(inputTypes[i]);
                infixList.Add(token.Object);
            }

            var polishList = infixList.TransformToPolish();
            Assert.Equal(expectedTypes.Length, polishList.Count);
            for (var i = 0; i < polishList.Count; ++i)
            {
                Assert.Equal(expectedTypes[i], polishList[i].Type);
            }
        }
    }
}
