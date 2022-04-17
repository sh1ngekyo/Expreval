using Expreval.Core.Enums;
using Expreval.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Expreval.Core.Runtime.Extensions
{
    public static class TokenListExtensions
    {
        public static List<IToken> TransformToPolish(this List<IToken> infixTokenList)
        {
            var outputQueue = new Queue<IToken>();
            var stack = new Stack<IToken>();

            int index = 0;
            while (infixTokenList.Count > index)
            {
                var t = infixTokenList[index];

                switch (t.Type)
                {
                    case TokenType.Variable:
                        outputQueue.Enqueue(t);
                        break;
                    case TokenType.OpenBracket:
                    case TokenType.BinaryFunction:
                    case TokenType.UnaryFunction:
                        stack.Push(t);
                        break;
                    case TokenType.CloseBracket:
                        while (stack.Peek().Type != TokenType.OpenBracket)
                        {
                            outputQueue.Enqueue(stack.Pop());
                        }
                        stack.Pop();
                        if (stack.Count > 0 && stack.Peek().Type == TokenType.UnaryFunction)
                        {
                            outputQueue.Enqueue(stack.Pop());
                        }
                        break;
                }

                ++index;
            }
            while (stack.Count > 0)
            {
                outputQueue.Enqueue(stack.Pop());
            }

            return outputQueue.Reverse().ToList();
        }
    }
}
