using Expreval.Core;
using Expreval.Core.Interfaces;
using Expreval.Core.Models;

using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Expreval.CLI.TruthTable
{
    class EntryPoint
    {
        private static void DisplayHeader()
        {
            Console.WriteLine("    Function  Description");
            Console.WriteLine("\t&\tAnd;");
            Console.WriteLine("\t|\tOr;");
            Console.WriteLine("\t!\tNot;");
            Console.WriteLine("\t>\tImlication;");
            Console.WriteLine("\t^\tXor;");
            Console.Write("\nYour expression (for example A & B): ");
        }

        private static IConfiguration<bool> CreateConfig()
        {
            var config = new ExpressionConfiguration<bool>();
            config.RegisterFunctions(
                ('&', Functions.BooleanAnd.Instanse),
                ('|', Functions.BooleanOr.Instanse),
                ('!', Functions.BooleanNot.Instanse),
                ('>', Functions.BooleanImplication.Instanse),
                ('^', Functions.BooleanXor.Instanse));
            return config;
        }

        private static void RegisterVariables(string expr, IConfiguration<bool> configuration)
        {
            Regex.Split(input: expr.Replace(" ", ""),
                            pattern: @$"([()&|!>^])")
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .ToList()
                .ForEach(x =>
                {
                    if (Regex.IsMatch(x, "(\\w+)") && !configuration.Variables.ContainsKey(x))
                    {
                        configuration.RegisterVariable(x, default);
                    }
                });
        }

        private static void BuildTable(string expressionRepresent, IConfiguration<bool> config)
        {
            var vars = config.Variables.Keys.ToList();
            vars.ForEach(x => Console.Write($"{x}\t"));
            Console.WriteLine("Result");
            for (var i = 0; i < (1 << vars.Count); i++)
            {
                var data = Enumerable.Range(0, vars.Count).Select(j => (i & (1 << j)) != 0).ToArray();
                for (int j = data.Length - 1; j >= 0; --j)
                {
                    config.Variables[vars[data.Length - 1 - j]] = data[j];
                    Console.Write(Convert.ToInt32(config.Variables[vars[data.Length - 1 - j]]) + "\t");
                }
                var expr = new Expression<bool>(expressionRepresent);
                expr.Configure(config);
                Console.Write(Convert.ToInt32(expr.Eval()));
                Console.WriteLine();
            }
        }

        private static void Main(string[] args)
        {
            while (true)
            {
                DisplayHeader();
                var config = CreateConfig();
                var expressionRepresent = Console.ReadLine();
                RegisterVariables(expressionRepresent, config);
                BuildTable(expressionRepresent, config);
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
