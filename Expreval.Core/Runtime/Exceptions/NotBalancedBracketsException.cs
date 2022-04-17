using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Runtime.Exceptions
{
    public class NotBalancedBracketsException : Exception
    {
        public NotBalancedBracketsException()
        {
        }

        public NotBalancedBracketsException(string expression)
            : base($"Unequal number of brackets in expression: {expression}.")
        {
        }

        public NotBalancedBracketsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
