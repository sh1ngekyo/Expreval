using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Runtime.Exceptions
{
    public class UnknownTokenException : Exception
    {
        public UnknownTokenException()
        {
        }

        public UnknownTokenException(string token, string expression)
            : base($"Unknown token '{token}' in expression: {expression}.")
        {
        }

        public UnknownTokenException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
