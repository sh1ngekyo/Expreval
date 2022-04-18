using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expreval.Core.Interfaces
{
    public interface IConfigurable<TVariable>
    {
        IReadOnlyDictionary<char, IFunction> Functions { get; }

        IReadOnlyDictionary<string, TVariable> Variables { get; }

        bool IsConfigrured { get; }

        string Representation { get; }

        void Configure(IConfiguration<TVariable> configuration);
    }
}
