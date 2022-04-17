using Expreval.Core.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Interfaces
{
    public interface IToken
    {
        TokenType Type { get; set; }
        dynamic Value { get; set; }
    }
}
