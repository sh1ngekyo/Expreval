using Expreval.Core.Exceptions;
using Expreval.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core
{
    public sealed class ExpressionConfiguration : IConfiguration
    {
        public ExpressionConfiguration()
        {
            Variables = new Dictionary<string, dynamic>();
            Functions = new Dictionary<char, IFunction>();
        }

        public Dictionary<string, dynamic> Variables { get; }

        public Dictionary<char, IFunction> Functions { get; }

        public void RegisterFunction<T>(char prefix, T function) where T : IFunction
        {
            if (!Functions.TryAdd(prefix, function))
            {
                throw new ValueAlreadyExistException($"Function '{prefix}' already exist in '{Functions}'");
            }
        }

        public void RegisterVariable<T>(string name, T variable)
        {
            if (!Variables.TryAdd(name, variable))
            {
                throw new ValueAlreadyExistException($"Variable '{name}' already exist in '{Variables}'");
            }
        }
    }
}
