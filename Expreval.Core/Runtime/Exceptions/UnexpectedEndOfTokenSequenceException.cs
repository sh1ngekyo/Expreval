using Expreval.Core.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Runtime.Exceptions
{
    public class UnexpectedEndOfTokenSequenceException : Exception
    {
        public UnexpectedEndOfTokenSequenceException(string message = "Unexpected end of token sequence.")
            : base(message)
        {
        }

        public UnexpectedEndOfTokenSequenceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
