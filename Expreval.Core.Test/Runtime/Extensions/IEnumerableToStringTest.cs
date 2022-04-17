using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Expreval.Core.Runtime.Extensions;

using Xunit;

namespace Expreval.Core.Test.Runtime.Extensions
{
    public class IEnumerableToStringTest
    {
        [Fact]
        public void ToString_ListChar_ReturnsString()
        {
            var input = new List<char>() { '1', '2', '3' };

            var expected = "123";

            var result = input.ToString<char>();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToString_ListInt_ReturnsString()
        {
            var input = new List<int>() { 1, 2, 3 };

            var expected = "123";

            var result = input.ToString<int>();

            Assert.Equal(expected, result);
        }
    }
}
