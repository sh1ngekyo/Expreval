using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Expreval.Core.Runtime.Extensions;

using Xunit;

namespace Expreval.Core.Test.Runtime.Extensions
{
    public class StringParenthesesCheckerTest
    {
        [Fact]
        public void IsCorrectBrackets_CorrectCount_ReturnsTrue()
        {
            var input = "((test)t(((es)))t((test)))";

            var result = input.IsCorrectBrackets('(', ')');

            Assert.True(result);
        }

        [Fact]
        public void IsCorrectBrackets_IncorrectCount_ReturnsFalse()
        {
            var input = "((test)t(((es)))t((test";

            var result = input.IsCorrectBrackets('(', ')');

            Assert.False(result);
        }
    }
}
