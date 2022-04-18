using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Interfaces
{
    public interface IConfiguration<TVariable>
    {
        Dictionary<char, IFunction> Functions { get; }

        Dictionary<string, TVariable> Variables { get; }

        void RegisterVariables(params (string Name, TVariable Variable)[] variables);

        void RegisterFunctions(params (char Prefix, IFunction Function)[] functions);

        void RegisterVariable(string name, TVariable variable);

        void RegisterFunction(char prefix, IFunction function);
    }
}
