using Expreval.Core.Enums;
using Expreval.Core.Interfaces;
using Expreval.Core.Models;
using Expreval.Core.Runtime.Exceptions;
using Expreval.Core.Runtime.Extensions;
using Expreval.Core.Runtime.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Expreval.Core.Runtime
{
    public sealed class Lexer
    {
        private readonly Expression expression;

        public Lexer(Expression expression)
        {
            this.expression = expression;
        }

        public List<IToken> LexRequest()
        {
            if (!expression.Representation.IsCorrectBrackets((char)TokenType.OpenBracket, (char)TokenType.CloseBracket))
            {
                throw new NotBalancedBracketsException(expression.Representation);
            }

            var tokens = new List<IToken>();
            Regex.Split(input: expression.Representation.Replace(" ", ""),
                        pattern: @$"([{TokenType.OpenBracket}{TokenType.CloseBracket}{expression.Functions.Keys.ToString<char>()}])")
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList()
            .ForEach(x =>
            {
                Token t;
                if (expression.Variables.TryGetValue(x, out var variable))
                {
                    t = new Token(TokenType.Variable, variable);
                }
                else if (expression.Functions.TryGetValue(x[0], out var function))
                {
                    t = new Token((TokenType)function.Type, function);
                }
                else if (x[0] is ((char)TokenType.OpenBracket) or ((char)TokenType.CloseBracket))
                {
                    t = new Token((TokenType)x[0], x);
                }
                else
                {
                    throw new UnknownTokenException(x, expression.Representation);
                }
                tokens.Add(t);
            });
            return tokens;
        }
    }
}
