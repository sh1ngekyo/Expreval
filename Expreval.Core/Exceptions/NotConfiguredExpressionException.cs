using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Exceptions
{
    public class NotConfiguredExpressionException : Exception
    {
        public NotConfiguredExpressionException()
        {
        }

        public NotConfiguredExpressionException(string expression)
            : base($"Not configured expression: {expression}.")
        {
        }

        public NotConfiguredExpressionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
