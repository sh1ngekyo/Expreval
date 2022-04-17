using Expreval.Core.Enums;
using Expreval.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Runtime.Models
{
    internal class Token : IToken
    {
        public Token(TokenType type, dynamic value)
        {
            Type = type;
            Value = value;
        }

        public TokenType Type { get; set; }
        public dynamic Value { get; set; }
    }
}
