using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Exceptions
{
    public class ValueAlreadyExistException : Exception
    {
        public ValueAlreadyExistException()
        {
        }

        public ValueAlreadyExistException(string message)
            : base(message)
        {
        }

        public ValueAlreadyExistException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
