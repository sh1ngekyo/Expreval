using Expreval.Core;
using Expreval.Core.Enums;
using Expreval.Core.Interfaces;
using Expreval.Core.Models;

using System;

namespace ConsoleApp1
{
    class Program
    {
        private class DummyBooleanAnd : IBinaryFunction<bool, bool, bool>
        {
            public FunctionType Type { get; set; }

            public bool Call(bool left, bool right)
            {
                return left & right;
            }
        }
        private class DummyBooleanOr : IBinaryFunction<bool, bool, bool>
        {
            public FunctionType Type { get; set; }

            public bool Call(bool left, bool right)
            {
                return left | right;
            }
        }
        private class DummyBooleanNot : IUnaryFunction<bool, bool>
        {
            public FunctionType Type { get; set; }

            public bool Call(bool var)
            {
                return !var;
            }
        }
        static void Main(string[] args)
        {
            /*bool A = false, B = false, C = true;

            var expected = !(A) & (B | C) & C;

            var config = new ExpressionConfiguration();
            config.RegisterVariable(nameof(A), A);
            config.RegisterVariable(nameof(B), B);
            config.RegisterVariable(nameof(C), C);
            config.RegisterFunction('&', new DummyBooleanAnd { Type = FunctionType.Binary });
            config.RegisterFunction('|', new DummyBooleanOr { Type = FunctionType.Binary });
            config.RegisterFunction('!', new DummyBooleanNot { Type = FunctionType.Unary });

            var expr = new Expression("!(A) & (B | C) & C");
            expr.Configure(config);*/

            while (true)
            {
                var config = new ExpressionConfiguration();
                config.RegisterFunction('&', new DummyBooleanAnd { Type = FunctionType.Binary });
                config.RegisterFunction('|', new DummyBooleanOr { Type = FunctionType.Binary });
                config.RegisterFunction('!', new DummyBooleanNot { Type = FunctionType.Unary });
                Expression expr;
                Console.WriteLine("Wellcome to binary calc");
                Console.WriteLine("1. For set var.");
                Console.WriteLine("2. For eval expression");
                while (Convert.ToChar(Console.ReadLine()) != '2')
                {
                    Console.Write("Enter var name, value: ");
                    var splited = Console.ReadLine().Split(",");
                    config.RegisterVariable(splited[0], Convert.ToBoolean(splited[1]));
                }
                Console.Write("Enter expression: "); 
                expr = new Expression(Console.ReadLine());
                expr.Configure(config);
                Console.WriteLine(expr.Representation + " = " + expr.Eval<bool>());
                Console.ReadLine();
                Console.Clear();
            }

            //Console.WriteLine(expr.Eval<bool>());
        }
    }
}
