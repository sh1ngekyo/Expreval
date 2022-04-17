using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Enums
{
    public enum TokenType : short
    {
        UnaryFunction,
        BinaryFunction,
        Variable = short.MaxValue,
        OpenBracket = 0x28,
        CloseBracket = 0x29
    }
}
