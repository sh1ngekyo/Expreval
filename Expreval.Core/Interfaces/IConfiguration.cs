using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Interfaces
{
    public interface IConfiguration
    {
        Dictionary<char, IFunction> Functions { get; }

        Dictionary<string, dynamic> Variables { get; }

        void RegisterVariable<T>(string name, T variable);

        void RegisterFunction<T>(char prefix, T function) where T : IFunction;
    }
}
