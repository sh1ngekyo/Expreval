using Expreval.Core.Exceptions;
using Expreval.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core
{
    public sealed class ExpressionConfiguration<TVariable> : IConfiguration<TVariable>
    {
        public ExpressionConfiguration()
        {
            Variables = new Dictionary<string, TVariable>();
            Functions = new Dictionary<char, IFunction>();
        }

        public Dictionary<char, IFunction> Functions { get; }

        public Dictionary<string, TVariable> Variables { get; }
         
        public void RegisterFunction(char prefix, IFunction function)
        {
            if (!Functions.TryAdd(prefix, function))
            {
                throw new ValueAlreadyExistException($"Function '{prefix}' already exist in '{Functions}'");
            }
        }

        public void RegisterFunctions(params (char Prefix, IFunction Function)[] functions)
        {
            foreach (var item in functions)
            {
                RegisterFunction(item.Prefix, item.Function);
            }
        }

        public void RegisterVariable(string name, TVariable variable)
        {
            if (!Variables.TryAdd(name, variable))
            {
                throw new ValueAlreadyExistException($"Variable '{name}' already exist in '{Variables}'");
            }
        }

        public void RegisterVariables(params (string Name, TVariable Variable)[] variables)
        {
            foreach (var item in variables)
            {
                RegisterVariable(item.Name, item.Variable);
            }
        }
    }
}
